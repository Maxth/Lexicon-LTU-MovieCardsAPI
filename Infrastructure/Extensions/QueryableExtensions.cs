using System;
using System.Linq.Expressions;

namespace Infrastructure.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> CustomOrderBy<T>(
        this IQueryable<T> source,
        IEnumerable<string> sortOptions
    )
    {
        var expression = source.Expression;
        int count = 0;
        foreach (var item in sortOptions)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var selector = Expression.PropertyOrField(parameter, item.Split("_")[0]);
            var method = item.Contains("desc", StringComparison.OrdinalIgnoreCase)
                ? (count == 0 ? "OrderByDescending" : "ThenByDescending")
                : (count == 0 ? "OrderBy" : "ThenBy");
            expression = Expression.Call(
                typeof(Queryable),
                method,
                new Type[] { source.ElementType, selector.Type },
                expression,
                Expression.Quote(Expression.Lambda(selector, parameter))
            );
            count++;
        }
        return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;
    }
}
