namespace APLMatchMaker.Client.Helpers
{
    public class PaginationMetadata
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        private string? _previousPageLink;
        public string? PreviousPageLink { get => _previousPageLink; set => _previousPageLink = RemoveBeforeApi(value!); }
        private string? _nextPageLink;
        public string? NextPageLink { get => _nextPageLink; set => _nextPageLink = RemoveBeforeApi(value!); }


        private static string? RemoveBeforeApi(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return null;
            var position = url.IndexOf("api/");
            return url.Substring(position);
        }
    }
}