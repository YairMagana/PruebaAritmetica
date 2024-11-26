using NCalc.Domain;
using NCalc.Exceptions;
using NCalc.Parser;
using NCalc.Visitors;
using PruebaAritmetica.Clases.CRCSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PruebaAritmetica.Clases
{
    public class CustomVisitorCR : ILogicalExpressionVisitor<CR>
    {
        public CR Visit(TernaryExpression expression)
        {
            TernaryExpression ternaryExpression = expression;
            throw new NotImplementedException();
        }

        public CR Visit(BinaryExpression expression)
        {
            CR left = expression.LeftExpression.Accept(this);
            CR right = expression.RightExpression.Accept(this);

            return expression.Type switch
            {
                BinaryExpressionType.Plus => left.add(right),
                BinaryExpressionType.Minus => left.add(right.negate()),
                BinaryExpressionType.Times => left.multiply(right),
                BinaryExpressionType.Div => left.divide(right),
                //BinaryExpressionType.Modulo => left % right,
                BinaryExpressionType.Exponentiation => left.ln().multiply(right).exp(),
                _ => throw new NotSupportedException($"Operación no soportada: {expression.Type}")
            };
        }

        public CR Visit(UnaryExpression expression)
        {
            CR operand = expression.Expression.Accept(this);

            return expression.Type switch
            {
                UnaryExpressionType.Positive => operand,
                UnaryExpressionType.Negate => operand.negate(),
                UnaryExpressionType.Not => throw new NotSupportedException("Operación NOT no soportada"),
                _ => throw new NotSupportedException($"Operación no soportada: {expression.Type}")
            };
        }

        public CR Visit(ValueExpression expression)
        {
            try
            {
                var valor = expression.Value?.ToString();
                if (string.IsNullOrEmpty(valor))
                    throw new NotSupportedException("Expresión vacía (ValueExpression).");
                return CR.valueOf(valor, 10);
            }
            catch (Exception ex)
            {
                throw new NotSupportedException("Tipo de valor no soportado (ValueExpression). \n", ex);
            }
        }

        public CR Visit(Function function)
        {
            if (function.Identifier.Name.Equals("sqrt", StringComparison.OrdinalIgnoreCase))
            {
                if (function.Parameters.Count != 1)
                    throw new ArgumentException("La función sqrt requiere exactamente un argumento.");

                CR argument = function.Parameters[0].Accept(this);
                return argument.sqrt();
            }

            throw new NotSupportedException($"Función no soportada: {function.Identifier.Name}");
        }

        public CR Visit(Identifier identifier)
        {
            if (identifier.Name.Equals("pi", StringComparison.OrdinalIgnoreCase))
            {
                return CR.PI;
            }
            else if (identifier.Name.Equals("e", StringComparison.OrdinalIgnoreCase))
            {
                return CR.valueOf(1).exp();
            }
            throw new NotSupportedException($"Identificador no soportado: {identifier.Name}");
        }

        public CR Visit(LogicalExpressionList list)
        {
            throw new NotImplementedException();
        }
    }

    internal class EjecutorOperacionesCR
    {
        public string EjecutarOperacion(string input)
        {
            try
            {
                string preprocessedInput = WrapNumbersInQuotes(input);
                //string preprocessedInput = input;

                // Crear una instancia de CustomVisitor
                var visitor = new CustomVisitorCR();

                var logicalExpressionContext = new LogicalExpressionParserContext(preprocessedInput, NCalc.ExpressionOptions.None);
                var logicalExpression = LogicalExpressionParser.Parse(logicalExpressionContext);

                // Evaluar la expresión usando el visitante personalizado
                CR resultado = logicalExpression.Accept(visitor);

                //CR resultado = CR.valueOf(input, 10);

                string sResultado = resultado.toString(300);
                string sResultadoNS = ToScientificNotation(sResultado, 1, 10);

                return $"{sResultado} ~ {sResultadoNS} (Notación científica)";
            }
            catch (NCalcEvaluationException)
            {
                return "-- Expresión inválida --";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al evaluar la expresión. \n" + ex.Message);
            }
        }

        public static string WrapNumbersInQuotes(string input)
        {
            // Regex pattern for numbers including scientific notation
            string pattern = @"(?<!\w)(\d*\.?\d+(?:E[-+]?\d+)?|\.\d+(?:E[-+]?\d+)?)(?!\w)";

            return Regex.Replace(input, pattern, match =>
            {
                string number = match.Value;
                // If number starts with minus, keep it outside quotes
                if (number.StartsWith("-"))
                {
                    return $"-\"{number.Substring(1)}\"";
                }
                return $"\"{number}\"";
            });
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

        public static string ToScientificNotation(string number, int leftDigits, int rightDigits)
        {
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
    }
}
