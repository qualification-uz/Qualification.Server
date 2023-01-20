using Microsoft.AspNetCore.Http;

namespace Qualification.Service.Helpers;

public class HttpContextHelper
{
    public static IHttpContextAccessor Accessor;
    public static HttpContext Context => Accessor.HttpContext;
    public static IHeaderDictionary ResponseHeaders => Context?.Response?.Headers!;
    public static long? UserId => long.Parse(Context?.User?.FindFirst("Id")?.Value ?? "0");
    public static string Role => Context?.User?.FindFirst("Role")?.Value;
}