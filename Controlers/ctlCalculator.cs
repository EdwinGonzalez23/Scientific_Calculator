using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Scientific_Calculator.Controlers
{
    public partial class ctlCalculator : UserControl
    {
        bool bSimpleExpression = false;
        public ctlCalculator() {
            InitializeComponent();
        }

        #region numbers clicked
        private void btn0_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "0";
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "1";
        }
        private void btn2_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "2";
        }
        private void btn3_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "3";
        }
        private void btn4_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "4";
        }
        private void btn5_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "5";
        }
        private void btn6_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "6";
        }
        private void btn7_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "7";
        }
        private void btn8_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "8";
        }
        private void btn9_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "9";
        }
        #endregion

        #region topthree
        private void btnClear_Click(object sender, EventArgs e) {
            primTextBox.Text = string.Empty;
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            string str = primTextBox.Text;
            int len = str.Length;
            if (len > 0)
                primTextBox.Text = (str.Substring(0, len - 1));
        }
        #endregion
        private void Result() {
            primTextBox.Text = Regex.Replace(primTextBox.Text, @"\s", "");
            if (validateExpression()) {
                string exp = primTextBox.Text;
                try {
                    var result = new DataTable().Compute(exp, null);
                    memTextBox.Text = primTextBox.Text;
                    primTextBox.Text = result.ToString();
                }
                catch (DivideByZeroException) {
                    memTextBox.Text = "Infinity/NaN";
                }    
            } else {
                    memTextBox.Text = "Please Enter A Valid Expression";
            }
        }
        private bool validateExpression() {
             Regex regex = new Regex(@"^[-+]?([0-9]|\-?\d+\.\d)+([-+*/]+[-+]?([0-9]|\-?\d+\.\d)+)*$");
            //Regex regex = new Regex(@"^[-+]?[0-9]+([-+*/]+[-+]?[0-9]+)*$");
            return regex.IsMatch(primTextBox.Text) ? true : false;
        }
        private void shiftCursor() {
            primTextBox.SelectionStart = primTextBox.Text.Length + 1;
            primTextBox.SelectionLength = 0;
        }
        #region event listeners
        private void btnEquals_Click(object sender, EventArgs e) {
            Result();
            primTextBox.SelectionStart = primTextBox.Text.Length + 1;
            primTextBox.SelectionLength = 0;
        }
        private void primTextBox_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Enter:
                    Result();
                    shiftCursor();
                    break;
            }
        }
        private void primTextBox_Enter(object sender, EventArgs e) {
            shiftCursor();
        }
        #endregion event listeners
        #region button functionality
        private void btnSqrt_Click(object sender, EventArgs e) {
            if (validateExpression()) {
                Result();
                double num = Convert.ToDouble(primTextBox.Text);
                if (num < 0) {
                    memTextBox.Text = "Error: Negative Sqrt";
                } else {
                    primTextBox.Text = Math.Sqrt(num).ToString();
                    memTextBox.Text = "Sqrt(" + num + ")";
                }
            }
        }
        #endregion button functionality
    }
}
