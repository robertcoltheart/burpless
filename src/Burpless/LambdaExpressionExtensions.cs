using System.Linq.Expressions;
using System.Reflection;

namespace Burpless;

internal static class LambdaExpressionExtensions
{
    public static string GetName(this LambdaExpression lambda)
    {
        if (lambda.Body is MemberExpression memberExpression)
        {
            if (memberExpression.Member is PropertyInfo property)
            {
                return property.Name;
            }

            if (memberExpression.Member is FieldInfo field)
            {
                return field.Name;
            }
        }

        if (lambda.Body is MethodCallExpression methodCallExpression)
        {
            return methodCallExpression.Method.Name;
        }

        throw new ArgumentException("Expression should be a method, property or field");
    }
}
