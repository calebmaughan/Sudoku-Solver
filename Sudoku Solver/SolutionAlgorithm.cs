using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver
{
    public abstract class SolutionAlgorithm
    {
        public void Solve(int squareNumber, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            checkSurroundings(squareNumber, puzzle, grid);
            updateCandidates(squareNumber, puzzle, grid);
            updateSurroundings(squareNumber, puzzle, grid);
        }

        public abstract void checkSurroundings(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid);
        public abstract void updateCandidates(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid);
        public abstract void updateSurroundings(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid);
    }
}
