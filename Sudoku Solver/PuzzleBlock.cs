using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver
{
    public class PuzzleBlock
    {
        public int index { get; set; }
        public List<int> Squares { get; set; }
        public List<int> Rows { get; set; }
        public List<int> Columns { get; set; }

        public PuzzleBlock(int ind, int puzzleSize)
        {
            int blockSize = Convert.ToInt32(Math.Sqrt(puzzleSize));
            index = ind;
            int blockRow = index / blockSize;
            int blockCol = index - (blockSize * blockRow);
            List<int> initialNumbers = new List<int>();
            for(int i = 0; i < blockSize; i++)
            {
                for(int j = 0; j < blockSize; j++)
                {
                    initialNumbers.Add(j + (i * puzzleSize));
                }
            }
            Squares = new List<int>();
            for(int i = 0; i < initialNumbers.Count; i++)
            {
                Squares.Add((initialNumbers[i] + (blockSize * blockCol)) + (blockRow * (blockSize * puzzleSize)));
            }
            Rows = new List<int>();
            for(int i = 0; i < blockSize; i++)
            {
                Rows.Add(i + (blockSize * blockRow));
            }
            Columns = new List<int>();
            for (int i = 0; i < blockSize; i++)
            {
                Columns.Add(i + (blockSize * blockCol));
            }
        }
    }
}
