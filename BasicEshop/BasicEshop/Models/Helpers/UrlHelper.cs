using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BasicEshop.Models.Helpers
{
    public static class UrlHelper
    {
        public static Dictionary<string, string> SingleSelection<T, TP>(
            T model,
            Expression<Func<T, TP>> expression,
            string value
        ) where T : ICastToStringDictionary
        {
            var prop = ExpressionHelper.GetPropertyInfo(expression);
            object? oldValue = prop.GetValue(model);

            prop.SetValue(model, value);
            var data = model.ToStringDictionary();
            prop.SetValue(model, oldValue);

            return data;
        }

        public static Dictionary<string, string> MultiSelection<T, TP>(
            T model,
            Expression<Func<T, TP>> expression,
            string value
        ) where T : ICastToStringDictionary
        {
            var prop = ExpressionHelper.GetPropertyInfo(expression);
            object? oldValue = prop.GetValue(model);

            List<string> values = oldValue?.ToString()?.Split(",").ToList() ?? new List<string>();
            if (values.Contains(value))
            {
                values.Remove(value);
            }
            else
            {
                values.Add(value);
            }

            var newValue = String.Join(',', values);

            prop.SetValue(model, newValue);
            var data = model.ToStringDictionary();
            prop.SetValue(model, oldValue);

            return data;
        }

        public static bool IsSelected(string? values, string value)
        {
            var valueList = values?.Split(",").ToList() ?? new List<string>();
            return valueList.Contains(value);
        }
    }
}