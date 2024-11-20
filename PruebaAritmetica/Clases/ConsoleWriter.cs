using System.Text;

namespace PruebaAritmetica.Clases
{
    public class ConsoleWriter : TextWriter
    {
        private readonly ConsoleManager _consoleManager;

        public ConsoleWriter(ConsoleManager consoleManager)
        {
            _consoleManager = consoleManager;
        }

        public override void WriteLine(string? value)
        {
            _consoleManager.WriteLine(value ?? string.Empty);
        }

        public override void Write(char value)
        {
            _consoleManager.WriteLine(value.ToString());
        }

        public override Encoding Encoding => Encoding.UTF8;
    }
}