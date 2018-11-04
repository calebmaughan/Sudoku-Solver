using System;

namespace Sudoku_Solver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Sudoku Solver!\nPress the enter key to exit.");

            new PuzzleSolver(@"C:\Users\cmaug\Desktop\testpuzzle.txt");

            Console.Read();
        }
    }
}
