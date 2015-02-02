using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Teacher
{
    public partial class Submissions : PageBase
    {
        public static string GetUrl(int assigmentId, int studentId)
        {
            return String.Format(
                "~/Teacher/Submissions.aspx?{0}={1}&{2}={3}",
                Constants.RequestAssignmentID, assigmentId,
                Constants.RequestUserID, studentId);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

    }
}