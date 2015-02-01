using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Complex.Domino.Web.Teacher
{
    public partial class Submissions : PageBase
    {
        public static string GetUrl(int courseId)
        {
            return GetUrl(courseId, -1);
        }

        public static string GetUrl(int courseId, int assignmentId)
        {
            var url = "~/Teacher/Submissions.aspx";
            url += String.Format("?{0}={1}", Constants.RequestCourseID, courseId);

            if (assignmentId > 0)
            {
                url += String.Format("&{0}={1}", Constants.RequestAssignmentID, assignmentId);
            }

            return url;
        }

        private HtmlTable table;

        protected int CourseID
        {
            get { return int.Parse(Request.QueryString[Constants.RequestCourseID]); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefreshForm();
        }

        private void RefreshForm()
        {
            var csf = new Lib.CourseSpreadsheetFactory(DatabaseContext)
            {
                CourseID = this.CourseID,
            };

            csf.FindStudents(-1, -1, "Name");
            csf.FindAssignments();
            csf.FindSubmissions(-1, -1, "Name");

            HtmlTableRow tr;
            HtmlTableCell td;

            table = new HtmlTable();

            // Header line
            tr = new HtmlTableRow();

            // -- corner
            td = new HtmlTableCell();
            tr.Cells.Add(td);

            for (int ai = 0; ai < csf.Assignments.Count; ai++)
            {
                td = new HtmlTableCell();
                td.InnerText = csf.Assignments[ai].Name;
                tr.Cells.Add(td);
            }

            table.Rows.Add(tr);

            // Data lines

            for (int si = 0; si < csf.Students.Count; si++)
            {
                tr = new HtmlTableRow();

                // User name
                td = new HtmlTableCell()
                {
                    InnerText = csf.Students[si].Name,
                };
                tr.Cells.Add(td);

                for (int ai = 0; ai < csf.Assignments.Count; ai++)
                {
                    td = new HtmlTableCell();

                    if (csf.Submissions[si][ai] != null)
                    {
                        td.InnerText = csf.Submissions[si][ai].Count.ToString();
                    }

                    tr.Cells.Add(td);
                }

                table.Rows.Add(tr);
            }

            tablePlaceholder.Controls.Add(table);
        }
    }
}