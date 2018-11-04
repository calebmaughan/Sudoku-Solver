using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver
{
    public class PuzzleStructure
    {
        public List<PuzzleBlock> blocks { get; set; }
        public List<PuzzleRow> rows { get; set; }
        public List<PuzzleColumn> columns { get; set; }

        public PuzzleStructure(int size)
        {
            blocks = new List<PuzzleBlock>();
            rows = new List<PuzzleRow>();
            columns = new List<PuzzleColumn>();
            for (int i = 0; i < size; i++)
            {
                blocks.Add(new PuzzleBlock(i, size));
                rows.Add(new PuzzleRow(i, size));
                columns.Add(new PuzzleColumn(i, size));
            }
        }

        public void debugWrite()
        {
            for(int i = 0; i < columns[1].Squares.Count; i++)
            {
                Console.Write(columns[1].Squares[i] + " ");
            }
        }
    }
}
