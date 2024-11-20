using System;
using System.Collections.Concurrent;
using System.Numerics;

namespace PruebaAritmetica.Clases
{
    public class BigDecimal
    {
        private BigInteger _numerator;
        private BigInteger _scale;

        public BigDecimal(string value)
        {
            ParseDecimalString(value);
        }

        public BigDecimal(BigInteger value)
        {
            _numerator = value;
            _scale = 0;
        }

        private void ParseDecimalString(string value)
        {
            value = string.IsNullOrEmpty(value) ? "0" : value;
            int decimalIndex = value.IndexOf('.');
            if (decimalIndex == -1)
            {
                _numerator = BigInteger.Parse(value);
                _scale = 0;
            }
            else
            {
                string withoutDecimal = value.Remove(decimalIndex, 1);
                _numerator = BigInteger.Parse(withoutDecimal);
                _scale = value.Length - decimalIndex - 1;
            }
        }

        public static BigDecimal Factorial(int n)
        {
            BigInteger result = BigInteger.One;

            if (n > 100)
            {
                const int chunkSize = 100;
                for (int i = 1; i <= n; i += chunkSize)
                {
                    BigInteger chunkResult = BigInteger.One;
                    int chunkEnd = Math.Min(n, i + chunkSize - 1);
                    for (int j = i; j <= chunkEnd; j++)
                        chunkResult *= j;

                    result *= chunkResult;
                }
            }
            else
            {
                for (int i = 1; i <= n; i++)
                    result *= i;
            }

            return new BigDecimal(result);
        }

        public static BigDecimal Factorial(int n, int chunkSize)
        {
            BigInteger result = BigInteger.One;

            if (n > chunkSize)
            {
                var partialResults = new ConcurrentBag<BigInteger>();

                Parallel.For(0, (n + chunkSize - 1) / chunkSize, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, i =>
                {
                    BigInteger chunkResult = BigInteger.One;
                    int chunkStart = i * chunkSize + 1;
                    int chunkEnd = Math.Min(n, chunkStart + chunkSize - 1);
                    for (int j = chunkStart; j <= chunkEnd; j++)
                    {
                        chunkResult *= j;
                    }
                    partialResults.Add(chunkResult);
                });

                foreach (var partialResult in partialResults)
                    result *= partialResult;
            }
            else
            {
                for (int i = 1; i <= n; i++)
                    result *= i;
            }

            return new BigDecimal(result);
        }

        public static BigDecimal operator +(BigDecimal a, BigDecimal b)
        {
            BigInteger scale = BigInteger.Max(a._scale, b._scale);
            BigInteger multiplierA = BigInteger.Pow(10, (int)(scale - a._scale));
            BigInteger multiplierB = BigInteger.Pow(10, (int)(scale - b._scale));

            BigInteger newNumerator = (a._numerator * multiplierA) + (b._numerator * multiplierB);
            return new BigDecimal(newNumerator) { _scale = scale };
        }

        public static BigDecimal operator -(BigDecimal a, BigDecimal b)
        {
            BigInteger scale = BigInteger.Max(a._scale, b._scale);
            BigInteger multiplierA = BigInteger.Pow(10, (int)(scale - a._scale));
            BigInteger multiplierB = BigInteger.Pow(10, (int)(scale - b._scale));

            BigInteger newNumerator = (a._numerator * multiplierA) - (b._numerator * multiplierB);
            return new BigDecimal(newNumerator) { _scale = scale };
        }

        public static BigDecimal operator *(BigDecimal a, BigDecimal b)
        {
            BigInteger newNumerator = a._numerator * b._numerator;
            BigInteger newScale = a._scale + b._scale;
            return new BigDecimal(newNumerator) { _scale = newScale };
        }

        public static BigDecimal operator /(BigDecimal a, BigDecimal b)
        {
            if (b._numerator == 0)
                throw new DivideByZeroException();

            int precision = 1200; // Default precision
            BigInteger multiplier = BigInteger.Pow(10, precision);
            BigInteger newNumerator = (a._numerator * multiplier) / b._numerator;
            return new BigDecimal(newNumerator) { _scale = a._scale - b._scale + precision };
        }

        public static BigDecimal Pi(int precision)
        {
            BigDecimal sum = new BigDecimal(BigInteger.Zero);
            BigDecimal one = new BigDecimal(BigInteger.One);
            BigDecimal four = new BigDecimal(new BigInteger(4));

            for (int k = 0; k < precision; k++)
            {
                BigDecimal term = one / new BigDecimal(new BigInteger(2 * k + 1));
                if (k % 2 == 0)
                {
                    sum += term;
                }
                else
                {
                    sum -= term;
                }
            }

            return four * sum;
        }

        public override string ToString()
        {
            string numStr = _numerator.ToString();
            if (_scale == 0) return numStr;

            if (numStr.Length <= _scale)
            {
                numStr = "0." + new string('0', (int)_scale - numStr.Length) + numStr;
            }
            else
            {
                numStr = numStr.Insert(numStr.Length - (int)_scale, ".");
            }
            return numStr;
        }
    }
}