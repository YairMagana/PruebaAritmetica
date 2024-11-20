using System.Numerics;

namespace PruebaAritmetica.Clases.CRCSharp
{
    internal class prescaled_asin_CR : CR
    {
        private CR _x;

        public prescaled_asin_CR(CR x)
        {
            _x = x;
        }

        public override BigInteger approximate(int prec)
        {
            if (prec >= 2) return big0;
            int iterations_needed = -3 * prec / 2 + 4;
            int calc_precision = prec - bound_log2(2 * iterations_needed) - 4;
            int op_prec = prec - 3;
            BigInteger op_appr = _x.get_appr(op_prec);
            BigInteger max_last_term = big1 << (prec - 4 - calc_precision);
            int exp = 1;
            BigInteger current_term = op_appr << (op_prec - calc_precision);
            BigInteger current_sum = current_term;
            BigInteger current_factor = current_term;
            while (BigInteger.Abs(current_term) >= max_last_term)
            {
                // ******************************************************
                // Implementar Token de cancelación
                // ******************************************************
                exp += 2;
                current_factor *= new BigInteger(exp - 2);
                current_factor = scale(current_factor * op_appr, op_prec + 2);
                current_factor *= op_appr;
                current_factor /= new BigInteger(exp - 1);
                current_factor = scale(current_factor, op_prec - 2);
                current_term = current_factor / new BigInteger(exp);
                current_sum += current_term;
            }
            return scale(current_sum, calc_precision - prec);
        }
    }
}