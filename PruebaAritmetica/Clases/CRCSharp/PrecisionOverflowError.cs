
namespace PruebaAritmetica.Clases.CRCSharp
{
    [Serializable]
    internal class PrecisionOverflowError : Exception
    {
        public PrecisionOverflowError()
        {
        }

        public PrecisionOverflowError(string? message) : base(message)
        {
        }

        public PrecisionOverflowError(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}