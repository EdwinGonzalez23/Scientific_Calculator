using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientific_Calculator.Classes
{
    class Functions
    {
        private List<string> mFunctions;

        public Functions() {
            mFunctions = new List<string>();
        }

        public bool CreateFunction(string function) {
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
                    mFunctions.Add(function);
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

        public List<string> GetFunctions() {
            return mFunctions;
        }

        public double ComputeFunction(string function, int x) {
            function = function.Replace(@"x", "*" + x);
            var result = new DataTable().Compute(function, null);
            return Convert.ToDouble(result);
        }

        private string PrepFunction(string function) {
            string NewString = function.Replace(@"x", "");
            return NewString;
        }
    }
}
