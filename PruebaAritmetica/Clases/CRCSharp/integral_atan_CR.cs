using System.Numerics;

namespace PruebaAritmetica.Clases.CRCSharp
{
    internal class integral_atan_CR : slow_CR
    {
        int _x;

        public integral_atan_CR(int x)
        {
            _x = x;
        }

        public override BigInteger approximate(int prec)
        {
            if (prec >= 1) return big0;
            int iterations_needed = -prec / 2 + 2;
            int calc_precision = prec - bound_log2(2 * iterations_needed) - 2;
            BigInteger scaled_1 = big1 << -calc_precision;
            BigInteger big_op = _x;
            BigInteger big_op_squared = _x * _x;
            BigInteger op_inverse = scaled_1 / big_op;
            BigInteger current_power = op_inverse;
            BigInteger current_term = op_inverse;
            BigInteger current_sum = op_inverse;
            int current_sign = 1;
            int n = 1;
            BigInteger max_trunc_error = big1 << (prec - 2 - calc_precision);

            while (BigInteger.Abs(current_term) >= max_trunc_error)
            {
                // ******************************************************
                // Implementar Token de cancelación
                // ******************************************************
                n += 2;
                current_power = current_power / big_op_squared;
                current_sign = -current_sign;
                current_term = current_power / (current_sign * n);
                current_sum = current_sum + current_term;
            }
            return scale(current_sum, calc_precision - prec);
        }
    }
}
