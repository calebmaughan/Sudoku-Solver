using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku_Solver
{
    public class RowElimination : PointingElimination
    {
        Dictionary<int, List<string>> rowBlock { get; set; }

        public override void checkSurroundings(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            used = false;
            Dictionary<string, int> containers = grid.findContainers(squareNum);
            row = grid.rows[containers["Row"]];
            block = grid.blocks[containers["Block"]];
            col = grid.columns[containers["Column"]];
            rowBlock = new Dictionary<int, List<string>>();
            for (int i = 0; i < Convert.ToInt32(Math.Sqrt(col.Squares.Count)); i++)
            {
                rowBlock.Add(i, new List<string>());
            }

            int currentBlock = 0;
            for (int i = 0; i < row.Squares.Count; i++)
            {
                if (puzzle.squares[row.Squares[i]].number == '-')
                {
                    AddRowBlockCandidates(row.Squares[i], currentBlock, puzzle);
                }
                if ((i + 1) % Convert.ToInt32(Math.Sqrt(row.Squares.Count)) == 0)
                {
                    currentBlock++;
                }
            }
            List<string> temp = new List<string>();
            for (int i = 0; i < rowBlock.Count; i++)
            {
                List<string> temp2 = new List<string>();
                for (int j = 0; j < rowBlock[i].Count; j++)
                {
                    if (!temp2.Contains(rowBlock[i][j]))
                    {
                        temp2.Add(rowBlock[i][j]);
                    }
                }
                for (int j = 0; j < rowBlock.Count; j++)
                {
                    if (j != i)
                    {
                        for (int k = 0; k < rowBlock[j].Count; k++)
                        {
                            if (temp2.Contains(rowBlock[j][k]))
                            {
                                temp2.Remove(rowBlock[j][k]);
                            }
                        }

                    }
                }
                temp = temp.Concat(temp2).ToList<string>();
            }
            for (int i = 0; i < rowBlock.Count; i++)
            {
                rowBlock[i] = rowBlock[i].Intersect(temp).ToList<string>();
            }
        }

        public override void updateCandidates(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            for (int i = 0; i < rowBlock.Count; i++)
            {
                if (rowBlock[i].Count > 0)
                {
                    PuzzleBlock update = grid.blocks[row.Blocks[i]];
                    for (int j = 0; j < update.Squares.Count; j++)
                    {
                        if (puzzle.squares[update.Squares[j]].number == '-' && !row.Squares.Contains(update.Squares[j]))
                        {
                            for (int k = 0; k < rowBlock[i].Count; k++)
                            {
                                if (puzzle.squares[update.Squares[j]].candidates.Contains(rowBlock[i][k]))
                                {
                                    puzzle.squares[update.Squares[j]].candidates.Remove(rowBlock[i][k]);
                                    used = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void AddRowBlockCandidates(int square, int block, PuzzleNumbers puzzle)
        {
            for (int i = 0; i < puzzle.squares[square].candidates.Count; i++)
            {
                if (!rowBlock[block].Contains(puzzle.squares[square].candidates[i]))
                {
                    rowBlock[block].Add(puzzle.squares[square].candidates[i]);
                }
            }
        }
    }
}
