using NCalc.Domain;
using NCalc.Exceptions;
using NCalc.Parser;
using NCalc.Visitors;
using BinaryExpression = NCalc.Domain.BinaryExpression;
using Expression = NCalc.Expression;
using UnaryExpression = NCalc.Domain.UnaryExpression;

public class CustomVisitor : ILogicalExpressionVisitor<double>
{
    public double Visit(TernaryExpression expression)
    {
        throw new NotImplementedException();
    }

    public double Visit(BinaryExpression expression)
    {
        double left = expression.LeftExpression.Accept(this);
        double right = expression.RightExpression.Accept(this);

        return expression.Type switch
        {
            BinaryExpressionType.Plus => left + right,
            BinaryExpressionType.Minus => left - right,
            BinaryExpressionType.Times => left * right,
            BinaryExpressionType.Div => left / right,
            BinaryExpressionType.Modulo => left % right,
            BinaryExpressionType.Exponentiation => Math.Pow(left, right),
            _ => throw new NotSupportedException($"Operación no soportada: {expression.Type}")
        };
    }

    public double Visit(UnaryExpression expression)
    {
        double operand = expression.Expression.Accept(this);

        return expression.Type switch
        {
            UnaryExpressionType.Negate => -operand,
            UnaryExpressionType.Not => throw new NotSupportedException("Operación NOT no soportada"),
            _ => throw new NotSupportedException($"Operación no soportada: {expression.Type}")
        };
    }

    public double Visit(ValueExpression expression)
    {
        if (double.TryParse(expression.Value.ToString(), out double result))
        {
            return result;
        }
        throw new NotSupportedException("Tipo de valor no soportado (ValueExpression)");
    }

    public double Visit(Function function)
    {
        throw new NotImplementedException();
    }

    public double Visit(Identifier identifier)
    {
        if (identifier.Name.Equals("pi", StringComparison.OrdinalIgnoreCase))
        {
            return Math.PI;
        }
        else if (identifier.Name.Equals("e", StringComparison.OrdinalIgnoreCase))
        {
            return Math.E;
        }
        throw new NotSupportedException($"Identificador no soportado: {identifier.Name}");
    }

    public double Visit(LogicalExpressionList list)
    {
        throw new NotImplementedException();
    }
}

namespace PruebaAritmetica.Clases
{
    internal class EjecutorOperaciones
    {
        public string EjecutarOperacion(string input)
        {
            try
            {
                // Crear una instancia de CustomVisitor
                var visitor = new CustomVisitor();

                var logicalExpressionContext = new LogicalExpressionParserContext(input, NCalc.ExpressionOptions.None);
                var logicalExpression = LogicalExpressionParser.Parse(logicalExpressionContext);

                // Evaluar la expresión usando el visitante personalizado
                double resultado = logicalExpression.Accept(visitor);

                return resultado.ToString();
            }
            // ¿Es Expresión inválida?
            catch (NCalcEvaluationException)
            {
                return "-- Expresión inválida --";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar la operación: \n" + ex.Message);
            }
        }
    }
}
