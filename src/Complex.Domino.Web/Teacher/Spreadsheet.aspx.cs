using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Complex.Domino.Web.Teacher
{
    public partial class Spreadsheet : PageBase
    {
        public static string GetUrl(int courseId)
        {
            return GetUrl(courseId, -1);
        }

        public static string GetUrl(int courseId, int assignmentId)
        {
            var url = "~/Teacher/Spreadsheet.aspx";
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
            var sf = new Lib.SpreadsheetFactory(DatabaseContext)
            {
                CourseID = this.CourseID,
            };


            // TODO: move these into a single function on the factory class
            sf.FindStudents(-1, -1, "Name");
            sf.FindAssignments();
            sf.FindSubmissions(-1, -1, "Name");

            table = new HtmlTable();
            table.Attributes.Add("class", "spreadsheet");

            table.Rows.Add(GenerateHeaderRow1(sf));
            table.Rows.Add(GenerateHeaderRow2(sf));

            for (int si = 0; si < sf.Students.Count; si++)
            {
                table.Rows.Add(GenerateStudentRow(sf, si));
            }

            tablePlaceholder.Controls.Add(table);
        }

        private HtmlTableRow GenerateHeaderRow1(Lib.SpreadsheetFactory sf)
        {
            HtmlTableRow tr;
            HtmlTableCell td;

            tr = new HtmlTableRow();

            // -- corner
            td = new HtmlTableCell()
            {
                ColSpan = 2
            };
            tr.Cells.Add(td);

            for (int ai = 0; ai < sf.Assignments.Count; ai++)
            {
                td = new HtmlTableCell()
                {
                    ColSpan = 3,
                    InnerText = sf.Assignments[ai].Name
                };

                tr.Cells.Add(td);
            }

            return tr;
        }

        private HtmlTableRow GenerateHeaderRow2(Lib.SpreadsheetFactory sf)
        {
            HtmlTableRow tr;
            HtmlTableCell td;

            tr = new HtmlTableRow();

            td = new HtmlTableCell();
            td.InnerText = Resources.Labels.UserName;
            tr.Cells.Add(td);

            td = new HtmlTableCell();
            td.InnerText = Resources.Labels.Name;
            tr.Cells.Add(td);

            for (int ai = 0; ai < sf.Assignments.Count; ai++)
            {
                td = new HtmlTableCell();
                td.InnerText = Resources.Labels.First.ToLower();
                tr.Cells.Add(td);

                td = new HtmlTableCell();
                td.InnerText = Resources.Labels.Last.ToLower();
                tr.Cells.Add(td);

                td = new HtmlTableCell();
                td.InnerText = Util.Enum.ToLocalized(typeof(Resources.Grades), sf.Assignments[ai].GradeType).ToLower();
                tr.Cells.Add(td);
            }

            return tr;
        }

        private HtmlTableRow GenerateStudentRow(Lib.SpreadsheetFactory sf, int si)
        {
            HtmlTableRow tr;
            HtmlTableCell td;

            tr = new HtmlTableRow();


            // User name
            td = new HtmlTableCell();
            td.InnerText = sf.Students[si].Name;
            td.Attributes.Add("class", "name");
            tr.Cells.Add(td);

            // Name
            td = new HtmlTableCell();
            td.InnerText = sf.Students[si].Description;
            td.Attributes.Add("class", "desc");
            tr.Cells.Add(td);

            for (int ai = 0; ai < sf.Assignments.Count; ai++)
            {
                var tds = GenerateSubmissionCells(sf, si, ai);

                for (int i = 0; i < tds.Length; i++)
                {
                    tr.Cells.Add(tds[i]);
                }
            }

            return tr;
        }

        private HtmlTableCell[] GenerateSubmissionCells(Lib.SpreadsheetFactory sf, int si, int ai)
        {
            var td = new HtmlTableCell[3];

            for (int i = 0; i < td.Length; i ++)
            {
                td[i] = new HtmlTableCell();
            }

            var ss = sf.Submissions[si][ai];

            if (ss != null)
            {
                if (ss.Count > 0)
                {
                    var s = ss[0];                  // First submission

                    td[0].InnerText = s.CreatedDate.ToString(Resources.DateTime.MonthDayFormat);
                }

                if (ss.Count > 1)
                {
                    var s = ss[ss.Count - 1];       // Last submission

                    td[1].InnerText = s.CreatedDate.ToString(Resources.DateTime.MonthDayFormat);
                }
            }

            // Set CSS

            td[0].Attributes.Add("class", "item");
            td[1].Attributes.Add("class", "item");
            td[2].Attributes.Add("class", "item");

            return td;
        }
    }
}