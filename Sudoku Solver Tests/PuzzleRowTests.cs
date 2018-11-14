using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku_Solver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver_Tests
{
    [TestClass]
    public class PuzzleRowTests
    {
        [TestMethod]
        public void ConstructionTest()
        {
            PuzzleRow row = new PuzzleRow(0, 9);
            Assert.IsNotNull(row.Blocks);
            Assert.IsNotNull(row.Squares);
            Assert.AreEqual(0, row.index);
        }
    }
}
