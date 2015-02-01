using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Teacher
{
    public partial class Courses : PageBase
    {
        public static string GetUrl()
        {
            return "~/Teacher/Courses.aspx";
        }

        public static string GetUrl(int semesterID)
        {
            return String.Format(
                "~/Teacher/Courses.aspx?{0}={1}",
                Constants.RequestSemesterID, semesterID);
        }
    }
}