using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku_Solver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver_Tests
{
    [TestClass]
    public class PuzzleSolverTests
    {
        [TestMethod()]
        public void ConstructionTest()
        {
            PuzzleSolver solver = new PuzzleSolver(@"C:\Users\cmaug\Desktop\School\Fall 2018\Object Oriented Programming\Homework\Homework 4\testpuzzle.txt", "");
            
        }
    }
}
