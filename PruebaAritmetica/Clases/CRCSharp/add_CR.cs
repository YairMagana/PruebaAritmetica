using System.Numerics;

namespace PruebaAritmetica.Clases.CRCSharp
{
    internal class add_CR : CR
    {
        CR _x;
        CR _y;

        public add_CR(CR x, CR y)
        {
            _x = x;
            _y = y;
        }

        public override BigInteger approximate(int prec)
        {
            return scale(_x.get_appr(prec - 2) + _y.get_appr(prec - 2), -2);
        }
    }
}
