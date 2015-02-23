using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Plugins
{
    public abstract class PluginControlBase : Web.UserControlBase
    {
        private int id;
        private int semesterID;
        private int courseID;
        private int assignmentID;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int SemesterID
        {
            get { return semesterID; }
            set { semesterID = value; }
        }

        public int CourseID
        {
            get { return courseID; }
            set { courseID = value; }
        }

        public int AssignmentID
        {
            get { return assignmentID; }
            set { assignmentID = value; }
        }

        protected PluginControlBase()
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.id = -1;
            this.semesterID = -1;
            this.courseID = -1;
            this.assignmentID = -1;
        }
    }
}
