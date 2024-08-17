using Market.Auth.Application.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Market.Auth.Application.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> SortFilter<T> (this IQueryable<T> query,
        BaseSortFilterDto options,params string[] searchingProperties)
    {
        if (options.HasSearch)
            query = Search(query, options, searchingProperties);
        
        //pagination
        query =
            query.Skip(options.PageSize * (options.PageSize - 1))
            .Take(options.PageSize);

        //sorting logic
        if(options.HasSort)
            query = query.OrderByProperty<T>(
                propertyName: options.SortBy, 
                descending: options.SortType.ToLower() == "desc");

        return query;
    }

    private static IQueryable<T> Search<T>(IQueryable<T> query, BaseSortFilterDto options, string[] searchingProperties)
    {
        if (options.HasSearch && searchingProperties.Length > 0)
        {
            var parameter = Expression.Parameter(typeof(T), "e");
            Expression? predicate = null;

            foreach (var property in searchingProperties)
            {
                var propertyInfo = typeof(T).GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null)
                {
                    var propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);

                    // Create the EF.Functions.Like method call expression
                    var likeMethod = typeof(DbFunctionsExtensions)
                        .GetMethod(nameof(DbFunctionsExtensions.Like),
                            new[] { typeof(DbFunctions), typeof(string), typeof(string) });
                    var likeExpression = Expression.Call(
                        likeMethod!,
                        Expression.Constant(EF.Functions), // DbFunctions instance
                        propertyAccess, // Property to search in
                        Expression.Constant($"%{options.SearchingWord}%") // Search pattern
                    );

                    if (predicate == null)
                    {
                        predicate = likeExpression;
                    }
                    else
                    {
                        predicate = Expression.OrElse(predicate, likeExpression);
                    }
                }
            }

            if (predicate != null)
            {
                var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);
                query = query.Where(lambda);
            }
        }

        return query;
    }

    private static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName, bool descending = false)
    {
        var property = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (property == null)
        {
            throw new ArgumentException($"Property '{propertyName}' does not exist on type '{typeof(T)}'.");
        }

        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);

        string methodName = descending ? "OrderByDescending" : "OrderBy";
        var method = typeof(Queryable).GetMethods()
                                       .Where(m => m.Name == methodName && m.GetParameters().Length == 2)
                                       .Single()
                                       .MakeGenericMethod(typeof(T), property.PropertyType);

        return (IQueryable<T>)method.Invoke(null, new object[] { source, orderByExpression });
    }

}
