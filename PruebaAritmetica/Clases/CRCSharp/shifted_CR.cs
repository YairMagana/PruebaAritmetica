using System.Numerics;

namespace PruebaAritmetica.Clases.CRCSharp
{
    internal class shifted_CR : CR
    {
        CR _x;
        int _n;

        public shifted_CR(CR x, int n)
        {
            _x = x;
            _n = n;
        }

        public override BigInteger approximate(int prec)
        {
            return _x.get_appr(prec - _n);
        }
    }
}
