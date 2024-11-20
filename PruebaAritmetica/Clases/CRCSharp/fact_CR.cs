using System.Numerics;

namespace PruebaAritmetica.Clases.CRCSharp
{
    internal class fact_CR : CR
    {
        int _x;
        int _chunkSize;

        public fact_CR(int x, int chunkSize = 256)
        {
            _x = x;
            _chunkSize = chunkSize;
        }

        public override BigInteger approximate(int prec)
        {
            BigInteger result = big1;

            if (_x > _chunkSize)
            {
                for (int i = 1; i <= _x; i += _chunkSize)
                {
                    BigInteger chunkResult = BigInteger.One;
                    int chunkEnd = Math.Min(_x, i + _chunkSize - 1);
                    for (int j = i; j <= chunkEnd; j++)
                        chunkResult *= j;

                    result *= chunkResult;
                }
            }
            else
            {
                for (int i = 1; i <= _x; i++)
                    result *= i;
            }
            return scale(result, -prec);
        }
    }
}
