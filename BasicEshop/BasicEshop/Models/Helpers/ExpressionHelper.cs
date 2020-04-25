using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BasicEshop.Models.Helpers
{
    public static class ExpressionHelper
    {
        public static PropertyInfo GetPropertyInfo<T, TP>(Expression<Func<T, TP>> expression)
        {
            var memberExpression = (MemberExpression) expression.Body;
            return (PropertyInfo) memberExpression.Member;
        }
    }
}