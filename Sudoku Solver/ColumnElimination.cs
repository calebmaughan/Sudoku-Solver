using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Sudoku_Solver
{
    public class ColumnElimination : PointingElimination
    {
        Dictionary<int, List<string>> colBlock { get; set; }

        protected override void checkSurroundings(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            used = false;
            Dictionary<string, int> containers = grid.findContainers(squareNum);
            row = grid.rows[containers["Row"]];
            block = grid.blocks[containers["Block"]];
            col = grid.columns[containers["Column"]];
            colBlock = new Dictionary<int, List<string>>();
            for (int i = 0; i < Convert.ToInt32(Math.Sqrt(col.Squares.Count)); i++)
            {
                colBlock.Add(i, new List<string>());
            }

            int currentBlock = 0;
            for (int i = 0; i < col.Squares.Count; i++)
            {
                if (puzzle.squares[col.Squares[i]].number == '-')
                {
                    AddColBlockCandidates(col.Squares[i], currentBlock, puzzle);
                }
                if ((i + 1) % Convert.ToInt32(Math.Sqrt(col.Squares.Count)) == 0)
                {
                    currentBlock++;
                }
            }
            List<string> temp = new List<string>();
            for (int i = 0; i < colBlock.Count; i++)
            {
                List<string> temp2 = new List<string>();
                for (int j = 0; j < colBlock[i].Count; j++)
                {
                    if (!temp2.Contains(colBlock[i][j]))
                    {
                        temp2.Add(colBlock[i][j]);
                    }
                }
                for (int j = 0; j < colBlock.Count; j++)
                {
                    if (j != i)
                    {
                        for (int k = 0; k < colBlock[j].Count; k++)
                        {
                            if (temp2.Contains(colBlock[j][k]))
                            {
                                temp2.Remove(colBlock[j][k]);
                            }
                        }

                    }
                }
                temp = temp.Concat(temp2).ToList<string>();
            }
            for (int i = 0; i < colBlock.Count; i++)
            {
                colBlock[i] = colBlock[i].Intersect(temp).ToList<string>();
            }
        }

        protected override void updateCandidates(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            for (int i = 0; i < colBlock.Count; i++)
            {
                if (colBlock[i].Count > 0)
                {
                    PuzzleBlock update = grid.blocks[col.Blocks[i]];
                    for (int j = 0; j < update.Squares.Count; j++)
                    {
                        if (puzzle.squares[update.Squares[j]].number == '-' && !col.Squares.Contains(update.Squares[j]))
                        {
                            for (int k = 0; k < colBlock[i].Count; k++)
                            {
                                if (puzzle.squares[update.Squares[j]].candidates.Contains(colBlock[i][k]))
                                {
                                    puzzle.squares[update.Squares[j]].candidates.Remove(colBlock[i][k]);
                                    used = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void AddColBlockCandidates(int square, int block, PuzzleNumbers puzzle)
        {
            for (int i = 0; i < puzzle.squares[square].candidates.Count; i++)
            {
                if (!colBlock[block].Contains(puzzle.squares[square].candidates[i]))
                {
                    colBlock[block].Add(puzzle.squares[square].candidates[i]);
                }
            }
        }
    }
}
