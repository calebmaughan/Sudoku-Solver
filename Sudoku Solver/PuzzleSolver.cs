using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver
{
    public class PuzzleSolver
    {
        PuzzleNumbers puzzle { get; set; }
        PuzzleStructure structure { get; set; }

        public PuzzleSolver(string input)
        {
            try
            {
                puzzle = new PuzzleNumbers(input);



                //puzzle.debugWrite();

                structure = new PuzzleStructure(puzzle.size);


                structure.debugWrite();
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Puzzle");
            }
        }
    }
}
