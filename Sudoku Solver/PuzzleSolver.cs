using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver
{
    public class PuzzleSolver
    {
        PuzzleNumbers puzzle { get; set; }
        PuzzleStructure structure { get; set; }
        bool invalid { get; set; }

        public PuzzleSolver(string input)
        {
            invalid = false;
            try
            {
                puzzle = new PuzzleNumbers(input);

                //puzzle.debugWrite();

                structure = new PuzzleStructure(puzzle.size);

                //structure.debugWrite();
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Puzzle");
                invalid = true;
            }

            Solve();
        }

        public void Solve()
        {
            if (!invalid)
            {
                for (int i = 0; i < puzzle.squares.Count; i++)
                {
                    if (puzzle.squares[i].number == '-')
                    {
                        new SingleCandidate().Solve(i, puzzle, structure);
                    }
                }
                for (int i = 0; i < puzzle.squares.Count; i++)
                {
                    Console.Write(puzzle.squares[i].number + " ");
                    if ((i + 1) % puzzle.size == 0)
                    {
                        Console.Write("\n");
                    }
                }
                Console.WriteLine("");
                bool used = true;
                bool solved = false;
                int timesused = 0;
                while (!solved && used)
                {                    
                    solved = true;
                    used = false;
                    for (int i = 0; i < puzzle.squares.Count; i++)
                    {
                        if (puzzle.squares[i].number == '-')
                        {
                            solved = false;
                            new UniqueCandidate().Solve(i, puzzle, structure);
                            if(puzzle.squares[i].number != '-')
                            {
                                used = true;
                                timesused++;
                                //Console.WriteLine(i);
                            }
                        }
                    }                    
                }

                for(int i = 0; i < puzzle.squares.Count; i++)
                {
                    new HiddenPair().Solve(i, puzzle, structure);
                }

                used = true;
                solved = false;
                //int timesused = 0;
                while (!solved && used)
                {
                    solved = true;
                    used = false;
                    for (int i = 0; i < puzzle.squares.Count; i++)
                    {
                        if (puzzle.squares[i].number == '-')
                        {
                            solved = false;
                            new UniqueCandidate().Solve(i, puzzle, structure);
                            if (puzzle.squares[i].number != '-')
                            {
                                used = true;
                                timesused++;
                                //Console.WriteLine(i);
                            }
                        }
                    }
                }
                Console.WriteLine("Solved: " + solved);
                Console.WriteLine("Unique Candidate used " + timesused + " times.");
                for (int i = 0; i < puzzle.squares.Count; i++)
                {
                    Console.Write(puzzle.squares[i].number + " ");
                    if ((i + 1) % puzzle.size == 0)
                    {
                        Console.Write("\n");
                    }
                }
            }
        }
    }
}
