using Microsoft.AspNetCore.Mvc;
using System;

namespace JobBoard.Web.Infrastructure.Extensions
{
    public static class ControllerExtensions
    {

        public static IActionResult RedirectToAction<TController>(this Controller currentController, string actionName)
        {
            string controllerName = typeof(TController).Name.Replace("Controller", String.Empty);

            return currentController.RedirectToAction(actionName, controllerName);
        }

        public static IActionResult RedirectToAction<TController>(this Controller currentController, string actionName, string id)
        {
            string controllerName = typeof(TController).Name.Replace("Controller", String.Empty);

            return currentController.RedirectToAction(actionName, controllerName, new { id });
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
