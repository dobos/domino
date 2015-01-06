using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Admin
{
    public partial class SemesterList : PageBase
    {
        public static string GetUrl()
        {
            return "~/Admin/SemesterList.aspx";
        }

        private Lib.SemesterFactory searchObject;

        protected void Page_Load(object sender, EventArgs e)
        {
            ToolbarCreate.NavigateUrl = Semester.GetUrl();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (semesterList.Visible)
            {
                semesterList.DataBind();
            }
        }

        protected void semesterDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            searchObject = new Lib.SemesterFactory(DatabaseContext);

            // TODO: set search criteria here

            e.ObjectInstance = searchObject;
        }
    }
}