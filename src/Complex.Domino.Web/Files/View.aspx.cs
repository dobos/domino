using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Files
{
    public partial class View : System.Web.UI.Page
    {
        public static string GetUrl(string fileName)
        {
            return String.Format("~/Files/View.aspx?file={0}", HttpUtility.UrlEncode(fileName));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}