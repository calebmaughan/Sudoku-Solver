using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku_Solver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver_Tests
{
    [TestClass]
    public class NumberSquareTests
    {
        [TestMethod()]
        public void ConstructionTest()
        {
            List<string> candidates = new List<string> { "1", "2", "3", "4" };
            NumberSquare square = new NumberSquare('-', candidates);
            Assert.IsNotNull(square.candidates);
            Assert.AreEqual(square.number, '-');
            square = new NumberSquare('1', candidates);
            Assert.IsNull(square.candidates);
            Assert.AreEqual(square.number, '1');
            try
            {
                square = new NumberSquare('5', candidates);
                Assert.Fail();
            }
            catch (Exception)
            {
            }
        }
    }
}
