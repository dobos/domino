using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Controls
{
    public partial class EntityForm : EntityFormBase
    {
        public bool NameVisible
        {
            get { return NameRow.Visible; }
            set { NameRow.Visible = value; }
        }

        public bool DescriptionVisible
        {
            get { return DescriptionRow.Visible; }
            set { DescriptionRow.Visible = value; }
        }

        public bool OptionsVisible
        {
            get { return OptionsRow.Visible; }
            set { OptionsRow.Visible = value; }
        }

        public bool CommentsVisible
        {
            get { return CommentsRow.Visible; }
            set { CommentsRow.Visible = value; }
        }

        public override void UpdateForm(Lib.Entity item)
        {
            var ro = !item.IsExisting && !item.Access.Create || item.IsExisting && !item.Access.Update;

            Name.Text = item.Name;
            Description.Text = item.Description;
            ReadOnly.Checked = item.ReadOnly;
            Hidden.Checked = item.Hidden;
            Comments.Text = item.Comments;

            Name.ReadOnly = ro;
            Description.ReadOnly = ro;
            Comments.ReadOnly = ro;
        }

        public override void SaveForm(Lib.Entity item)
        {
            item.Name = Name.Text;
            item.Description = Description.Text;
            item.ReadOnly = ReadOnly.Checked;
            item.Hidden = Hidden.Checked;
            item.Comments = Comments.Text;
        }
    }
}