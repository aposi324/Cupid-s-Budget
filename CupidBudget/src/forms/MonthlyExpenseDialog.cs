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
       // public State state;
        public ExpenseList expenseState;
        public int numEntries { get; set; }
        public MonthlyExpenseDialog(ExpenseList expenses)
        {
            InitializeComponent();
            this.expenseState = expenses;
            numEntries = 0;
        }

        private void MonthlyExpenseDialog_Load(object sender, EventArgs e)
        {

            foreach(Expense exp in expenseState)
            {
                var temp = new TextBox();
                temp.Text = exp.Key;
                temp.Dock = DockStyle.Fill;

                var temp2 = new NumericUpDown();
                temp2.Maximum = (decimal)1000000;
                temp2.Value = exp.Value;
                temp2.AutoSize = true;
                temp2.Dock = DockStyle.Fill;
               
                this.tableLayoutPanel1.Controls.Add(temp);
                this.tableLayoutPanel1.Controls.Add(temp2);
                numEntries++;
            }


          
        }

        void onAddNewClick(object sender, EventArgs e)
        {
            Console.WriteLine("CLICKED");
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            var newText = new TextBox();
            newText.Text = "Expense Name";
            newText.Dock = DockStyle.Fill;
            newText.AutoSize = true;
            var newNum = new NumericUpDown();
            newNum.Maximum = (decimal)1000000;
            newNum.Value = (decimal)100;
            newNum.AutoSize = true;
            newNum.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(value: newText);
            tableLayoutPanel1.Controls.Add(value: newNum);
            numEntries++;
            //
            //tableLayoutPanel1.GetRow[tableLayoutPanel1.RowCount-1]
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //var str = tableLayoutPanel1.GetControlFromPosition(1, 1);
            ExpenseList newExpenses = new ExpenseList();
            for (int i = 0; i < numEntries; i++)
            {
                string str = tableLayoutPanel1.GetControlFromPosition(0, i).Text;
                Decimal num = ((NumericUpDown)(tableLayoutPanel1.GetControlFromPosition(1, i))).Value;
                Console.WriteLine(str + ", " + num.ToString());
                newExpenses.Add(new Expense(str, num));
            }
           expenseState = newExpenses;
        }
    }
}
