using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver
{
    public class PointingElimination : SolutionAlgorithm
    {
        public bool used { get; set; }
        public override void checkSurroundings(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            throw new NotImplementedException();
        }

        public override void updateCandidates(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            throw new NotImplementedException();
        }

        public override void updateSurroundings(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            if (used)
            {
                for (int i = 0; i < puzzle.squares.Count; i++)
                {
                    if (puzzle.squares[i].number == '-' && puzzle.squares[i].candidates.Count == 1)
                    {
                        new SingleCandidate().Solve(i, puzzle, grid);
                        Console.WriteLine("backtrack " + i);
                    }
                }
            }
        }
    }
}
