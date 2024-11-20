using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAritmetica.Clases.CRCSharp
{
    public class StringFloatRep
    {
        public int sign;
        public string mantissa;
        public int radix;
        public int exponent;

        public StringFloatRep(int s, String m, int r, int e)
        {
            sign = s;
            mantissa = m;
            radix = r;
            exponent = e;
        }

        public string toString()
        {
            return $"{(sign < 0 ? "-" : "")}{mantissa}E{exponent}{(radix == 10 ? "" : $"(radix {radix})")}";
        }
    }
}
