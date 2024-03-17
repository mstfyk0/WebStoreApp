namespace StoreApp.Infrastructure.Extensions
{
    public static class HttpRequestExtension
    {
        public static string PathAndRequery(this HttpRequest request)
        {
            return request.QueryString.HasValue ? $"{request.Path}{request.QueryString}"
            :request.Path.ToString();
        }
    }
}