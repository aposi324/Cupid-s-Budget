using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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


        private void OnStateUpdate(object sender, EventArgs e)
        {
            // Update display for first person's data
            this.lbl_person1_salary.Text = this.state.Person1.Salary.ToString("C", CultureInfo.CurrentCulture);
            this.lbl_person1_contribution.Text = this.state.Person1.Contribution.ToString("C", CultureInfo.CurrentCulture);
            this.lbl_person1_contribution_weight.Text = this.state.Person1.ContributionWeight.ToString("P");
            this.lbl_person1_leftover.Text = ( ((this.state.Person1.Salary*this.state.Person1.Tax) / 12) - this.state.Person1.Contribution).ToString("C", CultureInfo.CurrentCulture);

            // Update display for second person's data
            this.lbl_person2_salary.Text = this.state.Person2.Salary.ToString("C", CultureInfo.CurrentCulture);
            this.lbl_person2_contribution.Text = this.state.Person2.Contribution.ToString("C", CultureInfo.CurrentCulture);
            this.lbl_person2_contribution_weight.Text = this.state.Person2.ContributionWeight.ToString("P");
            this.lbl_person2_leftover.Text = (((this.state.Person2.Salary * this.state.Person2.Tax) / 12) - this.state.Person2.Contribution).ToString("C", CultureInfo.CurrentCulture);
        }

        public FormMain()
        {
            InitializeComponent();
            state = new State(); //Initialize the state
            state.OnStateUpdate += this.OnStateUpdate;
            UpdateHousingCostLabel();
            lbl_utilities_total.Text = state.utilityExpenses.Total().ToString("C", CultureInfo.CurrentCulture);
            lbl_food_total.Text = state.foodExpenses.Total().ToString("C", CultureInfo.CurrentCulture);
            lbl_misc_total.Text = state.otherExpenses.Total().ToString("C", CultureInfo.CurrentCulture);
        }



        private void btn_person2_Click(object sender, EventArgs e)
        {
            this.state.Person2.Name = tb_person2_name.Text;
            this.state.Person2.Salary = num_person2_salary.Value;
            this.state.Person2.Tax = 1-(num_person2_tax_rate.Value * (decimal)0.01);
            Console.WriteLine(this.state.Person2);
            gb_person2_stats.Text = this.state.Person2.Name;

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

        private void btn_person1_Click_1(object sender, EventArgs e)
        {
            this.state.Person1.Name = tb_person1_name.Text;
            this.state.Person1.Salary = num_person1_salary.Value;
            this.state.Person1.Tax = 1-(num_person1_tax_rate.Value * (decimal)0.01);
            Console.WriteLine(this.state.Person1);
            gb_person1_stats.Text = this.state.Person1.Name;
            this.lbl_person1_salary.Text = this.state.Person1.Salary.ToString("C", CultureInfo.CurrentCulture);
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
                state.CurrentBudgetStyle = State.BudgetStyle.Weighted;
            } else if (rb_equal_leftover.Checked)
            {
                Console.WriteLine("Equal Leftover");
                state.CurrentBudgetStyle = State.BudgetStyle.EqualSpend;
            }
            else if (rb_equal_contribution.Checked)
            {
                Console.WriteLine("Equal Contribution");
                state.CurrentBudgetStyle = State.BudgetStyle.EqualContribution;
            }
            return;
        }



        private void rb_weighted_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton check = (RadioButton)sender;
            if (check.Checked) {
                updateResults();
            }

        }

        private void rb_equal_leftover_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton check = (RadioButton)sender;
            if (check.Checked)
            {
                updateResults();
            }
        }

        private void rb_equal_contribution_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton check = (RadioButton)sender;
            if (check.Checked)
            {
                updateResults();
            }
        }

   
    }
}
