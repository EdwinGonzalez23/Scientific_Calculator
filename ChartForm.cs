﻿using System;
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

        public void SetDataPoints(string function) {
            int y = ComputeDataPoints(function);
            Graph.Series[0].Points.AddXY(0, 0);
            Graph.Series[0].Points.AddXY(10, y);
        }

        public void SetGraphType(string type) {
            if (type.Equals("line")) {
                //Graph.ChartAreas[0].AxisY.ScaleView.Zoom(-15, 15);
                //Graph.ChartAreas[0].AxisX.ScaleView.Zoom(-15, 15);
                Graph.Series[0].ChartType = SeriesChartType.Line;
            }
        }

        private int ComputeDataPoints(string function) {
            function = function.Replace(@"x", "*" + 10);
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