using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sudoku_Solver
{
    public class PuzzleNumbers
    {
        public List<NumberSquare> squares { get; set; }
        public int size { get; set; }
        private List<string> masterCandidates { get; set; }
        
        public PuzzleNumbers(string inputFile)
        {
            squares = new List<NumberSquare>();
            string[] lines = File.ReadAllLines(inputFile);
            size = Convert.ToInt32(lines[0]);
            masterCandidates = new List<string>(lines[1].Split(' '));
            for(int i = 2; i < size + 2; i++)
            {
                string[] temp = lines[i].Split(' ');
                for(int j = 0; j < size; j++)
                {
                    char[] number = temp[j].ToCharArray();
                    squares.Add(new NumberSquare(number[0], masterCandidates));
                }
            }
        }
    }
}
