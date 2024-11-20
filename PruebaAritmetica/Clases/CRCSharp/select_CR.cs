using System.Numerics;

namespace PruebaAritmetica.Clases.CRCSharp
{
    internal class select_CR : CR
    {
        private CR selector;
        private CR _x;
        private CR _y;

        int selector_sign;

        public select_CR(CR cR, CR x, CR y)
        {
            selector = cR;
            selector_sign = selector.get_appr(-20).Sign;
            _x = x;
            _y = y;
        }

        public override BigInteger approximate(int prec)
        {
            if (selector_sign < 0) return _x.get_appr(prec);
            if (selector_sign > 0) return _y.get_appr(prec);
            BigInteger x_appr = _x.get_appr(prec - 1);
            BigInteger y_appr = _y.get_appr(prec - 1);
            BigInteger diff = BigInteger.Abs(x_appr - y_appr);
            if (diff <= big1)
                return scale(x_appr, -1);

            if (selector.signum() < 0)
            {
                selector_sign = -1;
                return scale(x_appr, -1);
            }
            else
            {
                selector_sign = 1;
                return scale(y_appr, -1);
            }
        }
    }
}