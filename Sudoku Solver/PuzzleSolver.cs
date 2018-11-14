using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Sudoku_Solver
{
    public class PuzzleSolver
    {
        List<NumberSquare> originalPuzzle { get; set; }
        PuzzleNumbers puzzle { get; set; }
        PuzzleStructure structure { get; set; }
        bool invalid { get; set; }
        Dictionary <string, SolutionAlgorithm> algorithms { get; set; }
        Dictionary <string, int> timesUsed { get; set; }
        Dictionary <string, double> timeSpent { get; set; }
        List<string> names { get; set; }
        double totalTime { get; set; }
        bool guess { get; set; }
        bool solved { get; set; }
        int possibleSolutions { get; set; }
        List<List<NumberSquare>> mySolution { get; set; }
        int currentDepth { get; set; }
        List<List<NumberSquare>> currentSolvage { get; set; }
        string output { get; set; }

        public PuzzleSolver(string input, string output_)
        {
            bool written = false;
            invalid = true;
            try
            {
                puzzle = new PuzzleNumbers(input);
                structure = new PuzzleStructure(puzzle.size);
                mySolution = new List<List<NumberSquare>>();
                possibleSolutions = 0;
                currentDepth = -1;
                currentSolvage = new List<List<NumberSquare>>();
                output = output_;
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Puzzle");
                Console.WriteLine("Input not correct");
                Console.Read();
                invalid = false;
                written = true;
            }
            InitializeDictionarys();
            if (invalid)
            {
                originalPuzzle = Clone(puzzle.squares) as List<NumberSquare>;
                Console.WriteLine("");
                invalid = Solve();
            }

            if (invalid)
            {
                writePuzzle();
            }
            else if(!written)
            {
                Console.WriteLine("Invalid Puzzle");
                Console.WriteLine("Unsolvable");
                Console.Read();
            }
        }

        void InitializeDictionarys()
        {
            algorithms = new Dictionary<string, SolutionAlgorithm>();
            algorithms.Add("Single Candidate", new SingleCandidate());
            algorithms.Add("Unique Candidate", new UniqueCandidate());
            algorithms.Add("Hidden Pairs", new HiddenPair());
            algorithms.Add("Block Row Elimination", new BlockRows());
            algorithms.Add("Block Column Elimination", new BlockCols());
            algorithms.Add("Column Elimination", new ColumnElimination());
            algorithms.Add("Row Elimination", new RowElimination());

            timesUsed = new Dictionary<string, int>();
            timesUsed.Add("Single Candidate", 0);
            timesUsed.Add("Unique Candidate", 0);
            timesUsed.Add("Hidden Pairs", 0);
            timesUsed.Add("Block Row Elimination", 0);
            timesUsed.Add("Block Column Elimination", 0);
            timesUsed.Add("Column Elimination", 0);
            timesUsed.Add("Row Elimination", 0);
            timesUsed.Add("Guess", 0);

            timeSpent = new Dictionary<string, double>();
            timeSpent.Add("Single Candidate", 0);
            timeSpent.Add("Unique Candidate", 0);
            timeSpent.Add("Hidden Pairs", 0);
            timeSpent.Add("Block Row Elimination", 0);
            timeSpent.Add("Block Column Elimination", 0);
            timeSpent.Add("Column Elimination", 0);
            timeSpent.Add("Row Elimination", 0);
            timeSpent.Add("Guess", 0);

            names = new List<string>();
            foreach (KeyValuePair<string, SolutionAlgorithm> pair in algorithms)
            {
                names.Add(pair.Key);
            }
        }

        public bool Solve()
        {
            guess = false;
            for (int i = 0; i < puzzle.squares.Count; i++)
            {
                if (puzzle.squares[i].number == '-')
                {
                    Stopwatch watch = Stopwatch.StartNew();
                    bool temp = algorithms[names[0]].Solve(i, puzzle, structure);
                    watch.Stop();
                    double time = watch.ElapsedMilliseconds;
                    timeSpent["Single Candidate"] += time;
                    if (temp)
                    {
                        timesUsed[names[0]]++;
                    }
                }
            }
            solved = false;
            int currentAlgorithm = 1;
            while (!solved)
            {

                bool used = true;
                while (used)
                {
                    solved = true;
                    used = false;
                    for (int i = 0; i < puzzle.squares.Count; i++)
                    {
                        if (puzzle.squares[i].number == '-')
                        {
                            solved = false;
                            string algo = names[currentAlgorithm];
                            Stopwatch watch = Stopwatch.StartNew();
                            bool temp = algorithms[algo].Solve(i, puzzle, structure);
                            watch.Stop();
                            double time = watch.ElapsedMilliseconds;
                            timeSpent[algo] += time;
                            if (temp)
                            {
                                used = true;
                                timesUsed[algo]++;
                            }
                        }
                    }
                    if (used && currentAlgorithm > 1)
                    {
                        currentAlgorithm = 1;
                    }
                }
                currentAlgorithm++;
                if (currentAlgorithm == algorithms.Count)
                {
                    solved = true;
                    guess = true;
                }
            }

            if (guess)
            {
                timesUsed["Guess"]++;
                solved = false;
                Stopwatch watch = Stopwatch.StartNew();
                solved = Guess();
                watch.Stop();
                timeSpent["Guess"] += watch.ElapsedMilliseconds;
                if (solved)
                {
                    puzzle.squares = mySolution[0];
                }
            }


            return solved;            
        }

        public bool Guess()
        {
            currentDepth++;
            bool returnVal = false;
            currentSolvage.Add(new List<NumberSquare>());
            currentSolvage[currentDepth] = Clone(puzzle.squares) as List<NumberSquare>;
            //currentSolvage.Add(new List<NumberSquare>(puzzle.squares));
            int leastCandidates = puzzle.size;
            for (int i = 0; i < puzzle.squares.Count; i++)
            {
                if(puzzle.squares[i].number == '-' && puzzle.squares[i].candidates.Count < leastCandidates)
                {
                    leastCandidates = puzzle.squares[i].candidates.Count;
                }
            }
            if(leastCandidates == 0)
            {
                currentSolvage.Remove(currentSolvage[currentDepth]);
                currentDepth--;
                return false;
            }
            bool squareFound = false;
            int index = 0;
            while (!squareFound)
            {
                if(puzzle.squares[index].number == '-' && puzzle.squares[index].candidates.Count == leastCandidates)
                {
                    squareFound = true;
                }
                else
                {
                    index++;
                }
            }
            if(index == 16)
            {

            }
            for(int i = 0; i < leastCandidates; i++)
            {
                puzzle.squares = Clone(currentSolvage[currentDepth]) as List<NumberSquare>;
                puzzle.squares[index].number = puzzle.squares[index].candidates[i].ToCharArray()[0];
                returnVal = Solve();
                if (returnVal)
                {
                    List<NumberSquare> temp = Clone(puzzle.squares) as List<NumberSquare>;
                    int different = 0;
                    for (int j = 0; j < mySolution.Count; j++)
                    {
                        bool currentsame = true;
                        int index1 = 0;
                        while(currentsame && index1 < mySolution.Count)
                        {
                            if(temp[index1].number != mySolution[j][index1].number)
                            {
                                currentsame = false;
                            }
                            index1++;
                        }
                        if (!currentsame)
                        {
                            different++;
                        }
                    }
                    if(different == mySolution.Count)
                    {
                        mySolution.Add(Clone(temp) as List<NumberSquare>);
                    }
                }
                currentSolvage[currentDepth][index].number = '-';
            }
            currentSolvage.RemoveAt(currentDepth);
            currentDepth--;
            if (mySolution.Count != 1)
            {
                returnVal = false;
            }
            else
            {
                returnVal = true;
            }
            return returnVal;
        }

        public void writePuzzle() {
            string write = "";
            for (int i = 0; i < originalPuzzle.Count; i++)
            {
                write += originalPuzzle[i].number + " ";
                if ((i + 1) % puzzle.size == 0)
                {
                    write += "\n";
                }
            }
            write += "\n";
            //write the final puzzle
            for (int i = 0; i < puzzle.squares.Count; i++)
            {
                write += puzzle.squares[i].number + " ";
                if ((i + 1) % puzzle.size == 0)
                {
                    write += "\n";
                }
            }
            foreach (KeyValuePair<string, SolutionAlgorithm> pair in algorithms)
            {
                timesUsed["Single Candidate"] += pair.Value.SinglesUsed;
                totalTime += timeSpent[pair.Key];
            }
            write += "\nTotal time: " + totalTime / 1000 + "\n";
            for (int i = 0; i < algorithms.Count; i++)
            {
                write += names[i] + " - Times Used: " + timesUsed[names[i]] + " - " + timeSpent[names[i]] / 1000 + " seconds\n";
            }
            write+= "Guess - Times Used: " + timesUsed["Guess"] + " - " + timeSpent["Guess"] / 1000 + " seconds";
            if (output == "")
            {
                Console.WriteLine(write);
                Console.Read();
            }
            else
            {
                try
                {
                    File.WriteAllLines(output, write.Split("\n"));
                    Console.WriteLine("Done");
                    Console.Read();
                }
                catch
                {
                    Console.WriteLine(write);
                    Console.Read();
                }
            }
        }

        public static object Clone(object obj)
        {
            object objResult = null;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);

                ms.Position = 0;
                objResult = bf.Deserialize(ms);
            }
            return objResult;
        }
    }
}
