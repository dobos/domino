using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Student
{
    public partial class Submission : EntityForm<Lib.Submission>
    {

        protected int AssignmentID
        {
            get { return Util.UrlParser.ParseInt(Request.QueryString[Constants.RequestAssignmentID]); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            fileBrowser.BasePath = Lib.DominoConfiguration.Instance.ScratchPath;
        }
    }
}