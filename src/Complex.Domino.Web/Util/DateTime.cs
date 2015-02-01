﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Complex.Domino.Web.Util
{
    public static class DateTime
    {
        public static string FormatFancy(System.DateTime value)
        {
            var now = System.DateTime.Now;
            var today = now.Date;

            string date;
            string time;

            if (value.Date == today)
            {
                date = Resources.DateTime.Today;
            }
            else if (value.Date.AddDays(1) == today)
            {
                date = Resources.DateTime.Yesterday;
            }
            else if (value.Date.AddDays(-1) == today)
            {
                date = Resources.DateTime.Tomorrow;
            }
            else
            {
                date = value.Date.ToShortDateString();
            }

            time = value.ToShortTimeString();

            return String.Format("{0} {1}", date, time);
        }
    }
}