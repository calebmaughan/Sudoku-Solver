using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver
{
    public abstract class SolutionAlgorithm
    {
        public int SinglesUsed { get; set; }
        public PuzzleRow row { get; set; }
        public PuzzleBlock block { get; set; }
        public PuzzleColumn col { get; set; }

        public SolutionAlgorithm()
        {
            SinglesUsed = 0;
        }

        public bool Solve(int squareNumber, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            checkSurroundings(squareNumber, puzzle, grid);
            updateCandidates(squareNumber, puzzle, grid);
            return updateSurroundings(squareNumber, puzzle, grid);
        }

        protected abstract void checkSurroundings(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid);
        protected abstract void updateCandidates(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid);
        protected abstract bool updateSurroundings(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid);
    }
}
