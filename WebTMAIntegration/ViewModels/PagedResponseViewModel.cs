namespace WebTMAIntegration.ViewModels

{
    public class PagedResponseViewModel<T>
    {
        public List<T> Data { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
    }
}
