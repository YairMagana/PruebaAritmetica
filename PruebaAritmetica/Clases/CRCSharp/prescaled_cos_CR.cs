using System.Numerics;

namespace PruebaAritmetica.Clases.CRCSharp
{
    internal class prescaled_cos_CR : slow_CR
    {
        CR _x;

        public prescaled_cos_CR(CR x)
        {
            _x = x;
        }

        public override BigInteger approximate(int prec)
        {
            if (prec >= 1) return big0;
            int iterations_needed = -prec / 2 + 4;
            int calc_precision = prec - bound_log2(2 * iterations_needed) - 4;
            int op_prec = prec - 2;
            BigInteger op_appr = _x.get_appr(op_prec);
            BigInteger current_term;
            int n;
            BigInteger max_trunc_error = big1 << (prec - 4 - calc_precision);
            n = 0;
            current_term = big1 << (-calc_precision);
            BigInteger current_sum = current_term;

            while (BigInteger.Abs(current_term) >= max_trunc_error)
            {
                // ******************************************************
                // Implementar Token de cancelación
                // ******************************************************
                n += 2;
                current_term = scale(current_term * op_appr, op_prec);
                current_term = scale(current_term * op_appr, op_prec);
                current_term /= (new BigInteger(-n)) * (new BigInteger(n - 1));
                current_sum += current_term;
            }

            return scale(current_sum, calc_precision - prec);
        }
    }
}
