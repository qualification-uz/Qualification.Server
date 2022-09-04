using Newtonsoft.Json;
using Qualification.Domain.Configurations;
using Qualification.Service.DTOs;
using Qualification.Service.Helpers;
using System.Linq.Expressions;

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
        HttpContextHelper.ResponseHeaders.Add("Access-Control-Expose-Headers", "X-Pagination");

        return @params.PageSize > 0 && @params.PageNumber >= 0
            ? source.Skip((@params.PageNumber - 1) * @params.PageSize).Take(@params.PageSize)
            : source;
    }

    public static IEnumerable<T> ToPagedList<T>(this IQueryable<T> source, PaginationParams @params)
    {
        var metaData = new PaginationMetaData(source.Count(), @params);

        var json = JsonConvert.SerializeObject(metaData);

        if (HttpContextHelper.ResponseHeaders.ContainsKey("X-Pagination"))
            HttpContextHelper.ResponseHeaders.Remove("X-Pagination");

        HttpContextHelper.ResponseHeaders.Add("X-Pagination", json);
        HttpContextHelper.ResponseHeaders.Add("Access-Control-Expose-Headers", "X-Pagination");

        return @params.PageSize > 0 && @params.PageNumber >= 0
            ? source.Skip((@params.PageNumber - 1) * @params.PageSize).Take(@params.PageSize)
            : source;
    }

    public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, Filter filter)
    {
        var expression = source.Expression;

        var parameter = Expression.Parameter(typeof(T), "x");
        var selector = Expression.PropertyOrField(parameter, filter?.OrderBy ?? "Id");
        
        var method = string.Equals(filter?.OrderType ?? "asc", "desc", StringComparison.OrdinalIgnoreCase) ? "OrderByDescending" : "OrderBy";
        
        expression = Expression.Call(typeof(Queryable), method,
            new Type[] { source.ElementType, selector.Type },
            expression, Expression.Quote(Expression.Lambda(selector, parameter)));

        return source.Provider.CreateQuery<T>(expression);
    }
}