using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace Complex.Domino.Util
{
    public static class Enum
    {
        public static string ToLocalized<T>(Type resourceType, T value)
        {
            string localized = null;
            string item = value.ToString();

            if (resourceType != null)
            {
                var prop = resourceType.GetProperty(item, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

                if (prop != null)
                {
                    localized = (string)prop.GetValue(null);
                }
            }

            return localized ?? item;
        }
    }
}