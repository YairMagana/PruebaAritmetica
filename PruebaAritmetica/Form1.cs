using ExtendedNumerics;
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
                    //EjecutorOperaciones ejecutor = new EjecutorOperaciones();
                    EjecutorOperacionesCR ejecutor = new EjecutorOperacionesCR();
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

            Stopwatch sw = new Stopwatch();
            sw.Start();

            string val1 = "25.38E138";
            string val2 = "45.593E137";
            string val3 = "3.875E55";
            string val4 = "14.8";
            string val5 = "9.78901234";
            string val6 = "1859.34175";
            string val7 = "479.8";
            string val8 = "105.4455";

            // Operaciones ussanddo CR de C# (~500ms)
            CR cr1 = CR.valueOf(val1, 10);
            CR cr2 = CR.valueOf(val2, 10);
            CR cr3 = CR.valueOf(val3, 10);
            CR cr4 = CR.valueOf(val4, 10);
            CR cr5 = CR.valueOf(val5, 10);
            CR cr6 = CR.valueOf(val6, 10);
            CR cr7 = CR.valueOf(val7, 10);
            CR cr8 = CR.valueOf(val8, 10);

            //CR temp1 = ((cr1.add(cr2)).divide(cr3.multiply(atan(cr4.ln().multiply(cr5).exp())))).multiply(cr6.cos().sqrt());
            //CR resultado = (cr7.ln().multiply(cr8).exp()).ln().subtract(((cr1.add(cr2)).divide(cr3.multiply(atan(cr4.ln().multiply(cr5).exp())))).multiply(cr6.cos().sqrt()));
            CR resultado = cr7.ln().multiply(cr8).exp();
            string res = resultado.toString(5000);

            //BigDecimal.Precision = 300;
            //BigDecimal.AlwaysTruncate = true;

            //// Operaciones usando BigDecimal
            //BigDecimal bd1 = BigDecimal.Parse(val1);
            //BigDecimal bd2 = BigDecimal.Parse(val2);
            //BigDecimal bd3 = BigDecimal.Parse(val3);
            //BigDecimal bd4 = BigDecimal.Parse(val4);
            //BigDecimal bd5 = BigDecimal.Parse(val5);
            //BigDecimal bd6 = BigDecimal.Parse(val6);
            //BigDecimal bd7 = BigDecimal.Parse(val7);
            //BigDecimal bd8 = BigDecimal.Parse(val8);

            ////BigDecimal temp1b = (bd1 + bd2) / (bd3 * BigDecimal.Arctan(BigDecimal.Exp(BigDecimal.Ln(bd4) * bd5)));/* * BigDecimal.SquareRoot(BigDecimal.Cos(bd6), 1500);
            ///*BigDecimal resultado = BigDecimal.Ln(BigDecimal.Exp(BigDecimal.Ln(bd7) * bd8)) - temp1b;*/

            //BigDecimal test1 = BigDecimal.Exp(BigDecimal.Ln(bd7) * bd8);

            //string res = test1.ToString();

            sw.Stop();
            MessageBox.Show($"Tiempo: {sw.ElapsedMilliseconds} ms");
            txtbResult.Text = res;

        }

        private CR atan(CR cr)
        {
            CR xSq = cr.multiply(cr);
            CR abs_sin_atan = cr.multiply(cr).divide(CR.valueOf(1).add(cr.multiply(cr))).sqrt();
            CR sin_atan = cr.select(abs_sin_atan.negate(), abs_sin_atan);
            return sin_atan.asin();
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
