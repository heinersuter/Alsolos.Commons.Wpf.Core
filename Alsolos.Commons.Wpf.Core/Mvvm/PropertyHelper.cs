using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Alsolos.Commons.Wpf.Core.Mvvm
{
    public static class PropertyHelper
    {
        public static string GetName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            if (!(propertyExpression.Body is MemberExpression memberExpression))
            {
                throw new ArgumentException(@"The expression is not a member access expression.", nameof(propertyExpression));
            }

            if (!(memberExpression.Member is PropertyInfo))
            {
                throw new ArgumentException(@"The member access expression does not access a property.", nameof(propertyExpression));
            }

            return memberExpression.Member.Name;
        }

        public static T CreateIfNeeded<T>(ref T field, Func<T> newInstanceCreateMethod)
            where T : class
        {
            if (newInstanceCreateMethod == null)
            {
                return null;
            }

            return field ??= newInstanceCreateMethod.Invoke();
        }
    }
}