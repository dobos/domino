using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Reflection;

namespace Complex.Domino.Web.Controls
{
    public class EnumField : BoundField
    {
        public string EnumType
        {
            get { return (string)ViewState["EnumType"]; }
            set { ViewState["EnumType"] = value; }
        }

        public string ResourceType
        {
            get { return (string)ViewState["ResourceType"]; }
            set { ViewState["ResourceType"] = value; }
        }

        public EnumField()
        {
        }

        protected override DataControlField CreateField()
        {
            return new EnumField();
        }

        protected override string FormatDataValue(object dataValue, bool encode)
        {
            string localized = null;
            string value = dataValue.ToString();

            if (ResourceType != null)
            {
                var type = Type.GetType(ResourceType);

                if (type != null)
                {
                    localized = Util.Enum.ToLocalized(type, dataValue);
                }
            }

            return base.FormatDataValue(localized ?? value, encode);
        }
    }
}