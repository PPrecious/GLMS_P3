using System.Net.Http.Headers;

namespace GLMS.Web.Services
{
    public class ApiService
    {
        private readonly IHttpClientFactory _factory;
        private readonly IHttpContextAccessor _httpContext;

        public ApiService(
            IHttpClientFactory factory,
            IHttpContextAccessor httpContext)
        {
            _factory = factory;
            _httpContext = httpContext;
        }

        public HttpClient CreateClient()
        {
            var client =
                _factory.CreateClient("GLMSApi");

            var token =
                _httpContext.HttpContext?
                .Session
                .GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        "Bearer",
                        token);
            }

            return client;
        }
    }
}