using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver
{
    class SingleCandidate : SolutionAlgorithm
    {
        List<char> duplicates { get; set; }
        PuzzleRow row { get; set; }
        PuzzleBlock block { get; set; }
        PuzzleColumn col { get; set; }

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

        public override void updateSurroundings(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            char solution;
            if(puzzle.squares[squareNum].candidates.Count == 1)
            {
                solution = (puzzle.squares[squareNum].candidates[0].ToCharArray())[0];
                puzzle.squares[squareNum].number = (puzzle.squares[squareNum].candidates[0].ToCharArray())[0];                
                for (int j = 0; j < row.Squares.Count; j++)
                {
                    if (puzzle.squares[row.Squares[j]].number == '-')
                    {
                        puzzle.squares[row.Squares[j]].candidates.Remove(solution.ToString());
                        if(puzzle.squares[row.Squares[j]].candidates.Count == 1)
                        {
                            Console.WriteLine("backtrack " + row.Squares[j]);
                            new SingleCandidate().Solve(row.Squares[j], puzzle, grid);
                        }
                    }
                    if (puzzle.squares[block.Squares[j]].number == '-' && puzzle.squares[block.Squares[j]].candidates.Contains(solution.ToString()))
                    {
                        puzzle.squares[block.Squares[j]].candidates.Remove(solution.ToString());
                        if (puzzle.squares[block.Squares[j]].candidates.Count == 1)
                        {
                            Console.WriteLine("backtrack " + block.Squares[j]);
                            new SingleCandidate().Solve(block.Squares[j], puzzle, grid);
                        }
                    }
                    if (puzzle.squares[col.Squares[j]].number == '-')
                    {
                        puzzle.squares[col.Squares[j]].candidates.Remove(solution.ToString());
                        if (puzzle.squares[col.Squares[j]].candidates.Count == 1)
                        {
                            Console.WriteLine("backtrack " + col.Squares[j]);
                            new SingleCandidate().Solve(col.Squares[j], puzzle, grid);
                        }
                    }
                }
            }

        }
    }
}
