using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;

namespace JobBoard.Web.Infrastructure
{
    public class ViewLocationExpander : IViewLocationExpander
    {

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
                var viewLocationFormats = new[]
                {
                    "/Areas/{2}/Views/{1}/{0}.cshtml",
                    "/Areas/{2}/Views/{1}/Partial/{0}.cshtml",
                    "/Areas/{2}/Views/Shared/{0}.cshtml",        
                    "/Areas/{2}/Views/Shared/Partial/{0}.cshtml",
                };
                return viewLocationFormats;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["customviewlocation"] = nameof(ViewLocationExpander); 
            
        }
    }
}
