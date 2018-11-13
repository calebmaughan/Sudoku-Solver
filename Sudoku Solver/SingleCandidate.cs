using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver
{
    class SingleCandidate : SolutionAlgorithm
    {
        List<char> duplicates { get; set; }


        public override void checkSurroundings(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            Dictionary<string, int> containers = grid.findContainers(squareNum);
            row = grid.rows[containers["Row"]];
            block = grid.blocks[containers["Block"]];
            col = grid.columns[containers["Column"]];
            duplicates = new List<char>();
            for(int i = 0; i < row.Squares.Count; i++)
            {
                if(puzzle.squares[row.Squares[i]].number != '-')
                {
                    if (!duplicates.Contains(puzzle.squares[row.Squares[i]].number))
                    {
                        duplicates.Add(puzzle.squares[row.Squares[i]].number);
                    }
                }
                if (puzzle.squares[col.Squares[i]].number != '-')
                {
                    if (!duplicates.Contains(puzzle.squares[col.Squares[i]].number))
                    {
                        duplicates.Add(puzzle.squares[col.Squares[i]].number);
                    }
                }
                if (puzzle.squares[block.Squares[i]].number != '-')
                {
                    if (!duplicates.Contains(puzzle.squares[block.Squares[i]].number))
                    {
                        duplicates.Add(puzzle.squares[block.Squares[i]].number);
                    }
                }
            }
        }

        public override void updateCandidates(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            for(int i = 0; i < duplicates.Count; i++)
            {
                string candidate = duplicates[i].ToString();
                puzzle.squares[squareNum].removeCandidate(candidate);
            }
        }

        public override bool updateSurroundings(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            bool used = false;
            char solution;
            if(puzzle.squares[squareNum].number == '-' && puzzle.squares[squareNum].candidates.Count == 1)
            {
                solution = (puzzle.squares[squareNum].candidates[0].ToCharArray())[0];
                puzzle.squares[squareNum].number = solution;
                used = true;
                for (int j = 0; j < row.Squares.Count; j++)
                {
                    if (puzzle.squares[row.Squares[j]].number == '-')
                    {
                        puzzle.squares[row.Squares[j]].candidates.Remove(solution.ToString());
                        if(puzzle.squares[row.Squares[j]].candidates.Count == 1 && puzzle.squares[row.Squares[j]].number == '-')
                        {
                            SingleCandidate temp = new SingleCandidate();
                            temp.Solve(row.Squares[j], puzzle, grid);
                            SinglesUsed++;
                            SinglesUsed += temp.SinglesUsed;
                        }
                    }
                    if (puzzle.squares[block.Squares[j]].number == '-' && puzzle.squares[block.Squares[j]].candidates.Contains(solution.ToString()))
                    {
                        puzzle.squares[block.Squares[j]].candidates.Remove(solution.ToString());
                        if (puzzle.squares[block.Squares[j]].candidates.Count == 1 && puzzle.squares[block.Squares[j]].number == '-')
                        {
                            SingleCandidate temp = new SingleCandidate();
                            temp.Solve(row.Squares[j], puzzle, grid);
                            SinglesUsed++;
                            SinglesUsed += temp.SinglesUsed;
                        }
                    }
                    if (puzzle.squares[col.Squares[j]].number == '-')
                    {
                        puzzle.squares[col.Squares[j]].candidates.Remove(solution.ToString());
                        if (puzzle.squares[col.Squares[j]].candidates.Count == 1 && puzzle.squares[col.Squares[j]].number == '-')
                        {
                            SingleCandidate temp = new SingleCandidate();
                            temp.Solve(row.Squares[j], puzzle, grid);
                            SinglesUsed++;
                            SinglesUsed += temp.SinglesUsed;
                        }
                    }
                }
            }
            return used;
        }
    }
}
