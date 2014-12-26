using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Complex.Domino.Git
{
    /// <summary>
    /// Summary description for CommitTest
    /// </summary>
    [TestClass]
    public class CommitTest
    {
        [TestMethod]
        public void ParseTest()
        {
            var text =
@"commit 250115a572cec457210cf96d0b776158aaff9bed
tree 5efb9bc29c482e023e40e0a2b3b7e49cec842034
author Laszlo Dobos <dobos@complex.elte.hu> 1419553691 +0100
committer Laszlo Dobos <dobos@complex.elte.hu> 1419553691 +0100

    test

";

            var c = new Commit();
            c.Read(new StringReader(text));

            Assert.AreEqual("250115a572cec457210cf96d0b776158aaff9bed", c.Hash);
            Assert.AreEqual(
                new DateTime(2014, 12, 26, 0, 28, 11),
                c.Date);
        }
    }
}
