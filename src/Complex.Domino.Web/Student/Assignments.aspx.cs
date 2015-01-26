using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Student
{
    public partial class Assignments : PageBase
    {
        public static string GetUrl()
        {
            return "~/Student/Assignments.aspx";
        }

        public static string GetUrl(int courseID)
        {
            return String.Format("~/Student/Assignments.aspx?courseID={0}", courseID);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

    }
}