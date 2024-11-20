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
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button1 = new Button();
            textBox3 = new TextBox();
            button2 = new Button();
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
            // textBox1
            // 
            textBox1.Location = new Point(22, 268);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(338, 23);
            textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(22, 295);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(338, 23);
            textBox2.TabIndex = 4;
            // 
            // button1
            // 
            button1.Location = new Point(380, 280);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 5;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(22, 324);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(338, 23);
            textBox3.TabIndex = 6;
            // 
            // button2
            // 
            button2.Location = new Point(503, 295);
            button2.Name = "button2";
            button2.Size = new Size(75, 51);
            button2.TabIndex = 7;
            button2.Text = "Test Functions";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(626, 379);
            Controls.Add(button2);
            Controls.Add(textBox3);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
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
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private TextBox textBox3;
        private Button button2;
    }
}
