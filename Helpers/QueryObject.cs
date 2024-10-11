namespace peace_api.Helpers
{
    public class QueryObject
    {
        // filter params
        public string? term { get; set; } = null;

        // pagination params
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 8;
    }
}