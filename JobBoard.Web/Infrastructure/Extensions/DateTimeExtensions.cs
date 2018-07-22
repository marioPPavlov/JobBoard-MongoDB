using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Web.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToShort(this DateTime dt)
        {
            string formattedDateTime = dt.ToString($"dd\\/MM\\/yyyy");

            return formattedDateTime; 
        }
    }
}
