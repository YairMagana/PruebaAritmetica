using System.Numerics;

namespace PruebaAritmetica.Clases.CRCSharp
{
    internal class inv_CR : CR
    {
        private CR _x;

        public inv_CR(CR x)
        {
            _x = x;
        }

        public override BigInteger approximate(int prec)
        {
            int msd = _x.msd();
            int inv_msd = 1 - msd;
            int digits_needed = inv_msd - prec + 3;
            int prec_needed = msd - digits_needed;
            int log_scale_factor = -prec - prec_needed;
            if (log_scale_factor < 0) return big0;
            BigInteger scaled_divisor = _x.get_appr(prec_needed);
            BigInteger abs_scaled_divisor = BigInteger.Abs(scaled_divisor);
            //BigInteger adj_dividend = (big1 << log_scale_factor) + (abs_scaled_divisor >> 1);

            BigInteger result = ((big1 << log_scale_factor) + (abs_scaled_divisor >> 1)) / abs_scaled_divisor;
            if (scaled_divisor.Sign < 0)
                return -result;
            else
                return result;
        }
    }
}