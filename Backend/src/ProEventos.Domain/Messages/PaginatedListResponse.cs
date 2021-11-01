namespace ProEventos.Domain.Messages
{
    public class PaginatedListResponse<T> : ListResponse<T> where T : class
    {
        public int SortColumn { get; set; }
        public string SortColumnDirection { get; set; }
        public string SearchValue { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }

        private int skip;
        public int Skip
        {
            get { return skip != default ? skip : 0; }
            set { skip = value; }
        }

        private int take;
        public int Take
        {
            get { return take != default ? take : 0; }
            set { take = value; }
        }
    }
}
