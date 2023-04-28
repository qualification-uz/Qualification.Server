using Microsoft.AspNetCore.Http;

namespace Qualification.Service.Helpers;

public class HttpContextHelper
{
    public static IHttpContextAccessor Accessor;
    public static HttpContext Context => Accessor.HttpContext;
    public static IHeaderDictionary ResponseHeaders => Context?.Response?.Headers!;
    public static long? UserId => long.Parse(Context?.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ?? "0");
    public static string Role => Context?.User?.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
}