using System.Numerics;

namespace PruebaAritmetica.Clases.CRCSharp
{
    internal class prescaled_exp_CR : CR
    {
        private CR _x;

        public prescaled_exp_CR(CR x)
        {
            _x = x;
        }

        public override BigInteger approximate(int prec)
        {
            if (prec >= 1) return big0;
            int iterations_needed = -prec / 2 + 2;
            int calc_precision = prec - bound_log2(2 * iterations_needed) - 4;
            int op_prec = prec - 3;
            BigInteger op_appr = _x.get_appr(op_prec);
            BigInteger scaled_1 = big1 << -calc_precision;
            BigInteger current_term = scaled_1;
            BigInteger current_sum = scaled_1;
            int n = 0;
            BigInteger max_trunc_error = big1 << (prec - 4 - calc_precision);
            while (BigInteger.Abs(current_term) >= max_trunc_error)
            {
                // ******************************************************
                // Implementar Token de cancelación
                // ******************************************************
                n += 1;
                current_term = scale(current_term * op_appr, op_prec);
                current_term /= n;
                current_sum += current_term;
            }
            return scale(current_sum, calc_precision - prec);
        }
    }
}