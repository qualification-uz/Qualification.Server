using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Qualification.Domain.Configurations;
using Qualification.Service.DTOs;
using Qualification.Service.Helpers;
using System.Linq;
using System.Linq.Expressions;

namespace Qualification.Service.Extensions;

public static class CollectionExtensions
{
    public static IEnumerable<T> ToPagedList<T>(this IEnumerable<T> source, PaginationParams @params)
    {
        if(@params is null)
        {
            return source;
        }

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
    
    public static void Shuffle<T> (this Random random, T[] array)
    {
        int length = array.Length;
        while (length > 1) 
        {
            int k = random.Next(length--);
            T temp = array[length];
            array[length] = array[k];
            array[k] = temp;
        }
    }

    public static IQueryable<T> Filter<T>(this IQueryable<T> source, Filter filter)
    {
        if (filter is null ||
            filter.Property is null ||
            filter.Value is null)
        {
            return source;
        }

        return source
            .Where(BuildPredicate<T>(filter.Property, filter.Value));
    }

    private static Expression<Func<T, bool>> BuildPredicate<T>(string propertyName, string value)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var left = propertyName.Split('.').Aggregate((Expression)parameter, Expression.Property);
        var body = MakeBinary(ExpressionType.Equal, left, value);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    private static Expression MakeBinary(ExpressionType type, Expression left, string value)
    {
        object typedValue = value.ToString();
        if (left.Type != typeof(string))
        {
            if (string.IsNullOrEmpty(value))
            {
                typedValue = null;
                if (Nullable.GetUnderlyingType(left.Type) == null)
                    left = Expression.Convert(left, typeof(Nullable<>).MakeGenericType(left.Type));
            }
            else
            {
                var valueType = Nullable.GetUnderlyingType(left.Type) ?? left.Type;
                typedValue = valueType.IsEnum ? Enum.Parse(valueType, value) :
                    valueType == typeof(Guid) ? Guid.Parse(value) :
                    Convert.ChangeType(value, valueType);
            }
        }
        var right = Expression.Constant(typedValue, left.Type);
        return Expression.MakeBinary(type, left, right);
    }
}