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
    }
}
