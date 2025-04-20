
using MyModels.DTO.Paged;
using System.Reflection;

namespace MyServices
{
    public static class QueryStringBuilder
    {
        public static string ToQueryString(this object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj) != null
                             from value in ToQueryParam(p, p.GetValue(obj)!)
                             select $"{Uri.EscapeDataString(value.Key)}={Uri.EscapeDataString(value.Value)}";

            return string.Join("&", properties);
        }

        private static IEnumerable<KeyValuePair<string, string>> ToQueryParam(PropertyInfo prop, object value)
        {
            if (value is IEnumerable<SortField> sortList)
            {
                int index = 0;
                foreach (var sort in sortList)
                {
                    yield return new($"{prop.Name}[{index}].Field", sort.Field);
                    yield return new($"{prop.Name}[{index}].Desc", sort.Desc.ToString().ToLower());
                    index++;
                }
            }
            else
            {
                yield return new(prop.Name, value.ToString()!);
            }
        }
    }

}
