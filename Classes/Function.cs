using Microsoft.VisualBasic.Devices;
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
            function = function.Replace(@"x", "(" + x + ")");
            var result = new DataTable().Compute(function, null);
            return Convert.ToDouble(result);
        }
        // Set x = 1 so express may be run through DataTable.Compute()
        private string PrepFunction(string function) {
            string NewString = function.Replace(@"x", "(1)");
            return NewString;
        }
        private bool CheckFunctionValid(string function) {
            try {
                string expression = PrepFunction(function);
                var result = new DataTable().Compute(expression, null);
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
