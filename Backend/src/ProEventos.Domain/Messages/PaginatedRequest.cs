namespace ProEventos.Domain.Messages
{
    public class PaginatedRequest : BaseRequest
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public string SearchValue { get; set; }
    }
}
