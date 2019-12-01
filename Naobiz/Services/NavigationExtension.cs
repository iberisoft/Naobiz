using Microsoft.AspNetCore.Components;
using System;
using System.Reflection;
using System.Web;

namespace Naobiz.Services
{
    static class NavigationExtension
    {
        public static void ParseQueryString(this NavigationManager navigationManager, object target)
        {
            var query = HttpUtility.ParseQueryString(new Uri(navigationManager.Uri).Query);
            var targetType = target.GetType();
            foreach (string key in query.Keys)
            {
                var property = targetType.GetProperty(key, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (property != null)
                {
                    property.SetValue(target, query[key]);
                }
            }
        }
    }
}
