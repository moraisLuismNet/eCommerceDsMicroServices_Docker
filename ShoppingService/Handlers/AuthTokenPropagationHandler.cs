using System.Net.Http.Headers;

namespace ShoppingService.Handlers
{
    public class AuthTokenPropagationHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthTokenPropagationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // Check for HTTP context and token
            if (_httpContextAccessor.HttpContext != null)
            {
                var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"]
                    .FirstOrDefault()?.Replace("Bearer ", "");

                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
