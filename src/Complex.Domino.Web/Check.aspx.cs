using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Complex.Domino;

namespace Complex.Domino.Web
{
    public partial class Check : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RunChecks()
        {
            try
            {
                Page.Response.Output.WriteLine("Sending e-mail to {0}...", Lib.DominoConfiguration.Instance.EmailFromAddress);

                Util.Email.SendFromDomino(
                    Lib.DominoConfiguration.Instance.EmailFromName,
                    Lib.DominoConfiguration.Instance.EmailFromAddress,
                    "Domino Test",
                    "Test");

                Page.Response.Output.WriteLine("OK");
            }
            catch (Exception ex)
            {
                Page.Response.Output.WriteLine("Error: {0}", ex.Message);
                
            }
        }
    }
}