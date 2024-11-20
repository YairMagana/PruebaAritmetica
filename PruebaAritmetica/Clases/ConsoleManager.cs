using System.Runtime.InteropServices;

namespace PruebaAritmetica.Clases
{

    public class ConsoleManager
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private TextWriter? originalConsoleOut;
        private Thread? consoleThread;

        public void OpenConsole()
        {
            if (AllocConsole())
            {
                originalConsoleOut = Console.Out;
                Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });

                consoleThread = new Thread(ConsoleLoop);
                consoleThread.IsBackground = true;
                consoleThread.Start();
            }
        }

        private void ConsoleLoop()
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (input == "exit")
                {
                    CloseConsole();
                    break;
                }
                // Aquí puedes agregar más lógica para manejar otros comandos
            }
        }

        public void WriteLine(string? message)
        {
            originalConsoleOut?.WriteLine(message ?? string.Empty);
        }

        public void WriteLine()
        {
            originalConsoleOut?.WriteLine();
        }

        private void CloseConsole()
        {
            if (originalConsoleOut != null)
            {
                Console.SetOut(originalConsoleOut);
                originalConsoleOut = null;
            }
            FreeConsole();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeConsole();
    }
}
