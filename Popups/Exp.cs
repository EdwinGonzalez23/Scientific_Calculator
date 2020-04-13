using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scientific_Calculator.Popups
{
    public partial class Exp : Form
    {
        public string Value {
            get { return tbText.Text.Trim(); }
        }

        public Exp() {
            InitializeComponent();
        }

        private void BtnSubmitText_Click(object sender, EventArgs e) {
            Close();
        }

        private void TextPrompt_Load(object sender, EventArgs e) {
            CenterToParent();
        }
    }
}
