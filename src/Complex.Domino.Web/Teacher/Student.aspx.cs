using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Teacher
{
    public partial class Student : EntityPage<Lib.User>
    {
        public static string GetUrl(int studentId)
        {
            return String.Format(
                "~/Teacher/Student.aspx?{0}={1}",
                Constants.RequestID, studentId);
        }
    }
}