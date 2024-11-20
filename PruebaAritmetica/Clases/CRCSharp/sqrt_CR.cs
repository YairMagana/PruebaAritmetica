using System.Numerics;

namespace PruebaAritmetica.Clases.CRCSharp
{
    internal class sqrt_CR : CR
    {
        int fp_prec = 50;
        int fp_op_prec = 60;

        private CR _x;

        public sqrt_CR(CR x)
        {
            _x = x;
        }

        public sqrt_CR(CR x, int min_p, BigInteger max_a)
        {
            _x = x;
            min_prec = min_p;
            max_appr = max_a;
            appr_valid = true;
        }

        public override BigInteger approximate(int prec)
        {
            int max_op_prec_needed = 2 * prec - 1;
            int msd = _x.iter_msd(max_op_prec_needed);
            if (msd <= max_op_prec_needed) return big0;
            int result_msd = msd / 2;
            int result_digits = result_msd - prec;
            if (result_digits > fp_prec)
            {
                int appr_digits = result_digits / 2 + 6;
                int appr_prec = result_msd - appr_digits;
                int prod_prec = 2 * appr_prec;
                BigInteger op_appr = _x.get_appr(prod_prec);
                BigInteger last_appr = get_appr(appr_prec);
                BigInteger prod_prec_scaled_numerator = (last_appr * last_appr) + op_appr;
                BigInteger scaled_numerator = scale(prod_prec_scaled_numerator, appr_prec - prec);
                BigInteger shifted_result = scaled_numerator / last_appr;
                return (shifted_result + big1) >> 1;
            }
            else
            {
                int op_prec = (msd - fp_op_prec) & ~1;
                int working_prec = op_prec - fp_op_prec;
                BigInteger scaled_bi_appr = _x.get_appr(op_prec) << fp_op_prec;
                double scaled_appr = (double)scaled_bi_appr;
                if (scaled_appr < 0d) throw new ArithmeticException("sqrt(negative)");
                double scaled_fp_sqrt = Math.Sqrt(scaled_appr);
                BigInteger scaled_sqrt = new BigInteger((long)scaled_fp_sqrt);
                int shift_count = working_prec / 2 - prec;
                return shift(scaled_sqrt, shift_count);
            }
        }
    }
}