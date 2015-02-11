using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Complex.Domino.Lib
{
    [TestClass]
    public class AccessTest
    {
        const string ConnectionString = "Data Source=localhost;Initial Catalog=Domino_Test;Integrated Security=true";

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            using (var context = Context.Create())
            {
                var i = new Installer(context);

                i.DropSchema();
                i.CreateSchema();
                i.InitializeData();
            }
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            using (var context = Context.Create())
            {
                var i = new Installer(context);

                i.DropSchema();
            }
        }

        [TestMethod]
        public void UserAccessTest()
        {
            using (var context = Context.Create())
            {
                // Sign in as a user
                var admin = new User(context);
                admin.SignIn("admin", "alma");

                context.User = admin;

                // Create semester
                var semester = new Semester(context)
                {
                    Name = "test",
                };
                semester.Save();

                // Create courses
                var course1 = new Course(context)
                {
                    SemesterID = semester.ID,
                    Name = "test",
                };
                course1.Save();

                var course2 = new Course(context)
                {
                    SemesterID = semester.ID,
                    Name = "test 2",
                };
                course2.Save();

                var assignment2 = new Assignment(context)
                {
                    CourseID = course2.ID,
                    Name = "test 2",
                };
                assignment2.Save();

                // Create teacher
                var teacher = new User(context)
                {
                    Name = "teacher",
                    Email = "teacher@test.com",
                };
                teacher.SetPassword("alma");
                teacher.Save();
                teacher.AddRole(new UserRole(course1.ID, teacher.ID, UserRoleType.Teacher));

                // Create student
                var student = new User(context)
                {
                    Name = "student",
                    Email = "student@test.com",
                };
                student.SetPassword("alma");
                student.Save();
                student.AddRole(new UserRole(course1.ID, student.ID, UserRoleType.Student));


                // --- Teacher test
                context.User = teacher;

                course1.Load();
                course1.Save();

                var assignment = new Assignment(context)
                {
                    CourseID = course1.ID,
                    Name = "assignment",
                };
                assignment.Save();  // Create
                assignment.Save();  // Modify

                try
                {
                    course2.Load();
                    Assert.Fail();
                }
                catch (SecurityException) { }

                try
                {
                    course2.Save();
                    Assert.Fail();
                }
                catch (SecurityException) { }

                try
                {
                    course2.Delete();
                    Assert.Fail();
                }
                catch (SecurityException) { }

                // --- create assignment to invalid course

                var assignment3 = new Assignment(context)
                {
                    CourseID = course2.ID,
                    Name = "assignment 3",
                };
                assignment3.Save();


                // Student test
                context.User = student;

                course1.Load();

                assignment.Load();

                var submission = new Submission(context)
                {
                    StudentID = student.ID,
                    AssignmentID = assignment.ID,
                    Name = "submission"
                };

                submission.Save();  // Create

                try
                {
                    submission.Save();  // Modify
                    Assert.Fail();
                }
                catch (SecurityException) { }

                try
                {
                    course1.Save();
                    Assert.Fail();
                }
                catch (SecurityException) { }

                try
                {
                    course2.Load();
                    Assert.Fail();
                }
                catch (SecurityException) { }

                try
                {
                    course2.Save();
                    Assert.Fail();
                }
                catch (SecurityException) { }

                try
                {
                    course2.Delete();
                    Assert.Fail();
                }
                catch (SecurityException) { }

                // --- submit to invalid assignment
                try
                {
                    var submission2 = new Submission(context)
                    {
                        StudentID = student.ID,
                        AssignmentID = assignment2.ID,
                        Name = "submission 2"
                    };

                    submission2.Save();  // Create
                    Assert.Fail();
                }
                catch (SecurityException) { }
            }
        }
    }
}
