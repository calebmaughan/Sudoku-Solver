using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sudoku_Solver
{
    class PuzzleNumbers
    {
        public List<NumberSquare> squares { get; set; }
        public int size { get; set; }
        public List<string> masterCandidates { get; set; }
        
        public PuzzleNumbers(string inputFile)
        {
            //try
            //{
                squares = new List<NumberSquare>();
                string[] lines = File.ReadAllLines(inputFile);
                size = Convert.ToInt32(lines[0]);
                masterCandidates = new List<string>(lines[1].Split(' '));
                for(int i = 2; i < lines.Length; i++)
                {
                    string[] temp = lines[i].Split(' ');
                    for(int j = 0; j < size; j++)
                    {
                        char[] number = temp[j].ToCharArray();
                        squares.Add(new NumberSquare(number[0], masterCandidates));
                    }
                }
            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine("Invalid Puzzle");
                
            //}

        }

        public void debugWrite()
        {
            for(int i = 0; i < squares.Count; i++)
            {
                if(squares[i].number == '-')
                {
                    Console.Write(squares[i].number + " ");
                    for(int j = 0; j < squares[i].candidates.Count; j++)
                    {
                        Console.Write(squares[i].candidates[j] + " ");
                    }
                    Console.WriteLine();
                }
            }
        }    
    }
}
