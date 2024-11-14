using PruebaAritmetica.Clases;

namespace PruebaAritmetica
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
    }
}
