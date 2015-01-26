using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Complex.Domino.Web.Controls
{
    public class DateTimeLabel : UserControl
    {
        public DateTime Value
        {
            get { return (DateTime)(ViewState["Value"] ?? DateTime.Now); }
            set { ViewState["Value"] = value; }
        }

        public string ExpiredCssClass
        {
            get { return (string)ViewState["ExpiredCssClass"]; }
            set { ViewState["ExpiredCssClass"] = value; }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteBeginTag("span");

            if (Value < DateTime.Now && ExpiredCssClass != null)
            {
                writer.WriteAttribute("class", ExpiredCssClass);
            }

            writer.Write(Value.ToString());

            writer.WriteEndTag("span");
        }
    }
}