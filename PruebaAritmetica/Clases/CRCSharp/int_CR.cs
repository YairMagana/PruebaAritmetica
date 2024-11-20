using System.Numerics;

namespace PruebaAritmetica.Clases.CRCSharp
{
    internal class int_CR : CR
    {
        BigInteger value;

        public int_CR(BigInteger n)
        {
            value = n;
        }

        public override BigInteger approximate(int prec)
        {
            return scale(value, -prec);
        }
    }
}
