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
using Scientific_Calculator.Classes;
using Scientific_Calculator.Popups;

namespace Scientific_Calculator.Controlers
{
    public partial class ctlCalculator : UserControl
    {
        string Pow2Input;
        bool bPower2Set = true;
        string SqrtInput;
        bool bSqrtSet = false;
        string LastNum = "0";
        bool bPowerInput = false;
        Calculator calculator;
        public ctlCalculator() {
            InitializeComponent();
            calculator = new Calculator();
        }

        #region numbers clicked
        private void btn0_Click(object sender, EventArgs e)
        {
            //primTextBox.Text += "0";
            calculator.setPrim("0");
            primTextBox.Focus();
            calculator.PemdasOperatorActivated = false;
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "1";
            primTextBox.Focus();
            calculator.PemdasOperatorActivated = false;
        }
        private void btn2_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "2";
            primTextBox.Focus();
            calculator.PemdasOperatorActivated = false;
        }
        private void btn3_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "3";
            primTextBox.Focus();
            calculator.PemdasOperatorActivated = false;
        }
        private void btn4_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "4";
            primTextBox.Focus();
            calculator.PemdasOperatorActivated = false;
        }
        private void btn5_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "5";
            primTextBox.Focus();
            calculator.PemdasOperatorActivated = false;
        }
        private void btn6_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "6";
            primTextBox.Focus();
            calculator.PemdasOperatorActivated = false;
        }
        private void btn7_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "7";
            primTextBox.Focus();
            calculator.PemdasOperatorActivated = false;
        }
        private void btn8_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "8";
            primTextBox.Focus();
            calculator.PemdasOperatorActivated = false;
        }
        private void btn9_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "9";
            primTextBox.Focus();
            calculator.PemdasOperatorActivated = false;
        }
        #endregion

        #region topthree
        private void btnClear_Click(object sender, EventArgs e) {
            calculator.ClearCalculator();
            FillCalcInfo();
            primTextBox.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            string str = primTextBox.Text;
            int len = str.Length;
            if (len > 0)
                primTextBox.Text = (str.Substring(0, len - 1));
        }
        #endregion
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
        #region Keyboard Listeners
        private void primTextBox_KeyDown(object sender, KeyEventArgs e) {
            Console.WriteLine(e.KeyCode);
            switch (e.KeyCode) {
                case Keys.Enter:
                    SuppressKey(sender, e);
                    calculator.CalculateResult();
                    FillCalcInfo();
                    return;
                case Keys.Add:
                    SuppressKey(sender, e);
                    btnAdd_Click(sender, e);
                    return;
                case Keys.Multiply:
                    SuppressKey(sender, e);
                    btnMult_Click(sender, e);
                    return;
                case Keys.Subtract:
                    SuppressKey(sender, e);
                    btnSub_Click(sender, e);
                    return;
                case Keys.Divide:
                    SuppressKey(sender, e);
                    btnDiv_Click(sender, e);
                    return;
                case Keys.D6:
                    SuppressKey(sender, e);
                    //calculator.RemoveLastPrimOp('^');
                    btnExp_Click(sender, e);
                    return;
                case Keys.Escape:
                    SuppressKey(sender, e);
                    calculator.ClearCalculator();
                    FillCalcInfo();
                    primTextBox.Focus();
                    return;
                default:
                    calculator.PemdasOperatorActivated = false;
                    break;
            }
            shiftCursor();
        }
        private void primTextBox_Enter(object sender, EventArgs e) {
            shiftCursor();
        }
        #endregion event listeners

        #region +*/- buttons
        private void btnAdd_Click(object sender, EventArgs e) {
            calculator.AddPressed();
            primTextBox.Text = calculator.getPrim();
            memTextBox.Text = calculator.getMem();
            primTextBox.Focus();
        }
        private void btnDiv_Click(object sender, EventArgs e) {
            calculator.DividePressed();
            primTextBox.Text = calculator.getPrim();
            memTextBox.Text = calculator.getMem();
            primTextBox.Focus();
        }

        private void btnMult_Click(object sender, EventArgs e) {
            calculator.MultPressed();
            primTextBox.Text = calculator.getPrim();
            memTextBox.Text = calculator.getMem();
            primTextBox.Focus();
        }

        private void btnSub_Click(object sender, EventArgs e) {
            calculator.SubPressed();
            primTextBox.Text = calculator.getPrim();
            memTextBox.Text = calculator.getMem();
            primTextBox.Focus();
        }
        #endregion +*/- buttons

        #region Non +*/- buttons
        private void btnEquals_Click(object sender, EventArgs e) {
            // Result();
            calculator.CalculateResult();
            FillCalcInfo();
            primTextBox.Focus();
        }
        private void btnSign_Click(object sender, EventArgs e) {
            calculator.SignChange();
            FillCalcInfo();
            primTextBox.Focus();
        }
        private void btnDot_Click(object sender, EventArgs e) {
            calculator.DotPressed();
            FillCalcInfo();
            primTextBox.Focus();
        }
        private void btnSqrt_Click(object sender, EventArgs e) {
            calculator.SqrtPressed(2);
            FillCalcInfo();
            primTextBox.Focus();
        }
        private void btnSqrt3_Click(object sender, EventArgs e) {
            calculator.SqrtPressed(3);
            FillCalcInfo();
            primTextBox.Focus();
        }
        private void btnPow2_Click(object sender, EventArgs e) {
            calculator.PowPressed(2);
            FillCalcInfo();
            primTextBox.Focus();
        }
        private void btnPow3_Click(object sender, EventArgs e) {
            calculator.PowPressed(3);
            FillCalcInfo();
            primTextBox.Focus();
        }
        private void btnCos_Click(object sender, EventArgs e) {
            calculator.CosPressed();
            FillCalcInfo();
            primTextBox.Focus();
        }
        private void btnSin_Click(object sender, EventArgs e) {
            calculator.SinPressed();
            FillCalcInfo();
            primTextBox.Focus();
        }
        private void btnTan_Click(object sender, EventArgs e) {
            calculator.TanPressed();
            FillCalcInfo();
            primTextBox.Focus();
        }
        private void btnLn_Click(object sender, EventArgs e) {
            calculator.lnPressed();
            FillCalcInfo();
            primTextBox.Focus();
        }
        #endregion Non +*/- buttons
        private bool TextNotEmpty(string str) {
            if (!string.IsNullOrEmpty(str) || !string.IsNullOrWhiteSpace(str)) {
                return true;
            } else {
                return false;
            }
        }
        private bool LastCharOperator(string str) {
            if (TextNotEmpty(str)) {
                int len = str.Length;
                char chr = str[len - 1];
                if (!Char.IsNumber(chr)) {
                    return true;
                }
                else return false;
            } else {
                return false;
            }
            
        }
        #region ignore
        private void AppendToMemBox(string op, bool flip) {
            int len = 0;
            char prevOp = ' ';
            // If memBox not empty, create Previous Operator 
            if (TextNotEmpty(memTextBox.Text)) {
                len = memTextBox.Text.Length;
                prevOp = (char)memTextBox.Text[len - 1];
            }
            // If MemBox and PrimaryBox are Empty Append a 0 op 
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
                }
                else if (!TextNotEmpty(primTextBox.Text)) {
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
        #endregion ignore
        private void FillCalcInfo() {
            primTextBox.Text = calculator.getPrim();
            memTextBox.Text = calculator.getMem();
            labelInfo.Text = calculator.getLabel();
        }
        string[] prenum = null;
        Match[] pre = null;
        string postnum = null;
        Match[] words = null;
        private void primTextBox_TextChanged(object sender, EventArgs e) {
            calculator.setPrim(primTextBox.Text);
            //string s = calculator.getPrim();
            //string t = calculator.getPrim().Substring(Math.Max(0, calculator.getPrim().Length - 2));
            //Regex regex = new Regex(@"\^[0-9]*");
            //if (regex.IsMatch(t)) {
            //    bPowerInput = true;
            //    if (!calculator.PemdasOperatorActivated)
            //        pre = Regex.Matches(calculator.getPrim(), @"\d+(?=(\^2))").Cast<Match>().ToArray();
            //        //prenum = Regex.Split(calculator.getPrim(), @"\d +(?=(\^2))"); // All Values Before ^[number]
            //    else if (calculator.PemdasOperatorActivated) {
            //        postnum = calculator.getPrim().Substring(calculator.getPrim().IndexOf("^") + 1);
            //        //words = Regex.Matches(calculator.getPrim(), @"\d+(?=(\^2))").Cast<Match>().ToArray();
            //    }
            //    // Grab Entire Word Before 2
            //}
            ////OperatorPhysicalPressed(sender.ToString(), sender, e);
        }

        private void memTextBox_TextChanged(object sender, EventArgs e) {
            //calculator.setMem(memTextBox.Text);
        }

        private void btnExp_Click(object sender, EventArgs e) {
            bool IsNum = false;
            int exp = 0;
            while (!IsNum) {
                string exponent = Microsoft.VisualBasic.Interaction.InputBox("Enter Exponent", string.Empty, string.Empty, -1, -1);
                if (int.TryParse(exponent, out exp) && exp >= 0) {
                    IsNum = true;
                }
            }

            calculator.PowPressed(exp);
            FillCalcInfo();
            primTextBox.Focus();
        }
        private void SuppressKey(object sender, KeyEventArgs e) {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
        private void OperatorPhysicalPressed(string str, object sender, EventArgs e) {
            switch (str) {
                case "+":
                    btnAdd_Click(sender, e);
                    break;
            }
        }

        private void UserValidation() {

        }

        private void primTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-')
                && (e.KeyChar != '^')) {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)) {
                e.Handled = true;
            }
        }

        private void memTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-')) {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)) {
                e.Handled = true;
            }
        }
    }
}
