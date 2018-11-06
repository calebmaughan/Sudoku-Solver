using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver
{
    public class NumberSquare
    {
        public List<string> candidates { get; set; }
        public char number { get; set; }

        public NumberSquare(char num, List<String> master)
        {
            number = num;
            if(!master.Contains(num.ToString()) && num != '-')
            {
                throw new Exception();
            }
            if(num == '-')
            {
                candidates = new List<string>(master);
            }
        }
        public void removeCandidate(string cand)
        {
            if (candidates.Contains(cand))
            {
                candidates.Remove(cand);
            }
        }
    }
}
