using System.Numerics;
using System.Text;

namespace PruebaAritmetica.Clases.CRCSharp
{
    public static class Utils
    {
        public static BigInteger ParseWithRadix(string value, int radix)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (radix < 2 || radix > 36)
                throw new ArgumentException("Radix must be between 2 and 36", nameof(radix));

            value = value.Trim();
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be empty", nameof(value));

            // Handle negative numbers
            bool isNegative = value[0] == '-';
            if (isNegative)
                value = value.Substring(1);

            BigInteger result = BigInteger.Zero;
            value = value.ToUpper();

            foreach (char c in value)
            {
                int digit;
                if (char.IsDigit(c))
                    digit = c - '0';
                else if (c >= 'A' && c <= 'Z')
                    digit = c - 'A' + 10;
                else
                    throw new ArgumentException($"Invalid character '{c}' for radix {radix}");

                if (digit >= radix)
                    throw new ArgumentException($"Digit '{c}' is not valid for radix {radix}");

                result = result * radix + digit;
            }

            return isNegative ? -result : result;
        }

        private const string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string ToStringWithRadix(this BigInteger value, int radix)
        {
            if (radix < 2 || radix > 36)
                throw new ArgumentException("Radix must be between 2 and 36", nameof(radix));

            if (value == 0)
                return "0";

            bool isNegative = value < 0;
            value = BigInteger.Abs(value);

            StringBuilder result = new StringBuilder();

            while (value > 0)
            {
                value = BigInteger.DivRem(value, radix, out BigInteger remainder);
                result.Insert(0, Digits[(int)remainder]);
            }

            if (isNegative)
                result.Insert(0, '-');

            return result.ToString();
        }
    }
}
