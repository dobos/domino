using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Complex.Domino.Git
{
    [TestClass]
    public class GitTest : TestBase
    {
        protected string TestRepoPath
        {
            get { return Path.GetFullPath("testrepo"); }
        }

        protected Git Git
        {
            get
            {
                var git = new Git()
                {
                    RepoPath = TestRepoPath,
                    Author = new User()
                    {
                        Name = "Test Testing",
                        Email = "test@testing.com"
                    }
                };

                return git;
            }
        }

        [TestInitialize]
        public void Initialize()
        {
            // Create directory for empty repo
            var repopath = TestRepoPath;

            Util.IO.ForceDeleteDirectory(repopath);

            Directory.CreateDirectory(repopath);
        }

        [TestCleanup]
        public void Cleanup()
        {
            var repopath = TestRepoPath;

            Domino.Util.IO.ForceDeleteDirectory(repopath);
        }

        [TestMethod]
        public void ThoroughTest()
        {
            Git.Init(false);

            Assert.IsTrue(Directory.Exists(Path.Combine(TestRepoPath, ".git")));

            // Create a file in the repo
            File.WriteAllText(
                Path.Combine(TestRepoPath, "test.txt"),
                "This is the first revision");

            Git.Add(".", true);
            Git.Commit("First revision", null, true);

            // Modify file
            File.WriteAllText(
                Path.Combine(TestRepoPath, "test.txt"),
                "This is the second revision");

            Git.Commit("Second revision", null, true);

            var commits = Git.ReadLog();
            Assert.AreEqual(2, commits.Count);
        }
    }
}
