using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Scientific_Calculator.Classes
{
    class Calculator
    {
        private string PrimText;
        private string MemText;
        private string LabelText;
        public bool PemdasOperatorActivated { get; set; }
        public bool NumberActivated { get; set; }
        public bool FunctionActivated { get; set; } //NON PEMDAS OPERATOR
        public bool AnswerCalculated = false;

        public void SqrtPressed() {
            // Calculate Result
        }
        public void AddPressed() {
            AppendToMem("+");
            //MemText += (PrimText + "+");
            //PrimText = string.Empty;
        }
        public void MultPressed() {
            AppendToMem("*");
            //MemText += (PrimText + "*");
            //PrimText = string.Empty;
        }
        public void DividePressed() {
            AppendToMem("/");
            //MemText += (PrimText + "/");
            //PrimText = string.Empty;
        }
        public void SubPressed() {
            AppendToMem("-");
            //MemText += (PrimText + "-");
            //PrimText = string.Empty;
        }
        public void DotPressed() {
            PrimText += ".";
            //PrimText = string.Empty;
        }

        public void SignChange() {
            double num = Convert.ToDouble(PrimText);
            num *= -1;
            PrimText = num.ToString();
        }
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
                    try {
                        num = Convert.ToDouble(PrimText);
                    } catch {
                        LabelText = "Please Enter a Valid Expression";
                        return;
                    }
                    break;
                case "OverrideMem":
                    try {
                        num = Convert.ToDouble(MemText);
                    } catch {
                        return;
                    }
                    break;
                case "Append":
                    try {
                        num = Convert.ToDouble(PrimText);
                    } catch {
                        LabelText = "Please Enter a Valid Expression";
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
                    
                    return;
                default:
                    break;
            }

            double result = Math.Pow(num, pow);
            LabelText = "Math.Pow(" + num + "," + pow + ")";
            MemText = result.ToString();
            PrimText = string.Empty;
        }

        public void SqrtPressed(int root) {
            double num = 0;
            switch (State()) {
                case "OverridePrim":
                    num = Convert.ToDouble(PrimText);
                    break;
                case "OverrideMem":
                    num = Convert.ToDouble(MemText);
                    break;
                case "Append":
                    num = Convert.ToDouble(PrimText);
                    double tmpResult = Math.Ceiling(Math.Pow(num, (double)1 / root));
                    MemText += tmpResult.ToString();
                    PrimText = string.Empty;
                    Result();
                    return;
                case "Abort":
                    LabelText = "Please Enter a Valid Expression";
                    return;
                default:
                    break;
            }
            LabelText = "Math.Pow(" + num + ", (" + 1 + "/" + root + "))";
            double result = Math.Ceiling(Math.Pow(num, (double)1 / root));
            MemText = result.ToString();
            PrimText = string.Empty;
        }
        public void CosPressed() {
            double num = 0;
            switch (State()) {
                case "OverridePrim":
                    num = Convert.ToDouble(PrimText);
                    break;
                case "OverrideMem":
                    num = Convert.ToDouble(MemText);
                    break;
                case "Append":
                    num = Convert.ToDouble(PrimText);
                    double tmpResult = Math.Cos(num);
                    MemText += tmpResult.ToString();
                    PrimText = string.Empty;
                    Result();
                    return;
                case "Abort":
                    LabelText = "Please Enter a Valid Expression";
                    return;
                default:
                    break;
            }
            LabelText = "Math.Cos(" + num + ")";
            double result = Math.Cos(num);
            MemText = result.ToString();
            PrimText = string.Empty;
        }
        public void SinPressed() {
            double num = 0;
            switch (State()) {
                case "OverridePrim":
                    num = Convert.ToDouble(PrimText);
                    break;
                case "OverrideMem":
                    num = Convert.ToDouble(MemText);
                    break;
                case "Append":
                    num = Convert.ToDouble(PrimText);
                    double tmpResult = Math.Sin(num);
                    MemText += tmpResult.ToString();
                    PrimText = string.Empty;
                    Result();
                    return;
                case "Abort":
                    LabelText = "Please Enter a Valid Expression";
                    return;
                default:
                    break;
            }
            LabelText = "Math.Sin(" + num + ")";
            double result = Math.Sin(num);
            MemText = result.ToString();
            PrimText = string.Empty;
        }
        public void TanPressed() {
            double num = 0;
            switch (State()) {
                case "OverridePrim":
                    num = Convert.ToDouble(PrimText);
                    break;
                case "OverrideMem":
                    num = Convert.ToDouble(MemText);
                    break;
                case "Append":
                    num = Convert.ToDouble(PrimText);
                    double tmpResult = Math.Tan(num);
                    MemText += tmpResult.ToString();
                    PrimText = string.Empty;
                    Result();
                    return;
                case "Abort":
                    LabelText = "Please Enter a Valid Expression";
                    return;
                default:
                    break;
            }
            LabelText = "Math.Tan(" + num + ")";
            double result = Math.Tan(num);
            MemText = result.ToString();
            PrimText = string.Empty;
        }
        private void Result() {

            // Enter Hit But No Operator (Just Override)
            if (TextNotEmpty(MemText) && TextNotEmpty(PrimText) && !LastCharOperator(MemText) && !LastCharOperator(PrimText)) {
                MemText = PrimText;
                PrimText = string.Empty;
                return;
            }
            MemText += PrimText;
            MemText = Regex.Replace(MemText, @"\s", ""); // Trim White Space
            if (1 == 1) {
                string exp = MemText;
                try {
                    var result = new DataTable().Compute(exp, null);
                    LabelText = "Expression: " + MemText;
                    MemText = result.ToString();
                    PrimText = string.Empty;
                    AnswerCalculated = true;
                }
                catch (Exception ex) {
                    if (ex is DivideByZeroException || ex is OverflowException || ex is SyntaxErrorException) {
                        LabelText = "Infinity/NaN/Syntax Error";
                    }
                }
            }
        }
        private string State() {
            if (!TextNotEmpty(MemText) && TextNotEmpty(PrimText)
                || (TextNotEmpty(MemText) && TextNotEmpty(PrimText) && !LastCharOperator(MemText))) {
                return "OverridePrim";
            } else if ((TextNotEmpty(MemText) && !TextNotEmpty(PrimText) && !LastCharOperator(MemText))) {
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
        /* Name: AppendToPrim()
         * Parameters: The Operator being appended
         * Return: void
         * Description: Handles data being inserted into memory (MemText)
         *  Prevents Operator's from being repeated by user. 
         *  Example: 3++3 repeats the + operator twice. 
         */
        private void AppendToMem(string Operator) {
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
                    LabelText = "Please Enter a Valid Expression";
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
