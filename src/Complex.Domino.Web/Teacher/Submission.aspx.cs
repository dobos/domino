using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Teacher
{
    public partial class Submission : SubmissionPage
    {
        public static string GetUrl(int assignmentID)
        {
            return GetUrl(assignmentID, -1);
        }

        public static string GetUrl(int assignmentID, int submissionID)
        {
            var url = "~/Teacher/Submission.aspx";
            var query = "";

            if (assignmentID > 0)
            {
                query += "&assignmentID=" + assignmentID.ToString();
            }

            if (submissionID > 0)
            {
                query += "&id=" + submissionID.ToString();
            }

            if (query.Length > 0)
            {
                url += "?" + query.Substring(1);
            }

            return url;
        } 
    }
}