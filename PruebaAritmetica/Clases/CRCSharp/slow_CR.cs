using System.Numerics;

namespace PruebaAritmetica.Clases.CRCSharp
{
    internal class slow_CR : CR
    {
        int max_prec = -64;
        int prec_incr = 32;

        public override BigInteger approximate(int prec)
        {
            throw new NotImplementedException();
        }

        public BigInteger get_appr(int prec)
        {
            check_prec(prec);
            if (appr_valid && prec >= min_prec)
                return scale(max_appr, min_prec - prec);
            else
            {
                int eval_prec = (prec >= max_prec ? max_prec : (prec - prec_incr + 1) & ~(prec_incr - 1));
                BigInteger result = approximate(eval_prec);
                min_prec = eval_prec;
                max_appr = result;
                appr_valid = true;
                return scale(result, eval_prec - prec);
            }
        }
    }
}
