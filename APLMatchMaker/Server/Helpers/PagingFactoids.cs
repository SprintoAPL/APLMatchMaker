namespace APLMatchMaker.Server.Helpers
{
    public class PagingFactoids
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => (CurrentPage > 1);
        public bool HasNext => (CurrentPage < TotalPages);


        public PagingFactoids(int currentPage, int totalPages, int totalCount, int pageSize)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
            TotalCount = totalCount;
            PageSize = pageSize;
        }
    }
}
