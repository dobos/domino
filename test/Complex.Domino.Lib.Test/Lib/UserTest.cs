using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Complex.Domino.Lib
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void HashPasswordTest()
        {
            using (var context = Context.Create())
            {
                var u = new User(context);

                u.SetPassword("alma");
            }
        }

        [TestMethod]
        public void LoadUserTest()
        {
            using (var context = Context.Create())
            {
                var u = new User(context);
                u.Load(1);
                u.Load();
            }
        }

        [TestMethod]
        public void UserRolesTest()
        {
            using (var context = Context.Create())
            {
                var u = new User(context);
                u.Load(1);

                Assert.IsTrue(u.IsInAdminRole());
            }
        }
    }
}
