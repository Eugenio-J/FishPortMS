namespace FishPortMS.Server.Helper
{
    public static class GetClientIP
    {
        public static string GetClientIpAddress(HttpContext context)
        {
            return context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        }
    }
}
