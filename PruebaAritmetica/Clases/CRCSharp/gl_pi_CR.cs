using System.Numerics;

namespace PruebaAritmetica.Clases.CRCSharp
{
    internal class gl_pi_CR : slow_CR
    {
        List<int> b_prec = new List<int>();
        List<BigInteger> b_val = new List<BigInteger>();

        private static readonly BigInteger TOLERANCE = new BigInteger(4);
        private static readonly CR SQRT_HALF = new sqrt_CR(one.shiftRight(1));

        public gl_pi_CR()
        {
            b_prec.Add(0);
            b_val.Add(0);
        }

        public override BigInteger approximate(int prec)
        {
            if (b_prec.Count > b_val.Count)
                b_prec.RemoveAt(b_prec.Count - 1);
            if (prec >= 0) return scale(new BigInteger(3), -prec);
            int extra_eval_prec = (int)Math.Ceiling(Math.Log(-prec) / Math.Log(2)) + 10;
            int eval_prec = prec - extra_eval_prec;
            BigInteger a = big1 << -eval_prec;
            BigInteger b = SQRT_HALF.get_appr(eval_prec);
            BigInteger t = big1 << (-eval_prec - 2);
            int n = 0;
            while (a - b > TOLERANCE)
            {
                BigInteger next_a = (a + b) >> 1;
                BigInteger a_diff = a - next_a;
                BigInteger b_prod = (a * b) >> -eval_prec;
                CR b_prod_as_CR = valueOf(b_prod).shiftRight(-eval_prec);
                BigInteger next_b;
                if (b_prec.Count == n + 1)
                {
                    next_b = b_prod_as_CR.sqrt().get_appr(eval_prec);
                    b_prec.Add(prec);
                    b_val.Add(scale(next_b, -extra_eval_prec));
                }
                else
                {
                    next_b = new sqrt_CR(b_prod_as_CR, b_prec[n + 1], b_val[n + 1]).get_appr(eval_prec);
                    b_prec[n + 1] = prec;
                    b_val[n + 1] = scale(next_b, -extra_eval_prec);
                }

                t -= (a_diff * a_diff) << (n + eval_prec);
                a = next_a;
                b = next_b;
                ++n;
            }
            BigInteger sum = a + b;
            BigInteger result = (sum * sum / t) >> 2;
            return scale(result, -extra_eval_prec);
        }
    }
}
