using MongoDB.Bson;
using System;

namespace JobBoard.Common.Extensions
{
    public static class StringExtensions
    {
        public static ObjectId ToObjectId(this string value)
        {
            ObjectId.TryParse(value, out ObjectId objectId);
            return objectId;
        }

        public static string GetEmailName(this string email)
        {
            int index = email.IndexOf('@');

            string emailName = email.Substring(0, index);
            return emailName;
        }

        public static string ToString(this DateTime dt)
        {
            string format = dt.ToString($"dd\\/MM\\/yyyy");

            return format;
        }
    }
}
