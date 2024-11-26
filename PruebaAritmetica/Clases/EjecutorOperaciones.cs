using NCalc.Domain;
using NCalc.Exceptions;
using NCalc.Parser;
using NCalc.Visitors;
using BinaryExpression = NCalc.Domain.BinaryExpression;
using Expression = NCalc.Expression;
using UnaryExpression = NCalc.Domain.UnaryExpression;

public class CustomVisitor : ILogicalExpressionVisitor<decimal>
{
    public decimal Visit(TernaryExpression expression)
    {
        throw new NotImplementedException();
    }

    public decimal Visit(BinaryExpression expression)
    {
        decimal left = expression.LeftExpression.Accept(this);
        decimal right = expression.RightExpression.Accept(this);

        return expression.Type switch
        {
            BinaryExpressionType.Plus => left + right,
            BinaryExpressionType.Minus => left - right,
            BinaryExpressionType.Times => left * right,
            BinaryExpressionType.Div => left / right,
            BinaryExpressionType.Modulo => left % right,
            //BinaryExpressionType.Exponentiation => Math.Pow(left, right),
            _ => throw new NotSupportedException($"Operación no soportada: {expression.Type}")
        };
    }

    public decimal Visit(UnaryExpression expression)
    {
        decimal operand = expression.Expression.Accept(this);

        return expression.Type switch
        {
            UnaryExpressionType.Negate => -operand,
            UnaryExpressionType.Not => throw new NotSupportedException("Operación NOT no soportada"),
            _ => throw new NotSupportedException($"Operación no soportada: {expression.Type}")
        };
    }

    public decimal Visit(ValueExpression expression)
    {
        if (decimal.TryParse(expression.Value?.ToString(), out decimal result))
        {
            return result;
        }
        throw new NotSupportedException("Tipo de valor no soportado (ValueExpression)");
    }

    public decimal Visit(Function function)
    {
        throw new NotImplementedException();
    }

    public decimal Visit(Identifier identifier)
    {
        if (identifier.Name.Equals("pi", StringComparison.OrdinalIgnoreCase))
        {
            return Decimal.Parse(Math.PI.ToString());
        }
        else if (identifier.Name.Equals("e", StringComparison.OrdinalIgnoreCase))
        {
            return Decimal.Parse(Math.E.ToString());
        }
        throw new NotSupportedException($"Identificador no soportado: {identifier.Name}");
    }

    public decimal Visit(LogicalExpressionList list)
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

                var logicalExpressionContext = new LogicalExpressionParserContext(input, NCalc.ExpressionOptions.IterateParameters);
                var logicalExpression = LogicalExpressionParser.Parse(logicalExpressionContext);

                // Evaluar la expresión usando el visitante personalizado
                decimal resultado = logicalExpression.Accept(visitor);

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
