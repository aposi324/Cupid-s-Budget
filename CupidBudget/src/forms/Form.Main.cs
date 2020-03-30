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
    public partial class FormMain : Form
    {
        State state;
        public FormMain()
        {
            InitializeComponent();
            state = new State(); //Initialize the state
            lbl_housing_total.Text = "$" + state.housingExpenses.Total().ToString();
            lbl_utilities_total.Text = "$" + state.utilityExpenses.Total().ToString();
            lbl_food_total.Text = "$" + state.FoodExpenses.Total().ToString();
            lbl_misc_total.Text = "$" + state.otherExpenses.Total().ToString();

        }

        private void btn_person1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("hello");
            btn_person1.Text = "Alex";
        }

        private void btn_person2_Click(object sender, EventArgs e)
        {
            this.state.Person2.Name = tb_person2_name.Text;
            this.state.Person2.Salary = num_person2_salary.Value;
            this.state.Person2.Tax = num_person2_tax_rate.Value * (decimal)0.01;
            Console.WriteLine(this.state.Person2);
            gb_person2_stats.Text = this.state.Person2.Name;
            
            /*
            Console.WriteLine("Hello 2");
            this.btn_person2.Text = "Sara";
            using (UserDialog userDialog = new UserDialog())
            {
                if (userDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.Text = userDialog.UserName;
                }
            }
            */
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_MouseClick(object sender, MouseEventArgs e)
        {
            using (UserDialog userDialog = new UserDialog())
            {
                if (userDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.Text = userDialog.UserName;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_person1_Click_1(object sender, EventArgs e)
        {
            this.state.Person1.Name = tb_person1_name.Text;
            this.state.Person1.Salary = num_person1_salary.Value;
            this.state.Person1.Tax = num_person1_tax_rate.Value * (decimal)0.01;
            Console.WriteLine(this.state.Person1);
            gb_person1_stats.Text = this.state.Person1.Name;
        }

        private void tb_person1_name_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
           // using (UserDialog userDialog = new UserDialog())
            using (MonthlyExpenseDialog dialog = new MonthlyExpenseDialog(state))
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Console.WriteLine("Heyo");
                }
            }
        }
    }
}
