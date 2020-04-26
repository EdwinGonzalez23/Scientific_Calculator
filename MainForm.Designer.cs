namespace Scientific_Calculator
{
    partial class MainForm
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
            this.ctlCalculator1 = new Scientific_Calculator.Controlers.ctlCalculator();
            this.SuspendLayout();
            // 
            // ctlCalculator1
            // 
            this.ctlCalculator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlCalculator1.Location = new System.Drawing.Point(0, 0);
            this.ctlCalculator1.Name = "ctlCalculator1";
            this.ctlCalculator1.Size = new System.Drawing.Size(537, 341);
            this.ctlCalculator1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 341);
            this.Controls.Add(this.ctlCalculator1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Controlers.ctlCalculator ctlCalculator1;
    }
}