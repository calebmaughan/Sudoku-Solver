using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku_Solver
{
    public class BlockCols : PointingElimination
    {
        Dictionary<int, List<string>> blockCol { get; set; }

        public override void checkSurroundings(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            used = false;
            Dictionary<string, int> containers = grid.findContainers(squareNum);
            row = grid.rows[containers["Row"]];
            block = grid.blocks[containers["Block"]];
            col = grid.columns[containers["Column"]];
            blockCol = new Dictionary<int, List<string>>();
            for (int i = 0; i < Convert.ToInt32(Math.Sqrt(block.Squares.Count)); i++)
            {
                blockCol.Add(i, new List<string>());
            }

            int currentCol = 0;
            for (int i = 0; i < block.Squares.Count; i++)
            {
                if (puzzle.squares[block.Squares[i]].number == '-')
                {
                    addBlockColCandidates(block.Squares[i], currentCol, puzzle);
                }
                currentCol++;
                if(currentCol == 3)
                {
                    currentCol = 0;
                }
            }
            List<string> temp = new List<string>();
            for (int i = 0; i < blockCol.Count; i++)
            {
                List<string> temp2 = new List<string>();
                for (int j = 0; j < blockCol[i].Count; j++)
                {
                    if (!temp2.Contains(blockCol[i][j]))
                    {
                        temp2.Add(blockCol[i][j]);
                    }
                }
                for (int j = 0; j < blockCol.Count; j++)
                {
                    if (j != i)
                    {
                        for (int k = 0; k < blockCol[j].Count; k++)
                        {
                            if (temp2.Contains(blockCol[j][k]))
                            {
                                temp2.Remove(blockCol[j][k]);
                            }
                        }

                    }
                }
                temp = temp.Concat(temp2).ToList<string>();
            }
            for (int i = 0; i < blockCol.Count; i++)
            {
                blockCol[i] = blockCol[i].Intersect(temp).ToList<string>();
            }
        }

        public override void updateCandidates(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            for (int i = 0; i < blockCol.Count; i++)
            {
                if (blockCol[i].Count > 0)
                {
                    PuzzleColumn update = grid.columns[block.Columns[i]];
                    for (int j = 0; j < update.Squares.Count; j++)
                    {
                        if (puzzle.squares[update.Squares[j]].number == '-' && !block.Squares.Contains(update.Squares[j]))
                        {
                            for (int k = 0; k < blockCol[i].Count; k++)
                            {
                                if (puzzle.squares[update.Squares[j]].candidates.Contains(blockCol[i][k]))
                                {
                                    puzzle.squares[update.Squares[j]].candidates.Remove(blockCol[i][k]);
                                    Console.WriteLine("bc used " + squareNum);
                                    used = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void addBlockColCandidates(int square, int row, PuzzleNumbers puzzle)
        {
            for (int i = 0; i < puzzle.squares[square].candidates.Count; i++)
            {
                if (!blockCol[row].Contains(puzzle.squares[square].candidates[i]))
                {
                    blockCol[row].Add(puzzle.squares[square].candidates[i]);
                }
            }
        }
    }
}
