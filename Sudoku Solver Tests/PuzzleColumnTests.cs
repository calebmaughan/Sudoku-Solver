using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku_Solver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver_Tests
{
    [TestClass]
    public class PuzzleColumnTests
    {
        [TestMethod()]
        public void ConstructionTests()
        {
            PuzzleColumn col = new PuzzleColumn(0, 9);
            Assert.IsNotNull(col.Blocks);
            Assert.IsNotNull(col.Squares);
            Assert.AreEqual(0, col.index);
        }
    }
}
