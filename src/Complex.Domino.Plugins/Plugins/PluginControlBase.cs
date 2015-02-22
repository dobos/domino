using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Plugins
{
    public abstract class PluginControlBase
    {
        private int id;
        private int semesterID;
        private int courseID;
        private int assignmentID;

        public int ID { get; set; }

        public int SemesterID { get; set; }

        public int CourseID { get; set; }

        public int AssignmentID { get; set; }
    }
}
