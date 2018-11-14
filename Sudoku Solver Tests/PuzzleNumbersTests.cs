using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku_Solver;

namespace Sudoku_Solver_Tests
{
    [TestClass]
    public class PuzzleNumbersTests
    {
        [TestMethod]
        public void ConstructionTest()
        {
            PuzzleNumbers puzzle = new PuzzleNumbers(@"C:\Users\cmaug\Desktop\School\Fall 2018\Object Oriented Programming\Homework\Homework 4\testpuzzle.txt");
            Assert.IsNotNull(puzzle.squares);
            Assert.AreEqual(puzzle.size, 9);
        }
    }
}
