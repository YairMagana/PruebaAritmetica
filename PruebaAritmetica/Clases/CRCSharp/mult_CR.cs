using System.Numerics;

namespace PruebaAritmetica.Clases.CRCSharp
{
    internal class mult_CR : CR
    {
        CR _x;
        CR _y;

        public mult_CR(CR x, CR y)
        {
            _x = x;
            _y = y;
        }

        public override BigInteger approximate(int prec)
        {
            int half_prec = (prec >> 1) - 1;
            int msd_op1 = _x.msd(half_prec);
            int msd_op2;

            if (msd_op1 == int.MinValue)
            {
                msd_op2 = _y.msd(half_prec);
                if (msd_op2 == int.MinValue)
                    return big0;
                else
                {
                    CR tmp;
                    tmp = _x;
                    _x = _y;
                    _y = tmp;
                    msd_op1 = msd_op2;
                }
            }

            int prec2 = prec - msd_op1 - 3;
            BigInteger appr2 = _y.get_appr(prec2);
            if (appr2.Sign == 0) return big0;
            msd_op2 = _y.known_msd();
            int prec1 = prec - msd_op2 - 3;
            BigInteger appr1 = _x.get_appr(prec1);
            int scale_digits = prec1 + prec2 - prec;
            return scale(appr1 * appr2, scale_digits);
        }
    }
}
