namespace PruebaAritmetica
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtbInput = new TextBox();
            txtbResult = new TextBox();
            btnCalcular = new Button();
            SuspendLayout();
            // 
            // txtbInput
            // 
            txtbInput.Location = new Point(22, 24);
            txtbInput.Multiline = true;
            txtbInput.Name = "txtbInput";
            txtbInput.Size = new Size(475, 76);
            txtbInput.TabIndex = 0;
            // 
            // txtbResult
            // 
            txtbResult.Location = new Point(22, 106);
            txtbResult.Multiline = true;
            txtbResult.Name = "txtbResult";
            txtbResult.ScrollBars = ScrollBars.Vertical;
            txtbResult.Size = new Size(475, 156);
            txtbResult.TabIndex = 1;
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(503, 24);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(75, 23);
            btnCalcular.TabIndex = 2;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(626, 379);
            Controls.Add(btnCalcular);
            Controls.Add(txtbResult);
            Controls.Add(txtbInput);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtbInput;
        private TextBox txtbResult;
        private Button btnCalcular;
    }
}
