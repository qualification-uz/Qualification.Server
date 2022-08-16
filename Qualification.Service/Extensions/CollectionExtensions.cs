using Newtonsoft.Json;
using Qualification.Domain.Configurations;
using Qualification.Service.Helpers;

namespace Qualification.Service.Extensions;

public static class CollectionExtensions
{
    public static IEnumerable<T> ToPagedList<T>(this IEnumerable<T> source, PaginationParams @params)
    {
        var metaData = new PaginationMetaData(source.Count(), @params);

        var json = JsonConvert.SerializeObject(metaData);

        if (HttpContextHelper.ResponseHeaders.ContainsKey("X-Pagination"))
            HttpContextHelper.ResponseHeaders.Remove("X-Pagination");

        HttpContextHelper.ResponseHeaders.Add("X-Pagination", json);

        return @params.PageSize > 0 && @params.PageIndex >= 0
            ? source.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize)
            : source;
    }

    public static IEnumerable<T> ToPagedList<T>(this IQueryable<T> source, PaginationParams @params)
    {
        var metaData = new PaginationMetaData(source.Count(), @params);

        var json = JsonConvert.SerializeObject(metaData);

        if (HttpContextHelper.ResponseHeaders.ContainsKey("X-Pagination"))
            HttpContextHelper.ResponseHeaders.Remove("X-Pagination");

        HttpContextHelper.ResponseHeaders.Add("X-Pagination", json);

        return @params.PageSize > 0 && @params.PageIndex >= 0
            ? source.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize)
            : source;
    }
}