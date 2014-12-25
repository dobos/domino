using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Complex.Domino.Git
{
    [TestClass]
    public class GitConfigurationTest
    {
        [TestMethod]
        public void BinPathTest()
        {
            Assert.IsTrue(File.Exists(Path.Combine(Git.Configuration.BinPath, "git.exe")));
        }
    }
}
