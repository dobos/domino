using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Teacher
{
    public partial class SemesterList : UserControlBase
    {
        private Lib.SemesterFactory searchObject;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (semesterList.Visible)
            {
                semesterList.DataBind();
            }
        }

        protected void courseDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            searchObject = new Lib.SemesterFactory(DatabaseContext);

            e.ObjectInstance = searchObject;
        }
    }
}