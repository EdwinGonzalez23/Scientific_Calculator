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
using System.Windows.Forms.DataVisualization.Charting;

namespace Scientific_Calculator.Controlers
{
    public partial class ctlCalculator : UserControl
    {
        Calculator calculator;
        Functions functions;
        public ctlCalculator() {
            InitializeComponent();
            calculator = new Calculator();
            functions = new Functions();
        }

        #region numbers clicked
        private void btn0_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "0";
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
        private void btnLeftPara_Click(object sender, EventArgs e) {
            primTextBox.Text += "(";
            primTextBox.Focus();
            calculator.PemdasOperatorActivated = false;
        }
        private void btnRightPara_Click(object sender, EventArgs e) {
            primTextBox.Text += ")";
            primTextBox.Focus();
            calculator.PemdasOperatorActivated = false;
        }
        #endregion

        #region Clear Cancel
        private void btnClear_Click(object sender, EventArgs e) {
            calculator.ClearCalculator();
            FillCalcInfo();
            primTextBox.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            string str = primTextBox.Text;
            int len = str.Length;
            if (len > 0) {
                primTextBox.Text = (str.Substring(0, len - 1));
                calculator.setPrim(primTextBox.Text);
            }
            primTextBox.Focus();
        }
        private void btnMemDel_Click(object sender, EventArgs e) {
            string str = memTextBox.Text;
            int len = str.Length;
            if (len > 0) {
                memTextBox.Text = (str.Substring(0, len - 1));
                calculator.setMem(memTextBox.Text);
            }
            primTextBox.Focus();
        }
        #endregion
 
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
            //Console.WriteLine(e.KeyCode);
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
                case Keys.Delete:
                    SuppressKey(sender, e);
                    btnMemDel_Click(sender, e);
                    return;
                case Keys.Divide:
                    SuppressKey(sender, e);
                    btnDiv_Click(sender, e);
                    return;
                case Keys.D6:
                    SuppressKey(sender, e);
                    btnExp_Click(sender, e);
                    return;
                case Keys.L:
                    SuppressKey(sender, e);
                    btnLn_Click(sender, e);
                    return;
                case Keys.Q:
                    SuppressKey(sender, e);
                    SquareRoot();
                    return;
                case Keys.C:
                    SuppressKey(sender, e);
                    btnCos_Click(sender, e);
                    return;
                case Keys.T:
                    SuppressKey(sender, e);
                    btnTan_Click(sender, e);
                    return;
                case Keys.S:
                    SuppressKey(sender, e);
                    btnSin_Click(sender, e);
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
            //if (TextNotEmpty(memTextBox.Text) && !TextNotEmpty(primTextBox.Text)) {
            //    if (mem)
            //}
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
        private void btnExp_Click(object sender, EventArgs e) {
            bool IsNum = false;
            int exp = 0;
            while (!IsNum) {
                string exponent = Microsoft.VisualBasic.Interaction.InputBox("Enter Exponent", string.Empty, string.Empty, -1, -1);
                if (int.TryParse(exponent, out exp) && exp >= 0) {
                    IsNum = true;
                }
            }
            try {
                if (TextNotEmpty(primTextBox.Text) && primTextBox.Text[primTextBox.TextLength - 1].Equals('^')) {
                    calculator.setPrim(primTextBox.Text.TrimEnd(primTextBox.Text[primTextBox.Text.Length - 1]));
                }
            }
            catch (System.IndexOutOfRangeException ex) {
                calculator.setLabel("Not a valid input");
                return;
            }

            calculator.PowPressed(exp);
            FillCalcInfo();
            primTextBox.Focus();
        }
        private void SquareRoot() {
            bool IsNum = false;
            int square = 0;
            while (!IsNum) {
                string exponent = Microsoft.VisualBasic.Interaction.InputBox("Enter Root", string.Empty, string.Empty, -1, -1);
                if (int.TryParse(exponent, out square) && square > 0) {
                    IsNum = true;
                }
            }
            try {
                if (primTextBox.Text[primTextBox.TextLength - 1].Equals('q')) {
                    calculator.setPrim(primTextBox.Text.TrimEnd(primTextBox.Text[primTextBox.Text.Length - 1]));
                }
            } catch (System.IndexOutOfRangeException ex) {
                calculator.setLabel("Not a valid input");
                return;
            }
            calculator.SqrtPressed(square);
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
        private void FillCalcInfo() {
            primTextBox.Text = calculator.getPrim();
            memTextBox.Text = calculator.getMem();
            labelInfo.Text = calculator.getLabel();
        }
        private void primTextBox_TextChanged(object sender, EventArgs e) {
            calculator.setPrim(primTextBox.Text);
        }

        private void memTextBox_TextChanged(object sender, EventArgs e) {
            //calculator.setMem(memTextBox.Text);
        }
        private void SuppressKey(object sender, KeyEventArgs e) {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
        private void primTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-')
                && (e.KeyChar != '^') && (e.KeyChar != 'l') && (e.KeyChar != 's')
                && (e.KeyChar != 'q') && (e.KeyChar != 't') && (e.KeyChar != 'c')
                && (e.KeyChar != '(') && (e.KeyChar != ')')) {
                e.Handled = true;
            } else {
                calculator.setPrim(Convert.ToString(e.KeyChar));
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)) {
                e.Handled = true;
            } else if (char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar)) {
                calculator.setPrim(Convert.ToString(e.KeyChar));
            }

        }

        private void btnFuncCreate_Click(object sender, EventArgs e) {
            bool exit = false;
            while (!exit) {
                string function = Microsoft.VisualBasic.Interaction.InputBox("Enter Function: f(x) = [your input]", string.Empty, string.Empty, -1, -1);
                if (function.Equals("")) {
                    break;
                }
                if (TextNotEmpty(function)) {
                    if (functions.CreateFunction(function)) {
                        FillFunctionCbo();
                        exit = true;
                    } else {
                        MessageBox.Show("Invalid Expression", "Invalid Expressionz", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
        }

        private void FillFunctionCbo() {
            cboFunctions.DataSource = null;
            cboFunctions.DataSource = functions.GetFunctions();
        }

        private void btnFuncComp_Click(object sender, EventArgs e) {
            bool exit = false;
            int variable = 0;
            if (cboFunctions.Items.Count > 0) {
                while (!exit) {
                    string var = Microsoft.VisualBasic.Interaction.InputBox("Enter integer X", string.Empty, string.Empty, -1, -1);
                    if (var.Equals("")) {
                        break;
                    }
                    if (TextNotEmpty(var)) {
                        exit = int.TryParse(var, out variable);
                    }
                }
                double result = functions.ComputeFunction(cboFunctions.Text, variable);
                calculator.setMem(Convert.ToString(result));
                calculator.setPrim(string.Empty);
                calculator.setLabel(String.Format("f({0}) = {1}", variable, cboFunctions.Text));
                FillCalcInfo();
            } else {
                MessageBox.Show("No Functions Exist", "Warning", MessageBoxButtons.OK);
            }
        }

        private void btnGraph_Click(object sender, EventArgs e) {
            ChartForm form = new ChartForm();
            if (cboFunctions.Items.Count > 0) {
                form.SetLegendText(cboFunctions.Text);
                form.SetGraphType("line");
                form.SetDataPoints(cboFunctions.Text);
                form.Show();
            }
        }
        // FOR TESTING ONLY
        //-----------------------------
        //private void memTextBox_KeyPress(object sender, KeyPressEventArgs e) {
        //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-')) {
        //        e.Handled = true;
        //    } else {
        //        calculator.setMem(Convert.ToString(e.KeyChar));
        //    }

        //    // only allow one decimal point
        //    if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)) {
        //        e.Handled = true;
        //    } else if (char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar)) {
        //        calculator.setMem(Convert.ToString(e.KeyChar));
        //    }
        //}
    }
}
