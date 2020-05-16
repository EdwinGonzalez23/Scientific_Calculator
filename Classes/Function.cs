using Microsoft.VisualBasic.Devices;
using Scientific_Calculator.Popups;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientific_Calculator.Classes
{
    class Function
    {
        private string mFunction;
        public Function() {
        }

        public bool CreateFunction(string function) {
            if(CheckFunctionValid(function)) {
                mFunction = function;
                return true;
            } 
            else {
                return false;
            }
        }

        public double ComputeFunction(string function, int x) {
            string expression;
            double result;
            // Try X with no Constant, Catch -> then try X with Constant
            try {
                expression = function.Replace(@"x", "(" + x + ")");
                result = Convert.ToDouble(new DataTable().Compute(expression, null));
            }
            catch {
                expression = function.Replace(@"x", "*(" + x + ")");
                result = Convert.ToDouble(new DataTable().Compute(expression, null));
            }
            return Convert.ToDouble(result);
        }
        
        private bool CheckFunctionValid(string function) {
            try {
                string expression = string.Empty;
                double result = 0;
                // Try X with no Constant, Catch -> then try X with Constant
                try {
                    expression = function.Replace(@"x", "(1)");
                    result = Convert.ToDouble(new DataTable().Compute(expression, null));
                }
                catch {
                    expression = function.Replace(@"x", "*(1)");
                    result = Convert.ToDouble(new DataTable().Compute(expression, null));
                }
                // Just in case something weird happens
                if (Double.IsInfinity(Convert.ToDouble(result))) {
                    return false;
                }
                else if (Double.IsNaN(Convert.ToDouble(result))) {
                    return false;
                }
                else {
                    return true;
                }
            }
            catch (Exception ex) {
                if (ex is DivideByZeroException || ex is OverflowException || ex is SyntaxErrorException) {
                    return false;
                }
            }
            return false;
        }

        public string GetFunction() {
            return mFunction;
        }
    }
}
