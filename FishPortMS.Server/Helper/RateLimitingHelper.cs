namespace FishPortMS.Server.Helper
{
    public static class RateLimitingHelper
    {
        public static string GetClientIp(HttpContext context)
        {
            // Use X-Forwarded-For if behind proxy like NGINX or Cloudflare
            var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            return !string.IsNullOrWhiteSpace(forwardedFor)
                ? forwardedFor.Split(',')[0]
                : context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        }
    }
}
