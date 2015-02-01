using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Teacher
{
    public partial class Assignments : PageBase
    {
        public static string GetUrl()
        {
            return "~/Teacher/Assignments.aspx";
        }

        public static string GetUrl(int courseId)
        {
            return String.Format(
                "~/Teacher/Assignments.aspx?{0}={1}",
                Constants.RequestID, courseId);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

    }
}