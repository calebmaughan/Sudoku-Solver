using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku_Solver
{
    class HiddenPair : SolutionAlgorithm
    {
        bool used { get; set; }
        bool skip { get; set; }
        List<int> rowPossible { get; set; }
        List<int> colPossible { get; set; }
        List<int> blockPossible { get; set; }

        public override void checkSurroundings(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            skip = true;
            if(puzzle.squares[squareNum].number == '-' && puzzle.squares[squareNum].candidates.Count == 2)
            {
                if(squareNum == 22)
                {
                    Console.Write("");
                }
                rowPossible = new List<int>();
                colPossible = new List<int>();
                blockPossible = new List<int>();
                skip = false;
                Dictionary<string, int> containers = grid.findContainers(squareNum);
                row = grid.rows[containers["Row"]];
                block = grid.blocks[containers["Block"]];
                col = grid.columns[containers["Column"]];
                List<string> compare = new List<string>(puzzle.squares[squareNum].candidates);
                for(int i = 0; i < row.Squares.Count; i++)
                {
                    if (puzzle.squares[row.Squares[i]].number == '-' && puzzle.squares[row.Squares[i]].candidates.SequenceEqual(compare) && row.Squares[i] != squareNum)
                    {
                        rowPossible.Add(row.Squares[i]);
                    }
                    if (puzzle.squares[col.Squares[i]].number == '-' && puzzle.squares[col.Squares[i]].candidates.SequenceEqual(compare) && col.Squares[i] != squareNum)
                    {
                        colPossible.Add(col.Squares[i]);
                    }
                    if (puzzle.squares[block.Squares[i]].number == '-' && puzzle.squares[block.Squares[i]].candidates.SequenceEqual(compare) && block.Squares[i] != squareNum)
                    {
                        blockPossible.Add(block.Squares[i]);
                    }
                }
            }
        }

        public override void updateCandidates(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            used = false;
            if (!skip)
            {
                if (blockPossible.Count == 1)
                {
                    for (int i = 0; i < block.Squares.Count; i++)
                    {
                        if (puzzle.squares[block.Squares[i]].number == '-' && block.Squares[i] != squareNum && !blockPossible.Contains(block.Squares[i]))
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                if (puzzle.squares[block.Squares[i]].candidates.Contains(puzzle.squares[squareNum].candidates[j]))
                                {
                                    puzzle.squares[block.Squares[i]].candidates.Remove(puzzle.squares[squareNum].candidates[j]);
                                    used = true;
                                }
                            }
                        }
                    }
                }
                if (colPossible.Count == 1)
                {
                    
                    for (int i = 0; i < col.Squares.Count; i++)
                    {
                        if (puzzle.squares[col.Squares[i]].number == '-' && col.Squares[i] != squareNum && !colPossible.Contains(col.Squares[i]))
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                if (puzzle.squares[col.Squares[i]].candidates.Contains(puzzle.squares[squareNum].candidates[j]))
                                {
                                    puzzle.squares[col.Squares[i]].candidates.Remove(puzzle.squares[squareNum].candidates[j]);
                                    used = true;
                                }
                            }
                        }
                    }
                }
                if (rowPossible.Count == 1)
                {
                    
                    for (int i = 0; i < row.Squares.Count; i++)
                    {
                        if (puzzle.squares[row.Squares[i]].number == '-' && row.Squares[i] != squareNum && !rowPossible.Contains(row.Squares[i]))
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                if (puzzle.squares[row.Squares[i]].candidates.Contains(puzzle.squares[squareNum].candidates[j]))
                                {
                                    puzzle.squares[row.Squares[i]].candidates.Remove(puzzle.squares[squareNum].candidates[j]);
                                    used = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        public override bool updateSurroundings(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            if (!skip)
            {
                for (int i = 0; i < row.Squares.Count; i++)
                {
                    if (puzzle.squares[row.Squares[i]].number == '-' && puzzle.squares[row.Squares[i]].candidates.Count == 1)
                    {
                        SingleCandidate temp = new SingleCandidate();
                        temp.Solve(row.Squares[i], puzzle, grid);
                        SinglesUsed++;
                        SinglesUsed += temp.SinglesUsed;
                    }
                }
                for (int i = 0; i < col.Squares.Count; i++)
                {
                    if (puzzle.squares[col.Squares[i]].number == '-' && puzzle.squares[col.Squares[i]].candidates.Count == 1)
                    {
                        SingleCandidate temp = new SingleCandidate();
                        temp.Solve(row.Squares[i], puzzle, grid);
                        SinglesUsed++;
                        SinglesUsed += temp.SinglesUsed;
                    }
                }
                for (int i = 0; i < block.Squares.Count; i++)
                {
                    if (puzzle.squares[block.Squares[i]].number == '-' && puzzle.squares[block.Squares[i]].candidates.Count == 1)
                    {
                        SingleCandidate temp = new SingleCandidate();
                        temp.Solve(row.Squares[i], puzzle, grid);
                        SinglesUsed++;
                        SinglesUsed += temp.SinglesUsed;
                    }
                }
            }
            return used;
        }
    }
}
