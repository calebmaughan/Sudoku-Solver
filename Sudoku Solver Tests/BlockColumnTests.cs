using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku_Solver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver_Tests
{
    [TestClass]
    public class BlockColumnTests
    {
        [TestMethod()]
        public void SolveTest()
        {
            PuzzleNumbers puzzle = new PuzzleNumbers(@"C:\Users\cmaug\Desktop\School\Fall 2018\Object Oriented Programming\Homework\Homework 4\testpuzzle.txt");
            PuzzleStructure grid = new PuzzleStructure(puzzle.size);
            BlockCols test = new BlockCols();
            test.Solve(0, puzzle, grid);
            Assert.IsNotNull(test.row);
            Assert.IsNotNull(test.block);
            Assert.IsNotNull(test.col);
            Assert.AreEqual(test.SinglesUsed, 0);
        }
    }
}
