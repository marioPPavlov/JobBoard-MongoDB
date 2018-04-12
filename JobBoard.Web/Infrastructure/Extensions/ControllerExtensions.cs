using JobBoard.Web.Areas.Home.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Reflection;

namespace JobBoard.Web.Infrastructure.Extensions
{
    public static class ControllerExtensions
    {
        #region RedirectionToAction
        public static IActionResult RedirectToAction<TController>(this Controller currentController, string actionName)
        {
            return currentController.RedirectToAction<TController>(actionName, null);
        }

        public static IActionResult RedirectToAction<TController>(this Controller currentController, string actionName, string id)
        {
            string controllerName = typeof(TController).Name.Replace("Controller", String.Empty);
            var area = typeof(TController).GetTypeInfo().GetCustomAttribute<AreaAttribute>().RouteValue;
            return currentController.RedirectToAction(actionName, controllerName, new { id, area });
        }
        #endregion

        #region RedirectionToLocal
        public static IActionResult RedirectToLocal(this Controller currentController, string returnUrl)
        {
            if (currentController.Url.IsLocalUrl(returnUrl))
            {
                return currentController.Redirect(returnUrl);
            }
            else
            {
                return currentController.RedirectToAction<HomeController>(nameof(HomeController.Index));
            }
        }
        #endregion
    }
}
