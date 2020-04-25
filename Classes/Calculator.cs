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
                    if (!PemdasOperatorActivated)
                        num = Convert.ToDouble(PrimText);
                    break;
                case "OverrideMem":
                    if (!PemdasOperatorActivated)
                        num = Convert.ToDouble(MemText);
                    break;
                case "Append":
                    num = Convert.ToDouble(PrimText);
                    double tmpResult = Math.Log10(num) / 0.4342944819;
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
            double result = Math.Log10(num) / 0.4342944819;
            LabelText = "ln(" + num + ")";
            MemText = result.ToString();
            PrimText = string.Empty;
        }
        public void PowPressed(int pow) {
            double num = 0;
            switch (State()) {
                case "OverridePrim":
                    RemoveLastPrimOp();
                    num = Convert.ToDouble(PrimText);
                    break;
                case "OverrideMem":
                    RemoveLastMemOp();
                    num = Convert.ToDouble(MemText);
                    break;
                case "Append":
                    RemoveLastPrimOp();
                    num = Convert.ToDouble(PrimText);
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
        public void ExpPressed(int exp) {

        }
        private void Result() {
            MemText += PrimText;
            MemText = Regex.Replace(MemText, @"\s", ""); // Trim White Space
            SanitizeUserInput();
            //if (!string.IsNullOrEmpty(Pow2Input))
            //    MemText = Regex.Replace(MemText, @"(\w*[0-9]\^2)", Pow2Input + "*" + Pow2Input);
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
                    if (ex is DivideByZeroException || ex is OverflowException) {
                        LabelText = "Infinity/NaN";
                    }
                }
            }
        }
        private void SanitizeUserInput() {
            // Power 2
            //List<string> str = Regex.Split(MemText, @"\d*(?= (\^2))");
            //string[] word = Regex.Split(MemText, @"\d+(?=(\^2))");
            //Match[] words = Regex.Matches(MemText, @"\d+(?=(\^2))").Cast<Match>().ToArray();
            //Regex.Replace(MemText, @"\d+(?=(\^2))", "aaaa");
        }
        private string State() {
            if (!TextNotEmpty(MemText) && TextNotEmpty(PrimText)
                || (TextNotEmpty(MemText) && TextNotEmpty(PrimText) && !LastCharOperator(MemText))) {
                return "OverridePrim";
            } else if ((TextNotEmpty(MemText) && !TextNotEmpty(PrimText) && !LastCharOperator(MemText))) {
                return "OverrideMem";
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

            if (!PemdasOperatorActivated) {
                if (!TextNotEmpty(MemText) && !TextNotEmpty(PrimText)) {
                    MemText += (0 + Operator);
                    PrimText = string.Empty;
                    PemdasOperatorActivated = true;
                }
                else if (!AnswerCalculated) {
                    MemText += (PrimText + Operator);
                    PrimText = string.Empty;
                    PemdasOperatorActivated = true;
                }
                else if (AnswerCalculated) {
                    if (!TextNotEmpty(PrimText)) {
                        MemText += (PrimText + Operator);
                        PrimText = string.Empty;
                        AnswerCalculated = false;
                    } else {
                        MemText += (Operator + PrimText);
                        PrimText = string.Empty;
                    }
                    PemdasOperatorActivated = false;
                }
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

        public void RemoveLastPrimOp() {
            char LastChar = ' ';
            if (TextNotEmpty(PrimText)) {
                 LastChar = PrimText[PrimText.Length - 1];

                if (LastChar.Equals('^')) {
                    PrimText = PrimText.TrimEnd(PrimText[PrimText.Length - 1]);
                }
            }
        }

        public void RemoveLastMemOp() {
            char LastChar = ' ';
            if (TextNotEmpty(MemText)) {
                LastChar = MemText[MemText.Length - 1];

                if (LastChar.Equals('^')) {
                    MemText = MemText.TrimEnd(MemText[MemText.Length - 1]);
                }
            }
        }
    }
}
