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
        #region AreaLink
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
        #endregion

        #region ViewDataLink
        public static Object GetViewDataLink<TController>(this IHtmlHelper htmlHelper, string actionName, object routeValues = null)
        {
            var controllerName = typeof(TController).Name.Replace("Controller", String.Empty);
            var area = typeof(TController).GetTypeInfo().GetCustomAttribute<AreaAttribute>().RouteValue;

            RouteValueDictionary route = new RouteValueDictionary(routeValues) {};
            route.Add("controllerName", controllerName);
            route.Add("actionName", actionName);
            route.Add("area", area);
            
            return route;
        }

        public static IHtmlContent ViewDataLink(this IHtmlHelper htmlHelper,  string title, object route, object htmlAttributes)
        {
            RouteValueDictionary routeValues = new RouteValueDictionary(route);
            var actionName = routeValues["actionName"].ToString();
            var controllerName = routeValues["controllerName"].ToString();
            routeValues.Remove("actionName");
            routeValues.Remove("controllerName");

            return htmlHelper.ActionLink(title, actionName, controllerName, routeValues, htmlAttributes);
        }

        #endregion
    }
}
