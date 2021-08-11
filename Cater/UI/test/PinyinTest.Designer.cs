namespace UI.test
{
    partial class PinyinTest
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
            this.txtPy = new System.Windows.Forms.TextBox();
            this.lblPy = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPy
            // 
            this.txtPy.Location = new System.Drawing.Point(103, 56);
            this.txtPy.Name = "txtPy";
            this.txtPy.Size = new System.Drawing.Size(100, 25);
            this.txtPy.TabIndex = 0;
            this.txtPy.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblPy
            // 
            this.lblPy.AutoSize = true;
            this.lblPy.Location = new System.Drawing.Point(103, 104);
            this.lblPy.Name = "lblPy";
            this.lblPy.Size = new System.Drawing.Size(0, 15);
            this.lblPy.TabIndex = 1;
            // 
            // PinyinTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 297);
            this.Controls.Add(this.lblPy);
            this.Controls.Add(this.txtPy);
            this.Name = "PinyinTest";
            this.Text = "PinyinTest";
            this.Load += new System.EventHandler(this.PinyinTest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPy;
        private System.Windows.Forms.Label lblPy;
    }
}