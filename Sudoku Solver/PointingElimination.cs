using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver
{
    public class PointingElimination : SolutionAlgorithm
    {
        PuzzleRow row { get; set; }
        PuzzleBlock block { get; set; }
        PuzzleColumn col { get; set; }
        Dictionary<int, List<string>> blockRow {get; set;}

        public override void checkSurroundings(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            blockRow = new Dictionary<int, List<string>>();
            Dictionary<string, int> containers = grid.findContainers(squareNum);
            row = grid.rows[containers["Row"]];
            block = grid.blocks[containers["Block"]];
            col = grid.columns[containers["Column"]];
            int currentRow = 0;
            for(int i = 0; i < block.Squares.Count; i++)
            {
                if(puzzle.squares[block.Squares[i]].number == '-')
                {
                    addBlockRowCandidates(block.Squares[i], currentRow, puzzle);
                }
                if((i+1)%Convert.ToInt32(block.Squares.Count) == 0)
                {
                    currentRow++;
                }
            }
        }

        public override void updateCandidates(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            throw new NotImplementedException();
        }

        public override void updateSurroundings(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            throw new NotImplementedException();
        }

        public void addBlockRowCandidates(int square, int row, PuzzleNumbers puzzle)
        {
            for(int i = 0; i < puzzle.squares[square].candidates.Count; i++)
            {
                if (!blockRow[row].Contains(puzzle.squares[square].candidates[i]))
                {
                    blockRow[row].Add(puzzle.squares[square].candidates[i]);
                }
            }
        }
    }
}
