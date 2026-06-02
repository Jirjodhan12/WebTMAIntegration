namespace WebTMAIntegration.Models
{
    public class PagedResponseModel<T>
    {
        public List<T> Data { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
    }
}
