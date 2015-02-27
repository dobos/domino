using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Web
{
    public abstract class EntityFormBase : System.Web.UI.UserControl
    {
        public abstract void UpdateForm(Lib.Entity item);

        public abstract void SaveForm(Lib.Entity item);
    }
}
