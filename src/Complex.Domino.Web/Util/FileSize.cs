using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Complex.Domino.Web.Util
{
    public static class FileSize
    {
        private const string FS_PB = "PiB";
        private const string FS_TB = "TiB";
        private const string FS_GB = "GiB";
        private const string FS_MB = "MiB";
        private const string FS_kB = "kiB";
        private const string FS_B = "B";

        private const long FL_PB = 0x4000000000000;
        private const long FL_TB = 0x10000000000;
        private const long FL_GB = 0x40000000;
        private const long FL_MB = 0x100000;
        private const long FL_kB = 0x400;

        public static string FormatFancy(long size)
        {
            double res;
            string unit;

            if (size < FL_kB)
            {
                res = size;
                unit = FS_B;
            }
            else if (size >= FL_kB && size < FL_MB)
            {
                res = (double)size / FL_kB;
                unit = FS_kB;
            }
            else if (size >= FL_MB && size < FL_GB)
            {
                res = (double)size / FL_MB;
                unit = FS_MB;
            }
            else if (size >= FL_GB && size < FL_TB)
            {
                res = (double)size / FL_GB;
                unit = FS_GB;
            }
            else if (size >= FL_TB && size < FL_PB)
            {
                res = (double)size / FL_TB;
                unit = FS_TB;
            }
            else
            {
                res = (double)size / FL_PB;
                unit = FS_PB;
            }

            return String.Format("{0:0.###} {1}", res, unit);

        }
    }
}