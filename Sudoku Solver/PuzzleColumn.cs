using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver
{
    public class PuzzleColumn
    {
        public int index { get; set; }
        public List<int> Squares { get; set; }
        public List<int> Blocks { get; set; }

        public PuzzleColumn(int ind, int puzzleSize)
        {
            int blockSize = Convert.ToInt32(Math.Sqrt(puzzleSize));
            index = ind;
            Squares = new List<int>();
            for (int i = 0; i < puzzleSize; i++)
            {
                Squares.Add((i * puzzleSize) + index);
            }
            Blocks = new List<int>();
            for (int i = 0; i < blockSize; i++)
            {
                Blocks.Add((i * blockSize) + (index/blockSize));
            }
        }
    }
}
