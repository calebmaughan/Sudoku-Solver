using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver
{
    public class UniqueCandidate : SolutionAlgorithm
    {
        Dictionary<char, int> blockCandidates { get; set; }
        Dictionary<char, int> rowCandidates { get; set; }
        Dictionary<char, int> colCandidates { get; set; }
        Dictionary<char, int> candidateCount { get; set; }

        public override void checkSurroundings(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            Dictionary<string, int> containers = grid.findContainers(squareNum);
            row = grid.rows[containers["Row"]];
            block = grid.blocks[containers["Block"]];
            col = grid.columns[containers["Column"]];
            blockCandidates = new Dictionary<char, int>();
            rowCandidates = new Dictionary<char, int>();
            colCandidates = new Dictionary<char, int>();
            for (int i = 0; i < row.Squares.Count; i++)
            {
                if (puzzle.squares[row.Squares[i]].number == '-' && row.Squares[i] != squareNum)
                {
                    for (int j = 0; j < puzzle.squares[row.Squares[i]].candidates.Count; j++)
                    {
                        if (!rowCandidates.ContainsKey(puzzle.squares[row.Squares[i]].candidates[j].ToCharArray()[0]))
                        {
                            rowCandidates.Add(puzzle.squares[row.Squares[i]].candidates[j].ToCharArray()[0], 1);
                        }
                        else
                        {
                            rowCandidates[puzzle.squares[row.Squares[i]].candidates[j].ToCharArray()[0]]++;
                        }
                    }
                }
                if (puzzle.squares[col.Squares[i]].number == '-' && col.Squares[i] != squareNum)
                {
                    for (int j = 0; j < puzzle.squares[col.Squares[i]].candidates.Count; j++)
                    {
                        if (!colCandidates.ContainsKey(puzzle.squares[col.Squares[i]].candidates[j].ToCharArray()[0]))
                        {
                            colCandidates.Add(puzzle.squares[col.Squares[i]].candidates[j].ToCharArray()[0], 1);
                        }
                        else
                        {
                            colCandidates[puzzle.squares[col.Squares[i]].candidates[j].ToCharArray()[0]]++;
                        }
                    }
                }
                if (puzzle.squares[block.Squares[i]].number == '-' && block.Squares[i] != squareNum)
                {
                    for (int j = 0; j < puzzle.squares[block.Squares[i]].candidates.Count; j++)
                    {
                        if (!blockCandidates.ContainsKey(puzzle.squares[block.Squares[i]].candidates[j].ToCharArray()[0]))
                        {
                            blockCandidates.Add(puzzle.squares[block.Squares[i]].candidates[j].ToCharArray()[0], 1);
                        }
                        else
                        {
                            blockCandidates[puzzle.squares[block.Squares[i]].candidates[j].ToCharArray()[0]]++;
                        }
                    }
                }
            }

        }

        public override void updateCandidates(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            candidateCount = new Dictionary<char, int>();

            for(int i = 0; i < puzzle.squares[squareNum].candidates.Count; i++)
            {
                candidateCount.Add(puzzle.squares[squareNum].candidates[i].ToCharArray()[0], 0);
            }
            Dictionary<char, int> tempCount = new Dictionary<char, int>(candidateCount);
            foreach(KeyValuePair<char, int> pair in rowCandidates)
            {
                if (tempCount.ContainsKey(pair.Key) && tempCount[pair.Key] == 0)
                {
                    tempCount.Remove(pair.Key);
                }                
            }
            if(tempCount.Count == 1)
            {
                candidateCount = tempCount;
                return;
            }
            tempCount = new Dictionary<char, int>(candidateCount);
            foreach (KeyValuePair<char, int> pair in colCandidates)
            {
                if (tempCount.ContainsKey(pair.Key) && tempCount[pair.Key] == 0)
                {
                    tempCount.Remove(pair.Key);
                }
            }
            if (tempCount.Count == 1)
            {
                candidateCount = tempCount;
                return;
            }
            tempCount = new Dictionary<char, int>(candidateCount);
            foreach (KeyValuePair<char, int> pair in blockCandidates)
            {
                if (tempCount.ContainsKey(pair.Key) && tempCount[pair.Key] == 0)
                {
                    tempCount.Remove(pair.Key);
                }
            }
            if (tempCount.Count == 1)
            {
                candidateCount = tempCount;
                return;
            }
        }

        public override bool updateSurroundings(int squareNum, PuzzleNumbers puzzle, PuzzleStructure grid)
        {
            bool used = false;
            if(candidateCount.Count == 1)
            {
                used = true;
                char answer = ' ';
                foreach (KeyValuePair<char, int> pair in candidateCount)
                {
                    answer = pair.Key;
                }
                puzzle.squares[squareNum].number = answer;
                for(int i = 0; i < row.Squares.Count; i++)
                {
                    if (puzzle.squares[row.Squares[i]].number == '-' && puzzle.squares[row.Squares[i]].candidates.Contains(answer.ToString()))
                    {
                        puzzle.squares[row.Squares[i]].candidates.Remove(answer.ToString());
                        if(puzzle.squares[row.Squares[i]].candidates.Count == 1)
                        {
                            SingleCandidate temp = new SingleCandidate();
                            temp.Solve(row.Squares[i], puzzle, grid);
                            SinglesUsed++;
                            SinglesUsed += temp.SinglesUsed;
                        }
                    }
                }
                for (int i = 0; i < col.Squares.Count; i++)
                {
                    if (puzzle.squares[col.Squares[i]].number == '-' && puzzle.squares[col.Squares[i]].candidates.Contains(answer.ToString()))
                    {
                        puzzle.squares[col.Squares[i]].candidates.Remove(answer.ToString());
                        if (puzzle.squares[col.Squares[i]].candidates.Count == 1)
                        {
                            SingleCandidate temp = new SingleCandidate();
                            temp.Solve(row.Squares[i], puzzle, grid);
                            SinglesUsed++;
                            SinglesUsed += temp.SinglesUsed;
                        }
                    }
                }
                for (int i = 0; i < block.Squares.Count; i++)
                {
                    if (puzzle.squares[block.Squares[i]].number == '-' && puzzle.squares[block.Squares[i]].candidates.Contains(answer.ToString()))
                    {
                        puzzle.squares[block.Squares[i]].candidates.Remove(answer.ToString());
                        if (puzzle.squares[block.Squares[i]].candidates.Count == 1)
                        {
                            SingleCandidate temp = new SingleCandidate();
                            temp.Solve(row.Squares[i], puzzle, grid);
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
