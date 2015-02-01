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

        public CourseSpreadsheetFactory(Context context)
            : base(context)
        {
        }

        private void InitializeMembers()
        {
            this.courseId = -1;
            this.assignmentId = -1;

            this.students = null;
            this.assignments = null;
            this.submissions = null;
        }

        private IEnumerable<User> FindStudents(int max, int from, string orderBy)
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

            return students;
        }

        private void FindAssignments()
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

        private void FindSubmissions(int max, int from, string orderBy)
        {
            var sql = @"
DECLARE @CourseID int = 1;

WITH u AS
(
	SELECT u.ID, ROW_NUMBER() OVER(ORDER BY u.Name) rn
	FROM [User] u
	INNER JOIN [UserRole] r ON r.UserID = u.ID
	WHERE r.CourseID = @CourseID AND r.UserRoleType = 3	-- student of the course
),
a AS
(
	SELECT a.ID, ROW_NUMBER() OVER(ORDER BY a.Name) rn
	FROM [Assignment] a
	WHERE CourseID = @CourseID AND (@AssigmentID = -1 OR ID = @AssignmentID)
),
s AS
(
	SELECT s.*, ROW_NUMBER() OVER(PARTITION BY s.StudentID ORDER BY s.ID) rn
	FROM [Submission] s
	WHERE TeacherID IS NULL		-- only student submissions
)
SELECT s.*
FROM (u CROSS JOIN a)
INNER JOIN s ON s.StudentID = u.ID AND s.AssignmentID = a.ID
WHERE u.rn BETWEEN @from AND @to
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
