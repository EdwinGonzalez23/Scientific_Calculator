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
        bool PemdasOperatorActivated;
        bool OperatorActivated;
        public void SqrtPressed() {
            // Calculate Result
        }
        public void AddPressed() {
            MemText += (PrimText + "+");
            PrimText = string.Empty;
        }
        public void MultPressed() {
            MemText += (PrimText + "*");
            PrimText = string.Empty;
        }
        public void DividePressed() {
            MemText += (PrimText + "/");
            PrimText = string.Empty;
        }
        public void SubPressed() {
            MemText += (PrimText + "-");
            PrimText = string.Empty;
        }
        public void DotPressed() {
            PrimText += ".";
            //PrimText = string.Empty;
        }
        // ln(x) = 1 / log
        public void lnPressed() {
            double num = 0;
            switch (State()) {
                case "OverridePrim":
                    num = Convert.ToDouble(PrimText);
                    break;
                case "OverrideMem":
                    num = Convert.ToDouble(MemText);
                    break;
                case "Append":
                    Result();
                    num = Convert.ToDouble(MemText);
                    break;
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
                    num = Convert.ToDouble(PrimText);
                    break;
                case "OverrideMem":
                    num = Convert.ToDouble(MemText);
                    break;
                case "Append":
                    Result();
                    num = Convert.ToDouble(MemText);
                    break;
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
                    Result();
                    num = Convert.ToDouble(MemText);
                    break;
                default:
                    break;
            }
            LabelText = LabelText = "Math.Pow(" + num + ", (" + 1 + "/" + root + "))";
            double result = Math.Ceiling(Math.Pow(num, (double)1 / root));
            MemText = result.ToString();
            PrimText = string.Empty;
        }
        public void ExpPressed(int exp) {

        }
        private void AppendToMemBox() {

        }
        private void Result() {
            MemText += PrimText;
            MemText = Regex.Replace(MemText, @"\s", ""); // Trim White Space
            //if (!string.IsNullOrEmpty(Pow2Input))
            //    MemText = Regex.Replace(MemText, @"(\w*[0-9]\^2)", Pow2Input + "*" + Pow2Input);
            if (1 == 1) {
                string exp = MemText;
                try {
                    var result = new DataTable().Compute(exp, null);
                    LabelText = "Expression: " + MemText;
                    MemText = result.ToString();
                    PrimText = string.Empty;
                }
                catch (Exception ex) {
                    if (ex is DivideByZeroException || ex is OverflowException) {
                        LabelText = "Infinity/NaN";
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
            else if (TextNotEmpty(PrimText) && TextNotEmpty(MemText) && LastCharOperator(MemText)) {
                return "Append";
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
    }
}
