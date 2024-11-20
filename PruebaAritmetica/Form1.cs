using PruebaAritmetica.Clases;
using PruebaAritmetica.Clases.CRCSharp;
using System.Diagnostics;
using System.Numerics;

namespace PruebaAritmetica
{
    public partial class Form1 : Form
    {
        private ConsoleManager consoleManager;

        public Form1()
        {
            InitializeComponent();
            consoleManager = new ConsoleManager();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtbInput.Text))
                {
                    EjecutorOperaciones ejecutor = new EjecutorOperaciones();
                    string resultado = ejecutor.EjecutarOperacion(txtbInput.Text);
                    txtbResult.Text = resultado;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: \n" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BigDecimal bigDecimal1 = new BigDecimal(textBox1.Text);
            BigDecimal bigDecimal2 = new BigDecimal(textBox2.Text);

            //CR cr1 = CR.valueOf(BigInteger.Parse(textBox1.Text));
            //CR cr2 = CR.valueOf(BigInteger.Parse(textBox2.Text));

            //CR suma = cr1.add(cr2);
            //CR resta = cr1.subtract(cr2);
            //CR multiplicacion = cr1.multiply(cr2);
            //CR division = cr1.divide(cr2);

            //BigInteger biSuma = suma.BigIntegerValue();
            //string sSuma = suma.toString();
            //string sResta = resta.toString();
            //string sMultiplicacion = multiplicacion.toString();
            //string sDivisison = division.toString();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            BigDecimal factorial = BigDecimal.Factorial(int.Parse(textBox1.Text), 200);
            sw.Stop();
            MessageBox.Show($"Tiempo: {sw.ElapsedMilliseconds} ms");
            textBox3.Text = factorial.ToString();


            //BigInteger resultado = BigInteger.Parse(textBox1.Text);
            //textBox3.Text = resultado.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int prec = int.Parse(string.IsNullOrWhiteSpace(textBox1.Text) ? "10" : textBox1.Text);

            consoleManager.OpenConsole();
            Console.SetOut(new ConsoleWriter(consoleManager));

            TestCR testCR = new TestCR(consoleManager);
            testCR.Test(prec);
        }
    }
}
