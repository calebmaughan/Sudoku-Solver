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

        public Dictionary<string, int> findContainers(int square)
        {
            Dictionary<string, int> returnList = new Dictionary<string, int>();
            for(int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].Squares.Contains(square))
                {
                    returnList.Add("Block", i);
                }
                if (rows[i].Squares.Contains(square))
                {
                    returnList.Add("Row", i);
                }
                if (columns[i].Squares.Contains(square))
                {
                    returnList.Add("Column", i);
                }
            }
            return returnList;
        }
    }
}
