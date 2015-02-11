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
        }
    }
}
