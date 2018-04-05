using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace JobBoard.Web.Infrastructure.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent AreaLink<TController>(this IHtmlHelper htmlHelper, string title, string actionName)
        {
            return htmlHelper.AreaLink<TController>(title, actionName, null, null);
        }

        public static IHtmlContent AreaLink<TController>(this IHtmlHelper htmlHelper, string title, string actionName, string id)
        {
            return htmlHelper.AreaLink<TController>(title, actionName, id, null);
        }

        public static IHtmlContent AreaLink<TController>(this IHtmlHelper htmlHelper, string title, string actionName, object routeValues)
        {
            return htmlHelper.AreaLink<TController>(title, actionName, null, routeValues);
        }

        public static IHtmlContent AreaLink<TController>(this IHtmlHelper htmlHelper, string title, string actionName, string id = null, object otherRouteValues = null )
        {
            var controllerName = typeof(TController).Name.Replace("Controller", String.Empty);
            var area = typeof(TController).GetTypeInfo().GetCustomAttribute<AreaAttribute>().RouteValue;

            RouteValueDictionary routeValues = new RouteValueDictionary(otherRouteValues) {};

            if(!routeValues.ContainsKey(nameof(area)))
                routeValues.Add(nameof(area), area);

            if(!routeValues.ContainsKey(nameof(id)))
                routeValues.Add(nameof(id), id);

            return htmlHelper.ActionLink(title, actionName, controllerName, routeValues);
        }
    }
}
