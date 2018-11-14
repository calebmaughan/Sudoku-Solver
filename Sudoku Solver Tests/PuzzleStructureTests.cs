using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku_Solver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver_Tests
{
    [TestClass]
    public class PuzzleStructureTests
    {
        [TestMethod()]
        public void ConstructionTest()
        {
            PuzzleStructure grid = new PuzzleStructure(9);
            Assert.IsNotNull(grid.blocks);
            Assert.IsNotNull(grid.rows);
            Assert.IsNotNull(grid.columns);
        }

        [TestMethod()]
        public void GetContainersTest()
        {
            PuzzleStructure grid = new PuzzleStructure(9);
            Dictionary <string, int> test = grid.findContainers(10);
            Assert.AreEqual(test["Column"], 1);
            Assert.AreEqual(test["Block"], 0);
            Assert.AreEqual(test["Row"], 1);
        }
    }
}
