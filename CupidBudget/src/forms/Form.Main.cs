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
            UpdateHousingCostLabel();
            lbl_utilities_total.Text = "$" + state.utilityExpenses.Total().ToString();
            lbl_food_total.Text = "$" + state.foodExpenses.Total().ToString();
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
            this.state.Person2.Tax = 1-(num_person2_tax_rate.Value * (decimal)0.01);
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
            this.state.Person1.Tax = 1-(num_person1_tax_rate.Value * (decimal)0.01);
            Console.WriteLine(this.state.Person1);
            gb_person1_stats.Text = this.state.Person1.Name;
            this.lbl_person1_salary.Text = this.state.Person1.Salary.ToString();
           
            //this.ch_person1.Series["asdf"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            //this.ch_person1.Series["asdf"].Points.DataBind()
        }

        private void tb_person1_name_TextChanged(object sender, EventArgs e)
        {
          
        }

        //Housing expense Edit button click event handler
        private void btn_housing_expenses_Click(object sender, EventArgs e)
        {
          
            using (MonthlyExpenseDialog dialog = new MonthlyExpenseDialog(state.housingExpenses))
            {
                //Open dialog box to edit the expenses
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Console.WriteLine("Heyo");
                    state.housingExpenses = dialog.expenseState;
                    UpdateHousingCostLabel();
                }
            }
        }


        private void UpdateHousingCostLabel()
        {
            lbl_housing_total.Text = "$" + state.housingExpenses.Total().ToString();
        }

        private void updateResults()
        {
            if (rb_weighted.Checked)
            {
                Console.WriteLine("Weighted Contribution");
                state.UpdateBudget();
            } else if (rb_equal_leftover.Checked)
            {
                Console.WriteLine("Equal Leftover");
                state.UpdateBudget();

            }
            else if (rb_equal_contribution.Checked)
            {
                Console.WriteLine("Equal Contribution");
                state.UpdateBudget();

            }
            return;
        }


        // Make these only happen once
        private void rb_weighted_CheckedChanged(object sender, EventArgs e)
        {
            updateResults();
        }

        private void rb_equal_leftover_CheckedChanged(object sender, EventArgs e)
        {
            updateResults();
        }

        private void rb_equal_contribution_CheckedChanged(object sender, EventArgs e)
        {
            updateResults();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
    }
}
