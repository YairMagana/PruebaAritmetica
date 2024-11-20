using System.Numerics;

namespace PruebaAritmetica.Clases.CRCSharp
{
    public abstract class CR
    {
        public static readonly BigInteger bigm750 = new BigInteger(-750);
        public static readonly BigInteger bigm2 = new BigInteger(-2);
        public static readonly BigInteger bigm1 = BigInteger.MinusOne;
        public static readonly BigInteger big0 = BigInteger.Zero;
        public static readonly BigInteger big1 = BigInteger.One;
        public static readonly BigInteger big2 = new BigInteger(2);
        public static readonly BigInteger big3 = new BigInteger(3);
        public static readonly BigInteger big6 = new BigInteger(6);
        public static readonly BigInteger big8 = new BigInteger(8);
        public static readonly BigInteger big10 = new BigInteger(10);
        public static readonly BigInteger big750 = new BigInteger(750);

        public BigInteger max_appr;
        public int min_prec;
        public bool appr_valid = false;

        public static CR one = valueOf(1);
        static CR two = valueOf(2);
        static CR four = valueOf(4);
        public static CR PI = new gl_pi_CR();
        public static CR atan_PI = four.multiply(four.multiply(atan_reciprocal(5)).subtract(atan_reciprocal(239)));
        static CR half_PI = PI.shiftRight(1);

        // Métodos abstractos
        public abstract BigInteger approximate(int prec);

        #region [Métodos miembros de CR]
        #region[    Métodos de manejo de dígitos]
        public int msd()
        {
            return iter_msd(int.MinValue);
        }

        public int msd(int n)
        {
            if (!appr_valid || max_appr <= big1 && max_appr >= bigm1)
            {
                get_appr(n - 1);
                if (BigInteger.Abs(max_appr) <= big1)
                    return int.MinValue;
            }
            return known_msd();
        }

        public int known_msd()
        {
            int length;

            if (max_appr.Sign >= 0)
                length = (int)max_appr.GetBitLength();
            else
                length = (int)(-max_appr).GetBitLength();

            return min_prec + length - 1;
        }

        public int iter_msd(int n)
        {
            int prec = 0;

            for (; prec > n + 30; prec = (prec * 3) / 2 - 16)
            {
                int _msd = msd(prec);
                if (_msd != int.MinValue) return _msd;
                check_prec(prec);
                // ******************************************************
                // Implementar Token de cancelación
                // ******************************************************
            }
            return msd(n);
        }
        #endregion

        #region [    Métodos de conversión]
        public string toString(int n, int radix)
        {
            CR scaled_CR;
            if (16 == radix)
                scaled_CR = shiftLeft(4 * n);
            else
            {
                BigInteger scale_factor = BigInteger.Pow(radix, n);
                if (scale_factor == 1)
                    scaled_CR = this;
                else
                    scaled_CR = multiply(valueOf(scale_factor));
            }

            BigInteger scaled_int = scaled_CR.get_appr(0);
            string scaled_string = Utils.ToStringWithRadix(BigInteger.Abs(scaled_int), radix);
            string result;
            if (0 == n)
                result = scaled_string;
            else
            {
                int len = scaled_string.Length;
                if (len <= n)
                {
                    string z = zeroes(n + 1 - len);
                    scaled_string = z + scaled_string;
                    len = n + 1;
                }
                string whole = scaled_string.Substring(0, len - n);
                string fraction = scaled_string.Substring(len - n);
                result = whole + "." + fraction;
            }
            if (scaled_int.Sign < 0)
                result = "-" + result;
            return result;
        }

        public string toString(int n)
        {
            return toString(n, 10);
        }

        public string toString()
        {
            return toString(10);
        }
        #endregion

        #region [    Métodos de comparación]
        public int compareTo(CR x, int r, int a)
        {
            int this_msd = iter_msd(a);
            int x_msd = x.iter_msd(this_msd > a ? this_msd : a);
            int max_msd = (x_msd > this_msd ? x_msd : this_msd);
            if (max_msd == int.MinValue) return 0;
            check_prec(r);
            int rel = max_msd + r;

            int abs_prec = (rel > a ? rel : a);
            return compareTo(x, abs_prec);
        }

        public int compareTo(CR x, int a)
        {
            int needed_prec = a - 1;
            BigInteger this_appr = get_appr(needed_prec);
            BigInteger x_appr = x.get_appr(needed_prec);
            int comp1 = this_appr.CompareTo(x_appr + big1);
            if (comp1 > 0) return 1;
            int comp2 = this_appr.CompareTo(x_appr - big1);
            if (comp2 < 0) return -1;
            return 0;
        }

        public int compareTo(CR x)
        {
            for (int i = -20; ; i *= 2)
            {
                check_prec(i);
                int result = compareTo(x, i);
                if (result != 0) return result;
                // ******************************************************
                // Implementar Token de cancelación
                // ******************************************************
            }
        }
        #endregion

        #region [    Métodos entregadores]
        public BigInteger BigIntegerValue()
        {
            return get_appr(0);
        }

        public int intValue()
        {
            return (int)BigIntegerValue();
        }

        public long longValue()
        {
            return (long)BigIntegerValue();
        }

        public double doubleValue()
        {
            int my_msd = iter_msd(-1080);
            if (int.MinValue == my_msd) return 0d;
            int needed_prec = my_msd - 60;
            double scaled_int = (double)get_appr(needed_prec);
            bool may_underflow = (needed_prec < -1000);
            long scaled_int_rep = BitConverter.DoubleToInt64Bits(scaled_int);
            long exp_adj = may_underflow ? needed_prec + 96 : needed_prec;
            long orig_exp = (scaled_int_rep >> 52) & 0x7ff;
            if ((orig_exp + exp_adj) >= ~0x7ff)
            {
                if (scaled_int < 0.0)
                    return double.NegativeInfinity;
                else
                    return double.PositiveInfinity;
            }
            scaled_int_rep += exp_adj << 52;
            double result = BitConverter.Int64BitsToDouble(scaled_int_rep);
            if (may_underflow)
            {
                double two48 = (double)(1L << 48);
                return result / two48 / two48;
            }
            else
                return result;
        }

        public byte byteValue()
        {
            return (byte)BigIntegerValue();
        }

        public StringFloatRep toStringFloatRep(int n, int radix, int m)
        {
            if (n <= 0) throw new ArithmeticException("Bad precision argument");
            double log2_radix = Math.Log((double)radix) / Math.Log(2.0);
            BigInteger big_radix = new BigInteger(radix);
            long long_msd_prec = (long)(log2_radix * (double)m);
            if (long_msd_prec > (long)int.MaxValue || long_msd_prec < (long)int.MinValue)
                throw new PrecisionOverflowError();
            int msd_prec = (int)long_msd_prec;
            check_prec(msd_prec);
            int msd = iter_msd(msd_prec - 2);
            if (msd == int.MinValue) return new StringFloatRep(0, "0", radix, 0);
            int exponent = (int)Math.Ceiling((double)msd / log2_radix);
            int scale_exp = exponent - n;
            CR scale;
            if (scale_exp > 0)
                scale = valueOf(BigInteger.Pow(big_radix, scale_exp)).inverse();
            else
                scale = valueOf(BigInteger.Pow(big_radix, -scale_exp));
            CR scaled_res = multiply(scale);
            BigInteger scaled_int = scaled_res.get_appr(0);
            int sign = scaled_int.Sign;
            string scaled_string = Utils.ToStringWithRadix(BigInteger.Abs(scaled_int), radix);
            while (scaled_string.Length < n)
            {
                scaled_res = scaled_res.multiply(valueOf(big_radix));
                exponent -= 1;
                scaled_int = scaled_res.get_appr(0);
                sign = scaled_int.Sign;
                scaled_string = Utils.ToStringWithRadix(BigInteger.Abs(scaled_int), radix);
            }
            if (scaled_string.Length > n)
            {
                exponent += (scaled_string.Length - n);
                scaled_string = scaled_string.Substring(0, n);
            }
            return new StringFloatRep(sign, scaled_string, radix, exponent);
        }
        #endregion

        #region [    Métodos de operaciones aritméticas]
        public CR assumeInt()
        {
            return new assumed_int_CR(this);
        }

        public CR shiftLeft(int n)
        {
            check_prec(n);
            return new shifted_CR(this, n);
        }

        public CR shiftRight(int n)
        {
            check_prec(n);
            return new shifted_CR(this, -n);
        }

        public CR negate()
        {
            return new neg_CR(this);
        }

        public CR abs()
        {
            return select(negate(), this);
        }

        public CR max(CR x)
        {
            return subtract(x).select(x, this);
        }

        public CR min(CR x)
        {
            return subtract(x).select(this, x);
        }

        public int signum(int a)
        {
            if (appr_valid)
            {
                int quick_try = max_appr.Sign;
                if (quick_try != 0) return quick_try;
            }
            int needed_prec = a - 1;
            BigInteger this_appr = get_appr(needed_prec);
            return this_appr.Sign;
        }

        public int signum()
        {
            for (int i = -20; ; i *= 2)
            {
                check_prec(i);
                int result = signum(i);
                if (result != 0) return result;
                // ******************************************************
                // Implementar Token de cancelación
                // ******************************************************
            }
        }

        public CR simple_ln()
        {
            return new prescaled_ln_CR(this.subtract(one));
        }

        public CR add(CR x)
        {
            return new add_CR(this, x);
        }

        public CR subtract(CR x)
        {
            return new add_CR(this, x.negate());
        }

        public CR multiply(CR x)
        {
            return new mult_CR(this, x);
        }

        public CR divide(CR x)
        {
            return new mult_CR(this, x.inverse());
        }

        public CR inverse()
        {
            return new inv_CR(this);
        }

        public CR exp()
        {
            int low_prec = -10;
            BigInteger rough_appr = get_appr(low_prec);
            //if (rough_appr.Sign < 0) return negate().exp().inverse();
            if (rough_appr > big2 || rough_appr < bigm2)
            {
                CR square_root = shiftRight(1).exp();
                return square_root.multiply(square_root);
            }
            else
                return new prescaled_exp_CR(this);
        }

        public CR factorial()
        {
            return new fact_CR(intValue());
        }

        private static readonly BigInteger low_ln_limit = new BigInteger(8);
        private static readonly BigInteger high_ln_limit = new BigInteger(24);
        private static readonly BigInteger scaled_4 = new BigInteger(64);
        public CR ln()
        {
            int low_prec = -4;
            BigInteger rough_appr = get_appr(low_prec);
            if (rough_appr.Sign < big0) throw new ArithmeticException();
            if (rough_appr <= low_ln_limit)
                return inverse().ln().negate();
            if (rough_appr >= high_ln_limit)
            {
                if (rough_appr <= scaled_4)
                {
                    CR quarter = sqrt().sqrt().ln();
                    return quarter.shiftLeft(2);
                }
                else
                {
                    int extra_bits = (int)rough_appr.GetBitLength() - 3;
                    CR scaled_result = shiftRight(extra_bits).ln();
                    return scaled_result.add(valueOf(extra_bits).multiply(ln2()));
                }
            }
            return simple_ln();
        }

        public CR cos()
        {
            BigInteger halfpi_multiples = divide(PI).get_appr(-1);
            BigInteger abs_halfpi_multiples = BigInteger.Abs(halfpi_multiples);
            if (abs_halfpi_multiples >= big2)
            {
                BigInteger pi_multiples = scale(halfpi_multiples, -1);
                CR adjustment = PI.multiply(valueOf(pi_multiples));
                if ((pi_multiples & big1).Sign != 0)
                    return subtract(adjustment).cos().negate();
                else
                    return subtract(adjustment).cos();
            }
            else if (BigInteger.Abs(get_appr(-1)) >= big2)
            {
                CR cos_half = shiftRight(1).cos();
                return cos_half.multiply(cos_half).shiftLeft(1).subtract(one);
            }
            else
                return new prescaled_cos_CR(this);
        }

        public CR sin()
        {
            return half_PI.subtract(this).cos();
        }

        public CR acos()
        {
            return half_PI.subtract(asin());
        }

        public CR asin()
        {
            BigInteger rough_appr = get_appr(-10);
            if (rough_appr > big750)
            {
                CR new_arg = one.subtract(multiply(this)).sqrt();
                return new_arg.acos();
            }
            else if (rough_appr < bigm750)
                return negate().asin().negate();
            else
                return new prescaled_asin_CR(this);
        }

        public static CR atan_reciprocal(int n)
        {
            return new integral_atan_CR(n);
        }

        public CR sqrt()
        {
            return new sqrt_CR(this);
        }

        public CR select(CR x, CR y)
        {
            return new select_CR(this, x, y);
        }
        #endregion

        public BigInteger get_appr(int prec)
        {
            check_prec(prec);
            if (appr_valid && prec >= min_prec)
                return scale(max_appr, min_prec - prec);
            else
            {
                BigInteger result = approximate(prec);
                min_prec = prec;
                max_appr = result;
                appr_valid = true;
                return result;
            }
        }
        #endregion

        #region[Métodos estáticos de CR]
        #region[    Métodos Asignadores]
        public static CR valueOf(BigInteger n)
        {
            return new int_CR(n);
        }

        public static CR valueOf(int n)
        {
            return new int_CR(n);
        }

        public static CR valueOf(long n)
        {
            return new int_CR(n);
        }

        public static CR valueOf(double n)
        {
            if (double.IsNaN(n)) throw new ArithmeticException();
            if (double.IsInfinity(n)) throw new ArithmeticException();
            bool negative = (n < 0.0);
            long bits = BitConverter.DoubleToInt64Bits(n);
            long mantissa = (bits & 0xfffffffffffff);
            int biased_exp = (int)(bits >> 52);
            int exp = biased_exp - 1075;
            if (biased_exp != 0)
                mantissa += (1L << 52);
            else
                mantissa <<= 1;
            CR result = valueOf(mantissa).shiftLeft(exp);
            if (negative) result = result.negate();
            return result;
        }

        public static CR valueOf(string s, int radix)
        {
            try
            {
                int len = s.Length;
                int start_pos = 0, point_pos;
                string fraction;
                while (s[start_pos] == ' ') ++start_pos;
                while (s[len - 1] == ' ') --len;
                point_pos = s.IndexOf('.', start_pos);
                if (point_pos == -1)
                {
                    point_pos = len;
                    fraction = "0";
                }
                else
                    fraction = s.Substring(point_pos + 1, len - point_pos - 1);

                string whole = s.Substring(start_pos, point_pos - start_pos);

                BigInteger scaled_result;
                if (radix != 10)
                    scaled_result = Utils.ParseWithRadix(whole + fraction, radix);
                else
                    scaled_result = BigInteger.Parse(whole + fraction);

                BigInteger divisor = BigInteger.Pow(radix, fraction.Length);
                return valueOf(scaled_result).divide(valueOf(divisor));
            }
            catch (Exception e)
            {
                throw new FormatException("Error al convertir el número", e);
            }
        }
        #endregion

        public static int bound_log2(int n)
        {
            int abs_n = Math.Abs(n);
            return (int)Math.Ceiling(Math.Log((double)(abs_n + 1)) / Math.Log(2d));
        }

        public static void check_prec(int n)
        {
            int high = n >> 28;
            int high_shifted = n >> 29;
            if (0 != (high ^ high_shifted))
                throw new PrecisionOverflowError();
        }

        private static string zeroes(int n)
        {
            return new string('0', n);
        }

        public static BigInteger shift(BigInteger k, int n)
        {
            if (n == 0) return k;
            if (n < 0) return k >> (-n);
            return k << n;
        }

        public static BigInteger scale(BigInteger k, int n)
        {
            if (n >= 0)
                return k << n;
            else
            {
                BigInteger adj_k = shift(k, n + 1) + big1;
                return adj_k >> 1;
            }
        }

        public static CR ln2()
        {
            CR ten_ninths = valueOf(10).divide(valueOf(9));
            CR twentyfive_twentyfourths = valueOf(25).divide(valueOf(24));
            CR eightyone_eightyeths = valueOf(81).divide(valueOf(80));

            CR ln2_1 = valueOf(7).multiply(ten_ninths.simple_ln());
            CR ln2_2 = valueOf(2).multiply(twentyfive_twentyfourths.simple_ln());
            CR ln2_3 = valueOf(3).multiply(eightyone_eightyeths.simple_ln());

            return ln2_1.subtract(ln2_2).add(ln2_3);
        }

        #endregion

    }
}
