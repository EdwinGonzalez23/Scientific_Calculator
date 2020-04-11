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
        string Pow2Input;
        bool bPower2Set = true;
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
            memTextBox.Text = string.Empty;
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
            memTextBox.Text += primTextBox.Text;
            memTextBox.Text = Regex.Replace(memTextBox.Text, @"\s", "");
            if (!string.IsNullOrEmpty(Pow2Input))
                memTextBox.Text = Regex.Replace(memTextBox.Text, @"(\w*[0-9]\^2)", Pow2Input + "*" + Pow2Input);
            if (validateExpression()) {
                string exp = memTextBox.Text;
                try {
                    var result = new DataTable().Compute(exp, null);
                    labelInfo.Text = "Expression: " + memTextBox.Text;
                    memTextBox.Text = result.ToString();
                    primTextBox.Text = string.Empty;
                }
                catch (Exception ex) {
                    if ( ex is DivideByZeroException || ex is OverflowException) {
                        labelInfo.Text = "Infinity/NaN";
                    }
                }    
            } else {
                    labelInfo.Text = "Please Enter A Valid Expression";
            }
        }
        private bool validateExpression() {
            string NoParenth = memTextBox.Text.Replace("(", "").Replace(")", "");
            Regex regex = new Regex(@"^[-+]?([0-9]|\-?\d+\.\d)+([-+*/]+[-+]?([0-9]|\-?\d+\.\d)+)*$");
            //Regex regex = new Regex(@"^[-+]?[0-9]+([-+*/]+[-+]?[0-9]+)*$");
            return regex.IsMatch(NoParenth) ? true : false;
        }
        private void shiftCursor() {
            primTextBox.SelectionStart = primTextBox.Text.Length + 1;
            primTextBox.SelectionLength = 0;
        }
        // Note: Repeat Operator Example: 0+0 or 1*1 or 1/1, etc
        private void AppendToMemBox(string op, bool flip) {
            int len = 0;
            char prevOp = ' ';
            // If memBox not empty, create Previous Operator 
            if (TextNotEmpty(memTextBox.Text)) {
                len = memTextBox.Text.Length;
                prevOp = (char)memTextBox.Text[len-1];
            }
            // If MemBox and PrimaryBox are Empty Append a 0 + 
            if (!TextNotEmpty(memTextBox.Text) && !TextNotEmpty(primTextBox.Text)) {
                memTextBox.Text += 0;
                memTextBox.Text += op;
            } 
            // If Repeat Operator and user input, append operator
            else if (prevOp.Equals(op[0]) && TextNotEmpty(primTextBox.Text)) { 
                InsertMemBox(!flip, op);
            } 
            // If Not a Repeat Operator, Add New Operator and append number
            else if (!prevOp.Equals(op[0]) && !Char.IsNumber(prevOp) && TextNotEmpty(primTextBox.Text)) {
                InsertMemBox(!flip, op);
            } 
            // If Not a Repeat and Mem Box has a number with no Operator, append operator in correct position
            else if (!prevOp.Equals(op[0]) && Char.IsNumber(prevOp)) {
                if (TextNotEmpty(primTextBox.Text)) {
                    flip = false;
                } else if (!TextNotEmpty(primTextBox.Text)) {
                    flip = true;
                }
                InsertMemBox(flip, op);
            }
            // Reset
            primTextBox.Text = string.Empty;
            primTextBox.SelectionStart = primTextBox.Text.Length + 1;
            primTextBox.SelectionLength = 0;
            primTextBox.Focus();
        }
        private void InsertMemBox(bool flip, string op) {
            if (flip) {
                memTextBox.Text += primTextBox.Text;
                memTextBox.Text += op;
            }
            else {
                memTextBox.Text += op;
                memTextBox.Text += primTextBox.Text;
            }
        }
        #region event listeners
        private void btnEquals_Click(object sender, EventArgs e) {
            Result();
        }
        private void primTextBox_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Enter:
                    Result();
                    shiftCursor();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
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
            primTextBox.Focus();
        }
        #endregion button functionality

        private void btnAdd_Click(object sender, EventArgs e) {
            AppendToMemBox("+", false);    
        }

        private void btnPow2_Click(object sender, EventArgs e) {
            if (TextNotEmpty(primTextBox.Text)) {
                Pow2Input = primTextBox.Text;
            } else {
                Pow2Input = memTextBox.Text;
            }
            bPower2Set = true;
            AppendToMemBox("^2", false);
            Result();
            bPower2Set = false;
        }
        private bool TextNotEmpty(string str) {
            if (!string.IsNullOrEmpty(str) || !string.IsNullOrWhiteSpace(str)) {
                return true;
            } else {
                return false;
            }
        }
    }
}
