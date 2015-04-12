using System;
using System.Linq;
using System.Linq.Expressions;

namespace StickingPlace
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Allows you to sort (in ascending) an IQueryable by a string literal of a property name
        /// </summary>
        /// <remarks>
        /// Go here for info:
        /// https://social.msdn.microsoft.com/Forums/en-US/3e3ae9c2-96ba-4569-946b-357251225650/linq-to-sql-orderby-with-reflection?forum=linqtosql
        /// </remarks>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            var x = Expression.Parameter(source.ElementType, "x");
            var selector = Expression.Lambda(Expression.PropertyOrField(x, propertyName), x);

            //The above is equivalent to:
            //x => x.{propertyName}
            //where {propertyName} is a given string

            //take the selector and create a new expression which calls OrderBy passing the above expression body to it
            return source.Provider.CreateQuery<T>(Expression.Call(typeof(Queryable), "OrderBy", new Type[] { source.ElementType, selector.Body.Type }, source.Expression, selector));
        }

        /// <summary>
        /// Allows you to sort (in descending) an IQueryable by a string literal of a property name
        /// </summary>
        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            var x = Expression.Parameter(source.ElementType, "x");
            var selector = Expression.Lambda(Expression.PropertyOrField(x, propertyName), x);

            //The above is equivalent to:
            //x => x.{propertyName}
            //where {propertyName} is a given string

            //take the selector and create a new expression which calls OrderByDescending passing the above expression body to it
            return source.Provider.CreateQuery<T>(Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { source.ElementType, selector.Body.Type }, source.Expression, selector));
        }
    }
}