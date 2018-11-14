using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku_Solver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Sudoku Solver!\nPlease enter an input file:");
            string input = Console.ReadLine();
            Console.WriteLine("Please enter an output file (or leave empty to display on the console):");
            string output = Console.ReadLine();
            new PuzzleSolver(input, output);
        }
    }
}
