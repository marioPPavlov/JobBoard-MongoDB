using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace JobBoard.Web.Infrastructure.Extensions
{
    public static class ControllerExtensions
    {

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

        public static IActionResult ViewOrNotFound(this Controller controller, object model)
        {
            if(model == null)
            {
                return controller.NotFound();
            }
            return controller.View(model);
        }
    }

}
