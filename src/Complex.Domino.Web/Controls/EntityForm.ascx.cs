using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Controls
{
    public partial class EntityForm : System.Web.UI.UserControl
    {
        public void UpdateForm(Lib.Entity item)
        {
            Name.Text = item.Name;
            Description.Text = item.Description;
            Enabled.Checked = item.Enabled;
            Visible.Checked = item.Visible;
            Comments.Text = item.Comments;
        }

        public void SaveForm(Lib.Entity item)
        {
            item.Name = Name.Text;
            item.Description = Description.Text;
            item.Enabled = Enabled.Checked;
            item.Visible = Visible.Checked;
            item.Comments = Comments.Text;
        }
    }
}