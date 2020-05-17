using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/*
 *  course: cmps3500
 *  Final Project: Scientific Calculator
 *  date: 05/18/2020
 *  username: egonzalez (Odin)  egonzalez88 (CSUB)
 *  name: Edwin Gonzalez
 *  description: This class represents a Scientific Calculator.
 *      It can perform most basic functionalities of a scientific calculator. 
 */

namespace Scientific_Calculator.Classes
{
    class Calculator
    {
        private string PrimText; // User input
        private string MemText; // Temp Memory for Calculator
        private string LabelText;
        public bool PemdasOperatorActivated { get; set; }
        public bool AnswerCalculated = false;

        
        public void AddPressed() {
            BasicOperatorPressed("+");
        }
        public void MultPressed() {
            BasicOperatorPressed("*");
        }
        public void DividePressed() {
            BasicOperatorPressed("/");
        }
        public void SubPressed() {
            BasicOperatorPressed("-");
        }
        public void DotPressed() {
            PrimText += ".";
        }
        
        public void SignChange() {
            double num = Convert.ToDouble(PrimText);
            num *= -1;
            PrimText = num.ToString();
        }

        /*
         * The following functionalities all work in a similar way:
         * 1. Case OverridePrim or OverrideMem: assign double num the value in either
         *      PrimText or MemText and compute result AFTER SWITCH statement
         *    
         * 2. Append: Assign double num the value of PrimText but compute result inside that case
         *      and return inside that case to exit method
         *      
         * 3. Abort: Invalid Operation. Abort Method
         */

        // ln(x) = 1 / log
        public void lnPressed() {
            double num = 0;
            switch (State()) {
                case "OverridePrim":
                    if (!PemdasOperatorActivated) {
                        if (!IsValidDouble(out num, PrimText)) {
                            return;
                        }
                    }
                    break;
                case "OverrideMem":
                    if (!PemdasOperatorActivated) {
                        if (!IsValidDouble(out num, MemText)) {
                            return;
                        }
                    }
                    //num = Convert.ToDouble(MemText);
                    break;
                case "Append":
                    if (!IsValidDouble(out num, PrimText)) {
                        return;
                    }
                    //num = Convert.ToDouble(PrimText);
                    if (IsValidExpression(out double tmpResult, Math.Log10(num) / 0.4342944819)) {
                        MemText += tmpResult.ToString();
                        PrimText = string.Empty;
                        Result();
                    }
                    return;
                case "Abort":
                    LabelText = "Please Enter a Valid Expression";
                    return;
                default:
                    break;
            }
            // Math.Log10(num) / 0.4342944819;
            if (IsValidExpression(out double result, Math.Log10(num) / 0.4342944819)) {
                LabelText = "ln(" + num + ")";
                MemText = result.ToString();
                PrimText = string.Empty;
            }
        }
        public void PowPressed(int pow) {
            double num = 0;
            switch (State()) {
                case "OverridePrim":
                    if (!IsValidDouble(out num, PrimText)) {
                        return;
                    }
                    break;
                case "OverrideMem":
                    if (!IsValidDouble(out num, MemText)) {
                        return;
                    }
                    break;
                case "Append":
                    if (!IsValidDouble(out num, PrimText)) {
                        return;
                    }
                    for (int i = 0; i < pow; i++) {
                        MemText += ("" + num + "*");
                    }
                    MemText = MemText.Remove(MemText.Length - 1);
                    PrimText = string.Empty;
                    Result();
                    return;
                case "Abort":
                    LabelText = "Please Enter a Valid Expression";
                    return;
                default:
                    break;
            }
            if (IsValidExpression(out double result, Math.Pow(num, pow))) {
                LabelText = "Math.Pow(" + num + "," + pow + ")";
                MemText = result.ToString();
                PrimText = string.Empty;
            }
        }

        public void SqrtPressed(int root) {
            double num = 0;
            switch (State()) {
                case "OverridePrim":
                    if (!IsValidDouble(out num, PrimText)) {
                        return;
                    }
                    break;
                case "OverrideMem":
                    if (!IsValidDouble(out num, MemText)) {
                        return;
                    }
                    break;
                case "Append":
                    if (!IsValidDouble(out num, PrimText)) {
                        return;
                    }
                    if (IsValidExpression(out double tmpResult, Math.Ceiling(Math.Pow(num, (double)1 / root)))) {
                        MemText += tmpResult.ToString();
                        PrimText = string.Empty;
                        Result();
                    }
                    return;
                case "Abort":
                    LabelText = "Please Enter a Valid Expression";
                    return;
                default:
                    break;
            }
            if (IsValidExpression(out double result, Math.Ceiling(Math.Pow(num, (double)1 / root)))) {
                LabelText = "Math.Pow(" + num + ", (" + 1 + "/" + root + "))";
                MemText = result.ToString();
                PrimText = string.Empty;
            }
        }
        public void CosPressed() {
            double num = 0;
            switch (State()) {
                case "OverridePrim":
                    if (!IsValidDouble(out num, PrimText)) {
                        return;
                    }
                    break;
                case "OverrideMem":
                    if (!IsValidDouble(out num, MemText)) {
                        return;
                    }
                    break;
                case "Append":
                    if (!IsValidDouble(out num, PrimText)) {
                        return;
                    }
                    if (IsValidExpression(out double tmpResult, Math.Cos(num * (Math.PI / 180)))) {
                        MemText += tmpResult.ToString();
                        PrimText = string.Empty;
                        Result();
                    }
                    //double tmpResult = Math.Cos(num);
                    return;
                case "Abort":
                    LabelText = "Please Enter a Valid Expression";
                    return;
                default:
                    break;
            }
            if (IsValidExpression(out double result, Math.Cos(num*(Math.PI/180)))) {
                LabelText = "Math.Cos(" + num + ")";
                MemText = result.ToString();
                PrimText = string.Empty;
            }
        }
        public void SinPressed() {
            double num = 0;
            switch (State()) {
                case "OverridePrim":
                    if (!IsValidDouble(out num, PrimText)) {
                        return;
                    }
                    break;
                case "OverrideMem":
                    if (!IsValidDouble(out num, MemText)) {
                        return;
                    }
                    break;
                case "Append":
                    if (!IsValidDouble(out num, PrimText)) {
                        return;
                    }
                    if (IsValidExpression(out double tmpResult, Math.Sin(num*Math.PI/180))) {
                        MemText += tmpResult.ToString();
                        PrimText = string.Empty;
                        Result();
                    }
                    return;
                case "Abort":
                    LabelText = "Please Enter a Valid Expression";
                    return;
                default:
                    break;
            }
            if (IsValidExpression(out double result, Math.Sin(num * Math.PI / 180))) {
                LabelText = "Math.Sin(" + num + ")";
                MemText = result.ToString();
                PrimText = string.Empty;
            }
        }
        public void TanPressed() {
            double num = 0;
            switch (State()) {
                case "OverridePrim":
                    if (!IsValidDouble(out num, PrimText)) {
                        return;
                    }
                    break;
                case "OverrideMem":
                    if (!IsValidDouble(out num, MemText)) {
                        return;
                    }
                    break;
                case "Append":
                    if (!IsValidDouble(out num, PrimText)) {
                        return;
                    }
                    if (IsValidExpression(out double tmpResult, Math.Tan(num*Math.PI/180))) {
                        MemText += tmpResult.ToString();
                        PrimText = string.Empty;
                        Result();
                    }
                    return;
                case "Abort":
                    LabelText = "Please Enter a Valid Expression";
                    return;
                default:
                    break;
            }
            if (IsValidExpression(out double result, Math.Tan(num * Math.PI / 180))) {
                LabelText = "Math.Tan(" + num + ")";
                MemText = result.ToString();
                PrimText = string.Empty;
            }
        }
        // ------------------------ END SWITCH STATEMENT FUNCTIONALITIES -----------------------------------
        private void Result() {

            // Result called, but Mem and Prim Text are empty (no user input at all)
            if (TextNotEmpty(MemText) && TextNotEmpty(PrimText) && !LastCharOperator(MemText) && !LastCharOperator(PrimText)) {
                MemText = PrimText;
                PrimText = string.Empty;
                return;
            }
            // Move everything to Mem Text (where operation will be calculated)
            MemText += PrimText;
            MemText = Regex.Replace(MemText, @"\s", ""); // Trim White Space
            string exp = MemText;
            try {
                var result = new DataTable().Compute(exp, null);
                if (Double.IsInfinity(Convert.ToDouble(result))) {
                    LabelText = "Please Enter a Valid Expression";
                    return;
                }
                else if (Double.IsNaN(Convert.ToDouble(result))) {
                    LabelText = "Please Enter a Valid Expression";
                    return;
                }
                LabelText = "Expression: " + MemText;
                MemText = result.ToString();
                PrimText = string.Empty;
                AnswerCalculated = true;
            }
            catch (Exception ex) {
                if (ex is DivideByZeroException || ex is OverflowException || ex is SyntaxErrorException) {
                    LabelText = "Infinity/NaN/Syntax Error";
                    PrimText = string.Empty;
                }
            }
            
        }
        /* The following function returns the state of the calculator. There are 5 states.
         * 
         * 1: Memory is empty, Primary is not empty OR Mem and Prim are not empty and an operator has not been pressed:
         *      OverridePrim: Act on Primary value
         *      
         * 2: Memory is not empty, Primary is empty and an operator has not been pressed:
         *      OverrideMem: Act on Memory value
         *      
         * 3: Memory is not empty, Primary is empty and an operator has been pressed:
         *      Abort: Expression in Memory is not valid until a number is in Primary    
         *      
         * 4: Memory is not empty, Primary is no empty and an operator has been pressed:
         *      Append: Append Primary to Memory (Chain expressions together)     
         *      
         * 5: An operation has occured, ABORT as state will not make sense (CURRENTLY DOES NOTHING)
         * 
         * RETURN NONE if NONE of these STATES OCCUR
         *      
         */
        private string State() {
            if (!TextNotEmpty(MemText) && TextNotEmpty(PrimText)
                || (TextNotEmpty(MemText) && TextNotEmpty(PrimText) && !LastCharOperator(MemText))) {
                return "OverridePrim";
            } 
            else if ((TextNotEmpty(MemText) && !TextNotEmpty(PrimText) && !LastCharOperator(MemText))) {
                return "OverrideMem";
            }
            else if ((TextNotEmpty(MemText) && !TextNotEmpty(PrimText) && LastCharOperator(MemText))) {
                return "Abort";
            }
            else if (TextNotEmpty(PrimText) && TextNotEmpty(MemText) && LastCharOperator(MemText)) {
                return "Append";
            } 
            else if (PemdasOperatorActivated) {
                return "Abort";
            }
            return "none";
        }
        public string getPrim() {
            return PrimText;
        }
        public string getMem() {
            return MemText;
        }
        public string getLabel() {
            return LabelText;
        }
        public void setPrim(string str) {
            PrimText = str;
        }
        public void setMem(string str) {
            MemText = str;
        }
        public void setLabel(string str) {
            LabelText = str;
        }
        public void CalculateResult() {
            Result();
        }
        /* An operators + * / - is pressed. 
         * OverridePrim: Replace Memory with Primary 
         * OverrideMem: Append Primary to Memory
         * Append: Append Primary to Memory
         * Abort: Not a valid expression
         * None: Primary and Memory are empty, append a 0 +
         */
        private void BasicOperatorPressed(string Operator) {
            switch (State()) {
                case "OverridePrim":
                    if (TextNotEmpty(MemText) && !LastCharOperator(MemText)) {
                        MemText = (PrimText + Operator);
                        PrimText = string.Empty;
                    } else {
                        MemText = (PrimText + Operator);
                        PrimText = string.Empty;
                    }
                    break;
                case "OverrideMem":
                    MemText += (PrimText + Operator);
                    PrimText = string.Empty;
                    break;
                case "Append":
                    MemText += (PrimText + Operator);
                    PrimText = string.Empty;
                    return;
                case "Abort":
                    if (Operator.Equals("(")) {
                        MemText += (PrimText + Operator);
                        PrimText = string.Empty;
                    }
                    else if (Operator.Equals(")")) {
                        MemText += (PrimText + Operator);
                        PrimText = string.Empty;
                    }
                    else {
                        LabelText = "Please Enter a Valid Expression";
                    }
                    return;
                case "none":
                    MemText = "0" + Operator;
                    break;
                default:
                    break;
            }
        }
        private bool TextNotEmpty(string str) {
            if (!string.IsNullOrEmpty(str) || !string.IsNullOrWhiteSpace(str)) {
                return true;
            }
            else {
                return false;
            }
        }
        // Check if last character in MemText is an Operator 
        // If True: a valid expression still needs to be formed
        private bool LastCharOperator(string str) {
            if (TextNotEmpty(str)) {
                int len = str.Length;
                char chr = str[len - 1];
                if (!Char.IsNumber(chr)) {
                    return true;
                }
                else return false;
            }
            else {
                return false;
            }
        }

        public void ClearCalculator() {
            PrimText = string.Empty;
            MemText = string.Empty;
            PemdasOperatorActivated = false;
            AnswerCalculated = false;
        }
        // Check if a string is a valid double representation 
        // Used to check if input into calculator is valid
        private bool IsValidDouble(out double num, string expression) {
            try {
                num = Convert.ToDouble(expression);
            }
            catch {
                LabelText = "Please Enter a Valid Expression";
                num = 0;
                return false;
            }
            return true;
        }
        // Is express valid?
        private bool IsValidExpression(out double result, double expression) {
            if (Double.IsInfinity(expression)) {
                LabelText = "Please Enter a Valid Expression";
                result = 0;
                return false;
            }
            else if (Double.IsNaN(expression)) {
                LabelText = "Please Enter a Valid Expression";
                result = 0;
                return false;
            }
            result = expression;
            return true;
        }
    }
}
