using System;

namespace ProEventos.Domain.Messages
{
    public class PaginatedResponse<T> : BaseResponse<T> where T : class
    {
        public PaginatedResponse(
            T data,
            int currentPage,
            int pageSize,
            int recordsTotal, 
            int recordsFiltered,
            string searchValue = ""
        )
        {
            Data = data;

            RecordsTotal = recordsTotal;
            RecordsFiltered = recordsFiltered;
            CurrentPage = currentPage != 0 ? currentPage : 1;
            PageSize = pageSize > 100 ? 100 : pageSize;
            TotalPages = (int) Math.Ceiling((recordsTotal/(double) pageSize));
            SearchValue = searchValue;
        }

        public int SortColumn { get; set; }
        public string SortColumnDirection { get; set; }
        public string SearchValue { get; set; }
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
