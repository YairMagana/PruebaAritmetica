using System.Numerics;

namespace PruebaAritmetica.Clases.CRCSharp
{
    internal class assumed_int_CR : CR
    {
        CR _x;

        public assumed_int_CR(CR x)
        {
            _x = x;
        }

        public override BigInteger approximate(int prec)
        {
            if (prec >= 0)
                return _x.get_appr(prec);
            else
                return scale(_x.get_appr(0), -prec);
        }
    }
}
