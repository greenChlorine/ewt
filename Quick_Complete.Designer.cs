namespace ewt360
{
    partial class Quick_Complete
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.combtn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // combtn
            // 
            this.combtn.ForeColor = System.Drawing.Color.Black;
            this.combtn.Location = new System.Drawing.Point(35, 30);
            this.combtn.Name = "combtn";
            this.combtn.Size = new System.Drawing.Size(155, 45);
            this.combtn.TabIndex = 0;
            this.combtn.Text = "Complete it!";
            this.combtn.UseVisualStyleBackColor = true;
            this.combtn.Click += new System.EventHandler(this.combtn_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(35, 131);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(719, 28);
            this.progressBar1.TabIndex = 1;
            // 
            // Quick_Complete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(793, 219);
            this.ControlBox = false;
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.combtn);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Quick_Complete";
            this.Text = "Quick_Complete";
            this.Load += new System.EventHandler(this.Quick_Complete_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button combtn;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}