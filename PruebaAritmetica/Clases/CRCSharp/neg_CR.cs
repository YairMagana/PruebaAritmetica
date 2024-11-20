using System.Numerics;

namespace PruebaAritmetica.Clases.CRCSharp
{
    internal class neg_CR : CR
    {
        CR _x;

        public neg_CR(CR x)
        {
            _x = x;
        }

        public override BigInteger approximate(int prec)
        {
            return -_x.get_appr(prec);
        }
    }
}
