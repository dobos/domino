using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                // Create course
                var course = new Course(context)
                {
                    SemesterID = semester.ID,
                    Name = "test",
                };
                course.Save();

                // Create teacher
                var teacher = new User(context)
                {
                    Name = "teacher",
                    Email = "teacher@test.com",
                };
                teacher.SetPassword("alma");
                teacher.Save();
                teacher.AddRole(new UserRole(course.ID, teacher.ID, UserRoleType.Teacher));
                
                // Create student
                var student = new User(context)
                {
                    Name = "student",
                    Email = "student@test.com",
                };
                student.SetPassword("alma");
                student.Save();
                student.AddRole(new UserRole(course.ID, student.ID, UserRoleType.Student));
            }
        }
    }
}
