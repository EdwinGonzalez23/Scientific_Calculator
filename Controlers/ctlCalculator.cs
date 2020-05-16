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
        Calculator Calculator;
        List<Function> FuncList;
        public ctlCalculator() {
            InitializeComponent();
            Calculator = new Calculator();
            FuncList = new List<Function>();
        }

        /* Event Listeners: Buttons 0-9, () */
        #region numbers clicked
        private void btn0_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "0";
            primTextBox.Focus();
            Calculator.PemdasOperatorActivated = false;
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "1";
            primTextBox.Focus();
            Calculator.PemdasOperatorActivated = false;
        }
        private void btn2_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "2";
            primTextBox.Focus();
            Calculator.PemdasOperatorActivated = false;
        }
        private void btn3_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "3";
            primTextBox.Focus();
            Calculator.PemdasOperatorActivated = false;
        }
        private void btn4_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "4";
            primTextBox.Focus();
            Calculator.PemdasOperatorActivated = false;
        }
        private void btn5_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "5";
            primTextBox.Focus();
            Calculator.PemdasOperatorActivated = false;
        }
        private void btn6_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "6";
            primTextBox.Focus();
            Calculator.PemdasOperatorActivated = false;
        }
        private void btn7_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "7";
            primTextBox.Focus();
            Calculator.PemdasOperatorActivated = false;
        }
        private void btn8_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "8";
            primTextBox.Focus();
            Calculator.PemdasOperatorActivated = false;
        }
        private void btn9_Click(object sender, EventArgs e)
        {
            primTextBox.Text += "9";
            primTextBox.Focus();
            Calculator.PemdasOperatorActivated = false;
        }
        private void btnLeftPara_Click(object sender, EventArgs e) {
            primTextBox.Text += "(";
            primTextBox.Focus();
            Calculator.PemdasOperatorActivated = false;
        }
        private void btnRightPara_Click(object sender, EventArgs e) {
            primTextBox.Text += ")";
            primTextBox.Focus();
            Calculator.PemdasOperatorActivated = false;
        }
        #endregion


        /* Event Listeners: Clear, Cancel, Deletes */
        #region Clear Cancel
        private void btnClear_Click(object sender, EventArgs e) {
            Calculator.ClearCalculator();
            UpdateCalculatorGUI();
            primTextBox.Focus();
        }
        // Delete 1 character from Primary Text box
        private void btnCancel_Click(object sender, EventArgs e) {
            string str = primTextBox.Text;
            int len = str.Length;
            if (len > 0) {
                primTextBox.Text = (str.Substring(0, len - 1));
                Calculator.setPrim(primTextBox.Text);
            }
            primTextBox.Focus();
        }
        // // Delete 1 character from Memory Text box
        private void btnMemDel_Click(object sender, EventArgs e) {
            string str = memTextBox.Text;
            int len = str.Length;
            if (len > 0) {
                memTextBox.Text = (str.Substring(0, len - 1));
                Calculator.setMem(memTextBox.Text);
            }
            primTextBox.Focus();
        }
        #endregion
 
        // Places Cursor at end of primTextBox after a reset occurs
        private void shiftCursor() {
            primTextBox.SelectionStart = primTextBox.Text.Length + 1;
            primTextBox.SelectionLength = 0;
        }
        
        /* Keyboard Listeners: Addition, Mult, Div, Etc */
        #region Keyboard Listeners
        private void primTextBox_KeyDown(object sender, KeyEventArgs e) {
            //Console.WriteLine(e.KeyCode);
            switch (e.KeyCode) {
                case Keys.Enter:
                    SuppressKey(sender, e);
                    Calculator.CalculateResult();
                    UpdateCalculatorGUI();
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
                case Keys.D6: // ^
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
                    Calculator.ClearCalculator();
                    UpdateCalculatorGUI();
                    primTextBox.Focus();
                    return;
                default:
                    Calculator.PemdasOperatorActivated = false;
                    break;
            }
            shiftCursor();
        }
        private void primTextBox_Enter(object sender, EventArgs e) {
            shiftCursor();
        }
        #endregion event listeners

        /* Event Listeners: Add, Sub, Mul, Sub */
        #region +*/- buttons
        private void btnAdd_Click(object sender, EventArgs e) {
            Calculator.AddPressed();
            primTextBox.Text = Calculator.getPrim();
            memTextBox.Text = Calculator.getMem();
            primTextBox.Focus();
        }
        private void btnDiv_Click(object sender, EventArgs e) {
            Calculator.DividePressed();
            primTextBox.Text = Calculator.getPrim();
            memTextBox.Text = Calculator.getMem();
            primTextBox.Focus();
        }

        private void btnMult_Click(object sender, EventArgs e) {
            Calculator.MultPressed();
            primTextBox.Text = Calculator.getPrim();
            memTextBox.Text = Calculator.getMem();
            primTextBox.Focus();
        }

        private void btnSub_Click(object sender, EventArgs e) {
            Calculator.SubPressed();
            primTextBox.Text = Calculator.getPrim();
            memTextBox.Text = Calculator.getMem();
            primTextBox.Focus();
        }
        #endregion +*/- buttons

        /* Event Listeners: Pow, Sqrt, Sin, Cos, etc (Non basic arithmitec operators) */
        #region Non +*/- buttons
        private void btnEquals_Click(object sender, EventArgs e) {
            Calculator.CalculateResult();
            UpdateCalculatorGUI();
            primTextBox.Focus();
        }
        private void btnSign_Click(object sender, EventArgs e) {
            Calculator.SignChange();
            UpdateCalculatorGUI();
            primTextBox.Focus();
        }
        private void btnDot_Click(object sender, EventArgs e) {
            Calculator.DotPressed();
            UpdateCalculatorGUI();
            primTextBox.Focus();
        }
        private void btnSqrt_Click(object sender, EventArgs e) {
            Calculator.SqrtPressed(2);
            UpdateCalculatorGUI();
            primTextBox.Focus();
        }
        private void btnSqrt3_Click(object sender, EventArgs e) {
            Calculator.SqrtPressed(3);
            UpdateCalculatorGUI();
            primTextBox.Focus();
        }
        private void btnPow2_Click(object sender, EventArgs e) {
            Calculator.PowPressed(2);
            UpdateCalculatorGUI();
            primTextBox.Focus();
        }
        private void btnPow3_Click(object sender, EventArgs e) {
            Calculator.PowPressed(3);
            UpdateCalculatorGUI();
            primTextBox.Focus();
        }
        private void btnCos_Click(object sender, EventArgs e) {
            Calculator.CosPressed();
            UpdateCalculatorGUI();
            primTextBox.Focus();
        }
        private void btnSin_Click(object sender, EventArgs e) {
            Calculator.SinPressed();
            UpdateCalculatorGUI();
            primTextBox.Focus();
        }
        private void btnTan_Click(object sender, EventArgs e) {
            Calculator.TanPressed();
            UpdateCalculatorGUI();
            primTextBox.Focus();
        }
        private void btnLn_Click(object sender, EventArgs e) {
            Calculator.lnPressed();
            UpdateCalculatorGUI();
            primTextBox.Focus();
        }
        private void btnExp_Click(object sender, EventArgs e) {
            bool IsNum = false;
            int exp = 0;
            // User Input Box - Asks for Exponent
            while (!IsNum) {
                string exponent = Microsoft.VisualBasic.Interaction.InputBox("Enter Exponent", string.Empty, string.Empty, -1, -1);
                if (int.TryParse(exponent, out exp) && exp >= 0) {
                    IsNum = true;
                }
            }
            // ^ stays appended when Keyboard ^ press occurs. Need to trim it. However, need to catch an out of bounds 
            // when function is called via GUI button press
            try {
                if (TextNotEmpty(primTextBox.Text) && primTextBox.Text[primTextBox.TextLength - 1].Equals('^')) {
                    Calculator.setPrim(primTextBox.Text.TrimEnd(primTextBox.Text[primTextBox.Text.Length - 1]));
                }
            }
            catch (System.IndexOutOfRangeException ex) {
                Calculator.setLabel("Not a valid input");
                return;
            }

            Calculator.PowPressed(exp);
            UpdateCalculatorGUI();
            primTextBox.Focus();
        }
        private void SquareRoot() {
            bool IsNum = false;
            int square = 0;
            // User Input Box - Asks for root
            while (!IsNum) {
                string exponent = Microsoft.VisualBasic.Interaction.InputBox("Enter Root", string.Empty, string.Empty, -1, -1);
                if (int.TryParse(exponent, out square) && square > 0) {
                    IsNum = true;
                }
            }
            // q stays appended when Keyboard q press occurs. Need to trim it. However, need to catch an out of bounds 
            // when function is called via GUI button press
            try {
                if (primTextBox.Text[primTextBox.TextLength - 1].Equals('q')) {
                    Calculator.setPrim(primTextBox.Text.TrimEnd(primTextBox.Text[primTextBox.Text.Length - 1]));
                }
            } catch (System.IndexOutOfRangeException ex) {
                Calculator.setLabel("Not a valid input");
                return;
            }
            Calculator.SqrtPressed(square);
            UpdateCalculatorGUI();
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
        /* Sets the Primary and Memory Textboxes of the Calculator ctl*/
        private void UpdateCalculatorGUI() {
            primTextBox.Text = Calculator.getPrim();
            memTextBox.Text = Calculator.getMem();
            labelInfo.Text = Calculator.getLabel();
        }
        private void primTextBox_TextChanged(object sender, EventArgs e) {
            Calculator.setPrim(primTextBox.Text);
        }

        // Handle a Key Stroke so it does not perform default keystroke behavior
        private void SuppressKey(object sender, KeyEventArgs e) {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        /* Allow only certain characters to be entered into the textboxes
         * The allowed characters appear in the first if statements */
        private void primTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-')
                && (e.KeyChar != '^') && (e.KeyChar != 'l') && (e.KeyChar != 's')
                && (e.KeyChar != 'q') && (e.KeyChar != 't') && (e.KeyChar != 'c')
                && (e.KeyChar != '(') && (e.KeyChar != ')')) {
                e.Handled = true;
            } else {
                Calculator.setPrim(Convert.ToString(e.KeyChar));
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)) {
                e.Handled = true;
            } else if (char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar)) {
                Calculator.setPrim(Convert.ToString(e.KeyChar));
            }

        }

        /* Code of Graph Functionality */
        #region Function/Graph

        /*  While User Input not valid, ask user to input Linear Function.
         *  If Linear Function valid, create that Function Object and add to 
         *  List of Functions. List<Function>
         */
        private void btnFuncCreate_Click(object sender, EventArgs e) {
            bool exit = false;
            while (!exit) {
                string function = Microsoft.VisualBasic.Interaction.InputBox("Enter Function: f(x) = [your input]", string.Empty, string.Empty, -1, -1);
                if (function.Equals("")) {
                    break;
                }
                if (TextNotEmpty(function)) {
                    Function func = new Function();
                    if (func.CreateFunction(function)) { // Return True if Linear Function Valid
                        FuncList.Add(func);
                        FillFunctionsCbo();
                        exit = true;
                    } else {
                        MessageBox.Show("Invalid Expression", "Invalid Expressionz", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
        }

        // Use List<Function> FuncList to Fill FUNCTIONS Combo Box
        private void FillFunctionsCbo() {
            cboFunctions.Items.Clear();
            foreach (Function function in FuncList) {
                cboFunctions.Items.Add(function.GetFunction());
            }
            cboFunctions.SelectedIndex = 0;
        }

        /* Computes a Linear Function:
         *      Ask for a Valid Variable X,
         *      grab selected function from Functions Combo Box,
         *      Compute Function (Using Function.ComputeFunction(string, int) method,
         *      Update Calculator Textboxes to show calculated result.
         */
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
                int index = cboFunctions.SelectedIndex;
                //double result = functions.ComputeFunction(cboFunctions.Text, variable);
                double result = FuncList[index].ComputeFunction(cboFunctions.Text, variable);
                Calculator.setMem(Convert.ToString(result));
                Calculator.setPrim(string.Empty);
                Calculator.setLabel(String.Format("f({0}) = {1}", variable, cboFunctions.Text));
                UpdateCalculatorGUI();
            } else {
                MessageBox.Show("No Functions Exist", "Warning", MessageBoxButtons.OK);
            }
        }
        // Pop out a Chart Form that graphs a Linear Function
        private void btnGraph_Click(object sender, EventArgs e) {
            ChartForm form = new ChartForm();
            if (cboFunctions.Items.Count > 0) {
                form.SetLegendText(cboFunctions.Text);
                form.SetGraphType("line");
                form.SetDataPoints(cboFunctions.Text);
                form.Show();
            }
        }
        #endregion


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
        private void memTextBox_TextChanged(object sender, EventArgs e) {
            //calculator.setMem(memTextBox.Text);
        }
    }
}
