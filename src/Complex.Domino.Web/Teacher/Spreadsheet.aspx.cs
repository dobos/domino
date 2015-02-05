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

        private TableHeaderRow GenerateHeaderRow1(Lib.SpreadsheetFactory sf)
        {
            TableHeaderRow tr;
            TableHeaderCell th;

            tr = new TableHeaderRow();

            // -- corner
            th = new TableHeaderCell()
            {
                ColumnSpan = 2
            };

            tr.Cells.Add(th);

            for (int ai = 0; ai < sf.Assignments.Count; ai++)
            {
                th = new TableHeaderCell()
                {
                    ColumnSpan = 3,
                    Text = sf.Assignments[ai].Name
                };

                tr.Cells.Add(th);
            }

            return tr;
        }

        private TableHeaderRow GenerateHeaderRow2(Lib.SpreadsheetFactory sf)
        {
            TableHeaderRow tr;
            TableHeaderCell th;

            tr = new TableHeaderRow();

            th = new TableHeaderCell();
            th.Text = Resources.Labels.UserName;
            tr.Cells.Add(th);

            th = new TableHeaderCell();
            th.Text = Resources.Labels.Name;
            tr.Cells.Add(th);

            for (int ai = 0; ai < sf.Assignments.Count; ai++)
            {
                th = new TableHeaderCell();
                th.Text = Resources.Labels.First.ToLower();
                tr.Cells.Add(th);

                th = new TableHeaderCell();
                th.Text = Resources.Labels.Last.ToLower();
                tr.Cells.Add(th);

                th = new TableHeaderCell();
                th.Text = Util.Enum.ToLocalized(typeof(Resources.Grades), sf.Assignments[ai].GradeType).ToLower();
                tr.Cells.Add(th);
            }

            return tr;
        }

        private TableRow GenerateStudentRow(Lib.SpreadsheetFactory sf, int si)
        {
            TableRow tr;
            TableCell td;

            tr = new TableRow()
            {
                CssClass = "item"
            };

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
                    CssClass = "subm"
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
                        Text = s.CreatedDate.ToString(Resources.DateTime.MonthDayFormat),
                    };

                    // If this is the only submission, point link to it directly,
                    // otherwise point to a list of submissions
                    if (ss.Count == 1)
                    {
                        a.NavigateUrl = Teacher.Submission.GetUrl(aa.ID, s.ID);
                    }
                    else
                    {
                        a.NavigateUrl = Teacher.Submissions.GetUrl(aa.ID, s.StudentID);
                    }

                    td[0].Controls.Add(a);

                    if (!s.IsRead)
                    {
                        // Unread

                        td[0].CssClass += " new";
                    }
                    else if (s.CreatedDate > aa.EndDateSoft)
                    {
                        // Late

                        td[0].CssClass += " late";
                    }

                    
                }

                if (ss.Count > 1)
                {
                    // Check if any of the submissions is unread

                    bool unread = false;
                    for (int i = 1; i < ss.Count; i++)
                    {
                        unread |= !ss[i].IsRead;
                    }

                    var s = ss[ss.Count - 1];       // Last submission

                    var a = new HyperLink()
                    {
                        Text = s.CreatedDate.ToString(Resources.DateTime.MonthDayFormat),
                        NavigateUrl = Teacher.Submission.GetUrl(aa.ID, s.ID),
                    };

                    td[1].Controls.Add(a);

                    if (unread)
                    {
                        // Unread

                        td[1].CssClass += " new";
                    }
                }
            }

            return td;
        }
    }
}