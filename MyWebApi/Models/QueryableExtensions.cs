using Microsoft.EntityFrameworkCore;
using MyModels.DTO.Paged;
using System.Linq.Expressions;
using System.Reflection;

namespace MyWebApi.Models
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResultDTO<T>> ToPagedResultAsync<T>(
            this IQueryable<T> query,
            PagedRequestBase request,
            Expression<Func<T, bool>>? searchPredicate = null)
        {
            // 1️⃣ 搜尋條件
            if (!string.IsNullOrWhiteSpace(request.SearchKeyword) && searchPredicate != null)
            {
                query = query.Where(searchPredicate);
            }

            // 2️⃣ 排序處理（含 fallback）
            if (request.SortFields != null && request.SortFields.Count > 0)
            {
                query = query.OrderBySortFields(request.SortFields);
            }
            else
            {
                // ➕ 預設排序 fallback（例如 Id）
                var idProp = typeof(T).GetProperty("Id", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (idProp != null)
                {
                    query = query.OrderBySortFields([new() { Field = "Id", Desc = false }]);

                }
                else
                {
                    throw new InvalidOperationException($"請指定排序欄位，或確保 {typeof(T).Name} 有 Id 欄位");
                }
            }

            // 3️⃣ 分頁計算
            int totalCount = await query.CountAsync();
            int skip = (request.PageNumber - 1) * request.PageSize;
            var dataList = await query.Skip(skip).Take(request.PageSize).ToListAsync();

            return new PagedResultDTO<T>(request.PageNumber, request.PageSize, totalCount, dataList);
        }

        // ✅ 動態排序方法不需改動
        public static IQueryable<T> OrderByDynamic<T>(
            this IQueryable<T> query, 
            string sortBy, 
            bool desc)
        {
            var entityType = typeof(T);
            var property = entityType.GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (property == null)
                return query; // 欄位不存在，不排序

            var parameter = Expression.Parameter(entityType, "x");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            string methodName = desc ? "OrderByDescending" : "OrderBy";
            var resultExpression = Expression.Call(typeof(Queryable), methodName,
                [entityType, property.PropertyType],
                query.Expression, Expression.Quote(orderByExpression));

            return query.Provider.CreateQuery<T>(resultExpression);
        }

        public static IQueryable<T> OrderBySortFields<T>(
            this IQueryable<T> query, 
            List<SortField> sortFields)
        {
            if (sortFields == null || sortFields.Count == 0)
                return query;

            var parameter = Expression.Parameter(typeof(T), "x");
            var source = query;

            foreach (var (sortField, index) in sortFields.Select((s, i) => (s, i)))
            {
                var property = typeof(T).GetProperty(sortField.Field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property == null) continue;

                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);

                string methodName = index == 0
                    ? sortField.Desc ? "OrderByDescending" : "OrderBy"
                    : sortField.Desc ? "ThenByDescending" : "ThenBy";

                var resultExp = Expression.Call(typeof(Queryable), methodName,
                    new Type[] { typeof(T), property.PropertyType },
                    source.Expression, Expression.Quote(orderByExp));

                source = query.Provider.CreateQuery<T>(resultExp); // ✅ Apply 回 IQueryable
            }

            return source;
        }

    }
}