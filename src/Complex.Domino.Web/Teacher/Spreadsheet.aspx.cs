using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

            var table = new Table();
            table.CssClass = "spreadsheet";

            table.Rows.Add(GenerateHeaderRow1(sf));
            table.Rows.Add(GenerateHeaderRow2(sf));

            for (int si = 0; si < sf.Students.Count; si++)
            {
                table.Rows.Add(GenerateStudentRow(sf, si));
            }

            tablePlaceholder.Controls.Add(table);
        }

        private TableRow GenerateHeaderRow1(Lib.SpreadsheetFactory sf)
        {
            TableRow tr;
            TableCell td;

            tr = new TableRow();

            // -- corner
            td = new TableCell()
            {
                ColumnSpan = 2
            };

            tr.Cells.Add(td);

            for (int ai = 0; ai < sf.Assignments.Count; ai++)
            {
                td = new TableCell()
                {
                    ColumnSpan = 3,
                    Text = sf.Assignments[ai].Name
                };

                tr.Cells.Add(td);
            }

            return tr;
        }

        private TableRow GenerateHeaderRow2(Lib.SpreadsheetFactory sf)
        {
            TableRow tr;
            TableCell td;

            tr = new TableRow();

            td = new TableCell();
            td.Text = Resources.Labels.UserName;
            tr.Cells.Add(td);

            td = new TableCell();
            td.Text = Resources.Labels.Name;
            tr.Cells.Add(td);

            for (int ai = 0; ai < sf.Assignments.Count; ai++)
            {
                td = new TableCell();
                td.Text = Resources.Labels.First.ToLower();
                tr.Cells.Add(td);

                td = new TableCell();
                td.Text = Resources.Labels.Last.ToLower();
                tr.Cells.Add(td);

                td = new TableCell();
                td.Text = Util.Enum.ToLocalized(typeof(Resources.Grades), sf.Assignments[ai].GradeType).ToLower();
                tr.Cells.Add(td);
            }

            return tr;
        }

        private TableRow GenerateStudentRow(Lib.SpreadsheetFactory sf, int si)
        {
            TableRow tr;
            TableCell td;

            tr = new TableRow();


            // User name
            td = new TableCell()
            {
                Text = sf.Students[si].Name,
                CssClass = "name",
            };

            tr.Cells.Add(td);

            // Name
            td = new TableCell()
            {
                Text = sf.Students[si].Description,
                CssClass = "desc"
            };

            tr.Cells.Add(td);

            for (int ai = 0; ai < sf.Assignments.Count; ai++)
            {
                tr.Cells.AddRange(GenerateSubmissionCells(sf, si, ai));
            }

            return tr;
        }

        private TableCell[] GenerateSubmissionCells(Lib.SpreadsheetFactory sf, int si, int ai)
        {
            var td = new TableCell[3];

            for (int i = 0; i < td.Length; i++)
            {
                td[i] = new TableCell()
                {
                    CssClass = "item"
                };
            }

            var aa = sf.Assignments[ai];
            var ss = sf.Submissions[si][ai];

            if (ss != null)
            {
                if (ss.Count > 0)
                {
                    var s = ss[0];                  // First submission

                    var a = new HyperLink()
                    {
                        NavigateUrl = "",
                        Text = s.CreatedDate.ToString(Resources.DateTime.MonthDayFormat),
                    };

                    td[0].Controls.Add(a);

                    // Late

                    if (s.CreatedDate > aa.EndDateSoft)
                    {
                        td[0].CssClass += " late";
                    }

                    // Unread
                }

                if (ss.Count > 1)
                {
                    var s = ss[ss.Count - 1];       // Last submission

                    var a = new HyperLink()
                    {
                        NavigateUrl = "",
                        Text = s.CreatedDate.ToString(Resources.DateTime.MonthDayFormat)
                    };

                    td[1].Controls.Add(a);

                    // Unread
                }
            }

            return td;
        }
    }
}