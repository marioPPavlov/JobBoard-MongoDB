using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Web.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string GetControllerName(this string controller)
        {
            return controller.Replace("Controller", string.Empty);
        }
    }
}
