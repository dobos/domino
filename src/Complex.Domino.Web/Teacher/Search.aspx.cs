using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Complex.Domino.Git;
using Complex.Domino.Lib;

namespace Complex.Domino.Web.Teacher
{
    public partial class Search : PageBase
    {
        public static string GetUrl()
        {
            return "~/Teacher/Search.aspx";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Ok_Click(object sender, EventArgs e)
        {
            var search = new Lib.Search(DatabaseContext)
            {
                Pattern = pattern.Text
            };

            matchList.DataSource = search.Find();
            matchList.DataBind();

            matchListPanel.Visible = true;
        }

        protected void MatchList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var sm = (SearchMatch)e.Item.DataItem;

                var submission = (HyperLink)e.Item.FindControl("submission");

                submission.Text = String.Format("{1} ({0}): {2}:{3}",
                    sm.Student.Name,
                    sm.Student.Description,
                    sm.FileName,
                    sm.Line);

                submission.NavigateUrl = Submission.GetUrl(sm.Submission.AssignmentID, sm.Submission.ID);
            }
        }
    }
}