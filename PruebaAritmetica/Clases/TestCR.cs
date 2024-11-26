using PruebaAritmetica.Clases.CRCSharp;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace PruebaAritmetica.Clases
{
    internal class TestCR
    {
        private readonly ConsoleManager _consoleManager;

        public TestCR(ConsoleManager consoleManager)
        {
            _consoleManager = consoleManager;
        }

        public void Test(int prec)
        {
            double x = 25.77;
            double y = 31.83;
            double z = 0.53;
            string xStr = x.ToString();
            string yStr = y.ToString();
            string zStr = z.ToString();
            CR cr1 = CR.valueOf(xStr, 10);
            CR cr2 = CR.valueOf(yStr, 10);
            CR cr3 = CR.valueOf(zStr, 10);

            // Iniciar cronómetro de sistema
            Stopwatch sw = new Stopwatch();

            // Mostar valores
            _consoleManager.WriteLine("**** Valores");
            _consoleManager.WriteLine($"{x} = {cr1.toString()}");
            _consoleManager.WriteLine($"{y} = {cr2.toString()}");
            _consoleManager.WriteLine();

            // Probar suma
            //sw.Start();
            //CR suma = cr1.add(cr2);
            //_consoleManager.WriteLine("**** Suma");
            //_consoleManager.WriteLine($"{x} + {y} = {suma.toStringFloatRep(1, 10, 0).toString()}");
            //sw.Stop();
            //_consoleManager.WriteLine($">> Tiempo: {sw.ElapsedMilliseconds} ms");
            //_consoleManager.WriteLine();

            //// Probar resta
            //sw.Restart();
            //CR resta = cr1.subtract(cr2);
            //_consoleManager.WriteLine("**** Resta");
            //_consoleManager.WriteLine($"{x} - {y} = {resta.toStringFloatRep(prec, 10, 0).toString()}");
            //sw.Stop();
            //_consoleManager.WriteLine($">> Tiempo: {sw.ElapsedMilliseconds} ms");
            //_consoleManager.WriteLine();

            //// Probar multiplicación
            //sw.Restart();
            //CR multiplicacion = cr1.multiply(cr2);
            //_consoleManager.WriteLine("**** Multiplicación");
            //_consoleManager.WriteLine($"{x} * {y} = {multiplicacion.toStringFloatRep(prec, 10, prec).toString()}");
            //sw.Stop();
            //_consoleManager.WriteLine($">> Tiempo: {sw.ElapsedMilliseconds} ms");
            //_consoleManager.WriteLine();

            //// Probar división
            //sw.Restart();
            //CR division = cr1.divide(cr2);
            //_consoleManager.WriteLine("**** División");
            //_consoleManager.WriteLine($"{x} / {y} = {division.toStringFloatRep(32, 10, 1).toString()}");
            //sw.Stop();
            //_consoleManager.WriteLine($">> Tiempo: {sw.ElapsedMilliseconds} ms");
            //_consoleManager.WriteLine();

            //// Probar número PI
            //sw.Restart();
            //CR pi = CR.PI;
            //_consoleManager.WriteLine("**** Número PI");
            //_consoleManager.WriteLine($"PI = {pi.toString(prec)}");
            //sw.Stop();
            //_consoleManager.WriteLine($">> Tiempo: {sw.ElapsedMilliseconds} ms");
            //_consoleManager.WriteLine();

            //// Probar número e
            //sw.Restart();
            //CR e = CR.valueOf(1).exp();
            //_consoleManager.WriteLine("**** Número e");
            //_consoleManager.WriteLine($"e = {e.toString(prec)}");
            //sw.Stop();
            //_consoleManager.WriteLine($">> Tiempo: {sw.ElapsedMilliseconds} ms");
            //_consoleManager.WriteLine();

            //// Probar raiz cuadrada
            //sw.Restart();
            //CR raiz = cr2.sqrt();
            //_consoleManager.WriteLine("**** Raiz cuadrada");
            //_consoleManager.WriteLine($"sqrt({y}) = {raiz.toString(prec)}");
            //sw.Stop();
            //_consoleManager.WriteLine($">> Tiempo: {sw.ElapsedMilliseconds} ms");
            //_consoleManager.WriteLine();

            // Probar logaritmo natural
            sw.Restart();
            CR logn = cr2.ln();
            _consoleManager.WriteLine("**** Logaritmo natural");
            _consoleManager.WriteLine($"ln({y}) = {logn.toString(prec)}");
            sw.Stop();
            _consoleManager.WriteLine($">> Tiempo: {sw.ElapsedMilliseconds} ms");
            _consoleManager.WriteLine();

            //// Probar logaritmo base 10
            //sw.Restart();
            //CR log10 = cr2.ln().divide(CR.valueOf(10).ln());
            //_consoleManager.WriteLine("**** Logaritmo base 10");
            //_consoleManager.WriteLine($"log10({y}) = {log10.toString(prec)}");
            //sw.Stop();
            //_consoleManager.WriteLine($">> Tiempo: {sw.ElapsedMilliseconds} ms");
            //_consoleManager.WriteLine();

            //// Probar valor al cuadrado
            //sw.Restart();
            //CR cuadrado = cr2.multiply(cr2);
            //_consoleManager.WriteLine("**** Cuadrado");
            //_consoleManager.WriteLine($"{y}^2 = {cuadrado.toString(prec)}");
            //sw.Stop();
            //_consoleManager.WriteLine($">> Tiempo: {sw.ElapsedMilliseconds} ms");
            //_consoleManager.WriteLine();

            //// Probar seno
            //sw.Restart();
            //CR seno = cr2.sin();
            //_consoleManager.WriteLine("**** Seno");
            //_consoleManager.WriteLine($"sin({y}) = {seno.toString(prec)}");
            //sw.Stop();
            //_consoleManager.WriteLine($">> Tiempo: {sw.ElapsedMilliseconds} ms");
            //_consoleManager.WriteLine();

            //// Probar coseno
            //sw.Restart();
            //CR coseno = cr2.cos();
            //_consoleManager.WriteLine("**** Coseno");
            //_consoleManager.WriteLine($"cos({y}) = {coseno.toString(prec)}");
            //sw.Stop();
            //_consoleManager.WriteLine($">> Tiempo: {sw.ElapsedMilliseconds} ms");
            //_consoleManager.WriteLine();

            //// Probar tangente
            //sw.Restart();
            //CR tangente = cr2.sin().divide(cr2.cos());
            //_consoleManager.WriteLine("**** Tangente");
            //_consoleManager.WriteLine($"tan({y}) = {tangente.toString(prec)}");
            //sw.Stop();
            //_consoleManager.WriteLine($">> Tiempo: {sw.ElapsedMilliseconds} ms");
            //_consoleManager.WriteLine();

            //// Probar arco seno
            //sw.Restart();
            //CR arcseno = cr3.asin();
            //_consoleManager.WriteLine("**** Arco seno");
            //_consoleManager.WriteLine($"asin({z}) = {arcseno.toString(prec)}");
            //sw.Stop();
            //_consoleManager.WriteLine($">> Tiempo: {sw.ElapsedMilliseconds} ms");
            //_consoleManager.WriteLine();

            //// Probar arco coseno
            //sw.Restart();
            //CR arccoseno = cr3.acos();
            //_consoleManager.WriteLine("**** Arco coseno");
            //_consoleManager.WriteLine($"acos({z}) = {arccoseno.toString(prec)}");
            //sw.Stop();
            //_consoleManager.WriteLine($">> Tiempo: {sw.ElapsedMilliseconds} ms");
            //_consoleManager.WriteLine();

            //// Probar arco tangente
            //sw.Restart();
            //CR xSq = cr3.multiply(cr3);
            //CR abs_sin_atan = xSq.divide(CR.valueOf(1).add(xSq)).sqrt();
            //CR sin_atan = cr3.select(abs_sin_atan.negate(), abs_sin_atan);
            //CR arcotangente = sin_atan.asin();
            //_consoleManager.WriteLine("**** Arco tangente");
            //_consoleManager.WriteLine($"atan({z}) = {arcotangente.toString(prec)}");
            //sw.Stop();
            //_consoleManager.WriteLine($">> Tiempo: {sw.ElapsedMilliseconds} ms");
            //_consoleManager.WriteLine();

            //// Probar exponencial
            //sw.Restart();
            //CR exp = cr2.exp();
            //_consoleManager.WriteLine("**** Exponencial");
            //_consoleManager.WriteLine($"exp({y}) = {exp.toString(prec)}");
            //sw.Stop();
            //_consoleManager.WriteLine($">> Tiempo: {sw.ElapsedMilliseconds} ms");
            //_consoleManager.WriteLine();

            //// Probar potencia de 10
            //sw.Restart();
            //CR potencia10 = cr2.multiply(CR.valueOf(10).ln()).exp();
            //_consoleManager.WriteLine("**** Potencia de 10");
            //_consoleManager.WriteLine($"10^{y} = {potencia10.toString(32)}");
            //sw.Stop();
            //_consoleManager.WriteLine($">> Tiempo: {sw.ElapsedMilliseconds} ms");
            //_consoleManager.WriteLine();

            //// Probar potencia
            //sw.Restart();
            //CR potencia = cr1.ln().multiply(cr2).exp();
            //_consoleManager.WriteLine("**** Potencia");
            //_consoleManager.WriteLine($"{x}^{y} = {potencia.toString(prec)}");
            //sw.Stop();
            //_consoleManager.WriteLine($">> Tiempo: {sw.ElapsedMilliseconds} ms");
            //_consoleManager.WriteLine();

            //// Probar factorial
            //sw.Restart();
            //int w = y < 40 ? (int)(2 * y * y) : (int)y;
            //CR cr4 = CR.valueOf(w);
            //CR factorial = cr4.factorial();
            //_consoleManager.WriteLine("**** Factorial");
            //_consoleManager.WriteLine($"{w}! = {factorial.toString(32)}");
            //sw.Stop();
            //_consoleManager.WriteLine($">> Tiempo: {sw.ElapsedMilliseconds} ms");
            //_consoleManager.WriteLine();

            //// Probar conversión de cadenas a notación científica
            //string val1 = "123456789.987654321";
            //string val2 = "0.00000000098765432100000";
            //string val3 = "-123456789.9876543210000";
            //string val4 = "-0.000000000987654321";
            //_consoleManager.WriteLine("**** Notación científica");
            //_consoleManager.WriteLine($"{val1} = {ToScientificNotation(val1, 30, 5)}");
            //_consoleManager.WriteLine($"{val2} = {ToScientificNotation(val2, 15, 5)}");
            //_consoleManager.WriteLine($"{val3} = {ToScientificNotation(val3, 5, 5)}");
            //_consoleManager.WriteLine($"{val4} = {ToScientificNotation(val4, 5, 5)}");

            //_consoleManager.WriteLine("1234 = " + ToScientificNotation("1234", 2, 1));
            //_consoleManager.WriteLine("0.1 = " + ToScientificNotation("0.1", 5, 10));
        }

        public static string ToScientificNotation(string number, int leftDigits, int rightDigits)
        {
            // Validate input
            if (string.IsNullOrEmpty(number) || !number.All(c => char.IsDigit(c) || c == '.' || c == '-'))
                throw new ArgumentException("Invalid number format");

            // Build the result with specified precision
            StringBuilder result = new StringBuilder();

            // Handle negative numbers
            bool isNegative = number.StartsWith("-");
            if (isNegative)
            {
                number = number.Substring(1);
                result.Append('-');
            }

            number = TrimTrailingZeros(number);

            // Split number into integer and decimal parts
            string[] parts = number.Split('.');
            string integerPart = parts[0];
            string decimalPart = parts.Length > 1 ? parts[1] : "";

            // Remove leading zeros from integer part
            integerPart = integerPart.TrimStart('0');
            if (string.IsNullOrEmpty(integerPart)) integerPart = "0";

            // Calculate exponent and significand
            int exponent;
            string significand;

            if (integerPart == "0")
            {
                // Handle numbers less than 1
                int leadingZeros = decimalPart.TakeWhile(c => c == '0').Count();
                string nonZeroDecimal = decimalPart.Substring(leadingZeros);

                if (string.IsNullOrEmpty(nonZeroDecimal))
                    return isNegative ? "-0" : "0"; // Return 0 if the number is exactly 0

                int realLeftDigits = Math.Min(leftDigits, nonZeroDecimal.Length);
                exponent = -(leadingZeros + 1) - realLeftDigits + 1;
                significand = nonZeroDecimal;
            }
            else
            {
                // Handle numbers greater than or equal to 1
                significand = integerPart + decimalPart;
                int realLeftDigits = Math.Min(leftDigits, significand.Length);
                exponent = integerPart.Length - realLeftDigits;
            }

            // Add left digits
            int availableLeftDigits = Math.Min(leftDigits, significand.Length);
            string leftPart = significand.Substring(0, availableLeftDigits);
            result.Append(string.IsNullOrEmpty(leftPart) ? "0" : leftPart);

            int remainingDigits = 0;
            // Add right digits if there are more digits and rightDigits > 0
            if (rightDigits > 0 && significand.Length > availableLeftDigits)
            {
                result.Append('.');
                remainingDigits = Math.Min(rightDigits, significand.Length - availableLeftDigits);
                result.Append(significand.Substring(availableLeftDigits, remainingDigits));
            }

            // Add exponent
            result.Append('E');
            result.Append(exponent);

            return result.ToString();
        }

        public static string TrimTrailingZeros(string number)
        {
            if (string.IsNullOrEmpty(number))
                return number;

            // Si el número contiene un punto decimal
            if (number.Contains('.'))
            {
                // Eliminar los ceros finales
                number = number.TrimEnd('0');

                // Si el último carácter es un punto decimal, también eliminarlo
                if (number.EndsWith("."))
                    number = number.TrimEnd('.');
            }

            return number;
        }
    }
}
