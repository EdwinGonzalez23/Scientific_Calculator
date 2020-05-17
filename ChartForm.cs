using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Scientific_Calculator
{
    public partial class ChartForm : Form
    {
        public ChartForm() {
            InitializeComponent();
        }

        public void SetLegendText(string str) {
            Graph.Series[0].LegendText = str;
        }
        // Create two points when x = 0 and x = 10 and graph a line
        public void SetDataPoints(string function) {
            int origin = ComputeDataPoints(function, 0);
            int y = ComputeDataPoints(function, 10);
            Graph.Series[0].Points.AddXY(0, origin);
            Graph.Series[0].Points.AddXY(10, y);
        }

        public void SetGraphType(string type) {
            if (type.Equals("line")) {
                //Graph.ChartAreas[0].AxisY.ScaleView.Zoom(0, 15);
                //Graph.ChartAreas[0].AxisX.ScaleView.Zoom(0, 15);
                Graph.Series[0].ChartType = SeriesChartType.Line;
            }
        }
        // Compute the Linear Function and return Y
        private int ComputeDataPoints(string function, int variable) {
            function = function.Replace(@"x", "*" + variable);
            // Try with no constant attached to X, catch -> try with constant attached to X
            try {
                var result = new DataTable().Compute(function, null);
                return Convert.ToInt32(result);
            }
            catch {
                function = function.Replace(@"*", string.Empty);
                var result = new DataTable().Compute(function, null);
                return Convert.ToInt32(result);
            }
        }
    }
}
