using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CupidLogic;

namespace CupidBudget
{
    public partial class MonthlyExpenseDialog : Form
    {
        State state;
        public MonthlyExpenseDialog(State state)
        {
            InitializeComponent();
            this.state = state;
        }

        private void MonthlyExpenseDialog_Load(object sender, EventArgs e)
        {
            /*
            TextBox txt = new TextBox();
            txt.Name = "txtBox1";
            txt.Text = "helloo";
            txt.Top = 25;
            txt.Left = 0;
            this.tableLayoutPanel1.Controls.Add(txt);
            */
            /*
            Label lbl = new Label();
            lbl.Text = "I am a label";
            this.tableLayoutPanel1.Controls.Add(lbl);
            */
            foreach(Expense exp in state.housingExpenses)
            {
                var temp = new TextBox();
                temp.Text = exp.Key;
                var temp2 = new NumericUpDown();
                temp2.Maximum = (decimal)1000000;
                temp2.Value = exp.Value;
                this.tableLayoutPanel1.Controls.Add(temp);
                this.tableLayoutPanel1.Controls.Add(temp2);
            }
            /*
            // Create the VScrollBar
            VScrollBar vScrollBar1 = new VScrollBar();
            // Dock the scrollbar to the right
            vScrollBar1.Dock = DockStyle.Right;
            // Add the scroll bar to the form
            this.panel1.Controls.Add(vScrollBar1);
            */

          
        }
    }
}
