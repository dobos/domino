using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Complex.Domino.Git
{

    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void CurrentTest()
        {
            var a = User.Current;

            Assert.IsNotNull(a.ToString());
        }

        [TestMethod]
        public void ParseTest()
        {
            var a = User.Parse("Laszlo Dobos <dobos@complex.elte.hu>");

            Assert.AreEqual("Laszlo Dobos", a.Name);
            Assert.AreEqual("dobos@complex.elte.hu", a.Email);
        }

        [TestMethod]
        public void ToStringTest()
        {
            var a = new User()
            {
                Name = "Laszlo Dobos",
                Email = "dobos@complex.elte.hu"
            };

            Assert.AreEqual("Laszlo Dobos <dobos@complex.elte.hu>", a.ToString());
        }
    }
}
