namespace APLMatchMaker.Client.Helpers
{
    public class PaginationMetadata
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        private string? previousPageLink;
        public string? PreviousPageLink { get => previousPageLink; set => previousPageLink = RemoveBeforeApi(value!); }
        private string? nextPageLink;
        public string? NextPageLink { get => nextPageLink; set => nextPageLink = RemoveBeforeApi(value!); }


        private static string? RemoveBeforeApi(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return null;
            var position = url.IndexOf("api/");
            return url.Substring(position);
        }
    }
}