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
    public class MatchTest
    {
        [TestMethod]
        public void ParseTest()
        {
            var text =
@"8b6a9176fe62d808f4f0cc9208b1d14fe780e9f6:2014-15-2/fiznum2/1.feladat/mvteljes.c:84:    matrixbe(M,&matrix,&sor,&oszlop);";

            var m = new Match();
            m.Read(new StringReader(text));

            Assert.AreEqual("8b6a9176fe62d808f4f0cc9208b1d14fe780e9f6", m.Hash);
            Assert.AreEqual("2014-15-2/fiznum2/1.feladat/mvteljes.c", m.FileName);
            Assert.AreEqual(84, m.Line);
            Assert.AreEqual("    matrixbe(M,&matrix,&sor,&oszlop);", m.Text);
        }
    }
}
