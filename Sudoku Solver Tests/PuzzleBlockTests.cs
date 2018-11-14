using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku_Solver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver_Tests
{
    [TestClass]
    public class PuzzleBlockTests
    {
        [TestMethod()]
        public void ConstructionTest()
        {
            PuzzleBlock block = new PuzzleBlock(0, 9);
            Assert.IsNotNull(block.Columns);
            Assert.IsNotNull(block.Squares);
            Assert.IsNotNull(block.Rows);
            Assert.AreEqual(0, block.index);
        }
    }
}
