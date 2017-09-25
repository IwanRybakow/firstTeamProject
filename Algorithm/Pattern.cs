using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class Pattern
    {
        public int Weight { get; set; }
        public string PatternString
        {
            get
            {
                return PatternStringBuilder.ToString();
            }
        }
        public StringBuilder PatternStringBuilder { get; set; }
        public Pattern(int w)
        {
            Weight = w;
            PatternStringBuilder = new StringBuilder();
        }
    }
}
