using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
    public class CourseSpreadsheetFactory : ContextObject
    {
        private int courseId;
        private int assignmentId;

        private List<User> students;
        private Dictionary<int, int> studentIds;
        private List<Assignment> assignments;
        private Dictionary<int, int> assignmentIds;
        private List<Submission>[][] submissions;

        public int CourseID
        {
            get { return courseId; }
            set { courseId = value; }
        }

        public int AssignmentID
        {
            get { return assignmentId; }
            set { assignmentId = value; }
        }

        public List<User> Students
        {
            get { return students; }
        }

        public List<Assignment> Assignments
        {
            get { return assignments; }
        }

        public List<Submission>[][] Submissions
        {
            get { return submissions; }
        }

        public CourseSpreadsheetFactory(Context context)
            : base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.courseId = -1;
            this.assignmentId = -1;

            this.students = null;
            this.assignments = null;
            this.submissions = null;
        }

        public void FindStudents(int max, int from, string orderBy)
        {
            var uf = new UserFactory(Context)
            {
                CourseID = courseId,
                Role = UserRoleType.Student

                // TODO: add search criteria
            };

            students = new List<User>(uf.Find(max, from, orderBy));
            studentIds = new Dictionary<int, int>();

            for (int i = 0; i < students.Count; i++)
            {
                studentIds.Add(students[i].ID, i);
            }
        }

        public void FindAssignments()
        {
            if (assignmentId > 0)
            {
                var assignment = new Assignment(Context);
                assignment.Load(assignmentId);

                assignments = new List<Assignment>();
                assignments.Add(assignment);
            }
            else
            {
                var af = new AssignmentFactory(Context)
                {
                    CourseID = courseId
                };

                assignments = new List<Assignment>(af.Find(-1, -1, "Name"));
            }

            assignmentIds = new Dictionary<int, int>();

            for (int i = 0; i < assignments.Count; i++)
            {
                assignmentIds.Add(assignments[i].ID, i);
            }
        }

        public void FindSubmissions(int max, int from, string orderBy)
        {
            var sql = @"
WITH u AS
(
	SELECT u.*, ROW_NUMBER() OVER(ORDER BY u.Name) rn
	FROM [User] u
	INNER JOIN [UserRole] r ON r.UserID = u.ID
	WHERE r.CourseID = @CourseID AND r.UserRoleType = 3	-- student of the course
),
a AS
(
	SELECT a.*, ROW_NUMBER() OVER(ORDER BY a.Name) rn
	FROM [Assignment] a
	WHERE CourseID = @CourseID AND (@AssignmentID = -1 OR ID = @AssignmentID)
),
s AS
(
	SELECT s.*, ROW_NUMBER() OVER(PARTITION BY s.StudentID ORDER BY s.ID) rn
	FROM [Submission] s
	WHERE TeacherID IS NULL		-- only student submissions
)
SELECT s.*,
    a.Name AssignmentName, a.Description AssignmentDescription,
    c.ID CourseID, c.Name CourseName, c.Description CourseDescription,
    r.ID SemesterID, r.Name SemesterName, r.Description SemesterDescription,
    u.Name StudentName, teacher.Name TeacherName
FROM (u CROSS JOIN a)
INNER JOIN s ON s.StudentID = u.ID AND s.AssignmentID = a.ID
INNER JOIN [Course] c ON c.ID = a.CourseID
INNER JOIN [Semester] r ON r.ID = c.SemesterID
LEFT OUTER JOIN [User] teacher ON teacher.ID = s.TeacherID
--WHERE u.rn BETWEEN @from AND @to
ORDER BY u.rn, a.rn, s.rn
";

            using (var cmd = Context.CreateCommand(sql))
            {

                cmd.Parameters.Add("@CourseID", SqlDbType.Int).Value = courseId;
                cmd.Parameters.Add("@AssignmentID", SqlDbType.Int).Value = assignmentId;

                cmd.Parameters.Add("@from", SqlDbType.Int).Value = from;
                cmd.Parameters.Add("@to", SqlDbType.Int).Value = from + max;

                submissions = new List<Submission>[students.Count][];
                for (int i = 0; i < submissions.Length; i++)
                {
                    submissions[i] = new List<Submission>[assignments.Count];
                }

                foreach (var submission in Context.ExecuteCommandReader<Submission>(cmd))
                {
                    var si = studentIds[submission.StudentID];
                    var ai = assignmentIds[submission.AssignmentID];

                    if (submissions[si][ai] == null)
                    {
                        submissions[si][ai] = new List<Submission>();
                    }

                    submissions[si][ai].Add(submission);
                }
            }
        }
    }
}
