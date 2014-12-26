using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Complex.Domino.Git
{
    [TestClass]
    public class GitTest
    {
        protected string TestRepoPath
        {
            get { return Path.GetFullPath("testrepo"); }
        }

        [TestInitialize()]
        public void Initialize() 
        {
            // Create directory for empty repo
            var repopath = TestRepoPath;

            if (Directory.Exists(repopath))
            {
                Directory.Delete(repopath, true);
            }

            Directory.CreateDirectory(repopath);
        }

        [TestCleanup()]
        public void Cleanup() 
        {
            var repopath = TestRepoPath;

            if (Directory.Exists(repopath))
            {
                Directory.Delete(repopath, true);
            }
        }

        [TestMethod]
        public void InitTest()
        {
            var git = new Git(TestRepoPath);

            git.Init();

            Assert.IsTrue(Directory.Exists(Path.Combine(TestRepoPath, ".git")));
        }
    }
}
