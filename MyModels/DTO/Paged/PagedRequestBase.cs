namespace MyModels.DTO.Paged
{
    public class PagedRequestBase
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? SearchKeyword { get; set; }
        // SortFields[0].Field=price&SortFields[0].Desc=true
        public List<SortField>? SortFields { get; set; } 
    }
}
