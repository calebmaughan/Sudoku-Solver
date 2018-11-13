using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku_Solver
{
    public class BlockRows : PointingElimination
    {
        Dictionary<int, List<string>> blockRow {get; set;}

        public override void checkSurroundings(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            if(squareNum == 13)
            {

            }
            used = false;
            Dictionary<string, int> containers = grid.findContainers(squareNum);
            row = grid.rows[containers["Row"]];
            block = grid.blocks[containers["Block"]];
            col = grid.columns[containers["Column"]];
            blockRow = new Dictionary<int, List<string>>();
            for(int i = 0; i < Convert.ToInt32(Math.Sqrt(block.Squares.Count)); i++)
            {
                blockRow.Add(i, new List<string>());
            }

            int currentRow = 0;
            for(int i = 0; i < block.Squares.Count; i++)
            {
                if(puzzle.squares[block.Squares[i]].number == '-')
                {
                    addBlockRowCandidates(block.Squares[i], currentRow, puzzle);
                }
                if((i+1)%Convert.ToInt32(Math.Sqrt(block.Squares.Count)) == 0)
                {
                    currentRow++;
                }
            }
            List<string> temp = new List<string>();
            for(int i = 0; i < blockRow.Count; i++)
            {
                List<string> temp2 = new List<string>();
                for(int j = 0; j < blockRow[i].Count; j++)
                {
                    if (!temp2.Contains(blockRow[i][j]))
                    {
                        temp2.Add(blockRow[i][j]);
                    }
                }
                for(int j = 0; j < blockRow.Count; j++)
                {
                    if(j != i)
                    {
                        for(int k = 0; k < blockRow[j].Count; k++)
                        {
                            if (temp2.Contains(blockRow[j][k]))
                            {
                                temp2.Remove(blockRow[j][k]);
                            }
                        }
                        
                    }
                }
                temp = temp.Concat(temp2).ToList<string>();
            }
            //You need to except off the stuff you havent excepted off of!
            for(int i = 0; i < blockRow.Count; i++)
            {
                blockRow[i] = blockRow[i].Intersect(temp).ToList<string>();
            }
        }

        public override void updateCandidates(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            for(int i = 0; i < blockRow.Count; i++)
            {
                if(blockRow[i].Count > 0)
                {
                    PuzzleRow update = grid.rows[block.Rows[i]];
                    for(int j = 0; j < update.Squares.Count; j++)
                    {
                        if(puzzle.squares[update.Squares[j]].number == '-' && !block.Squares.Contains(update.Squares[j]))
                        {
                            for(int k = 0; k < blockRow[i].Count; k++)
                            {
                                if (puzzle.squares[update.Squares[j]].candidates.Contains(blockRow[i][k]))
                                {
                                    used = true;
                                    puzzle.squares[update.Squares[j]].candidates.Remove(blockRow[i][k]);
                                }
                            }
                        }
                    }
                }
            }
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
