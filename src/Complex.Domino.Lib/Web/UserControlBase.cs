using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Complex.Domino.Lib;

namespace Complex.Domino.Web
{
    public class UserControlBase : UserControl
    {

        protected Context DatabaseContext
        {
            get { return ((PageBase)Page).DatabaseContext; }
        }

        public string SessionGuid
        {
            get { return ((PageBase)Page).SessionGuid; }
        }
    }
}