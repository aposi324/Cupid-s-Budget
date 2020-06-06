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
        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
      
        private void OnStateUpdate(object sender, EventArgs e)
        {
            // Update display for first person's data
            this.lbl_person1_salary.Text = this.state.Person1.Salary.ToString("C", culture);
            this.lbl_person1_contribution.Text = this.state.Person1.Contribution.ToString("C", culture);
            this.lbl_person1_contribution_weight.Text = this.state.Person1.ContributionWeight.ToString("P");
            this.lbl_person1_leftover.Text = ( ((this.state.Person1.Salary*this.state.Person1.Tax) / 12) - this.state.Person1.Contribution).ToString("C", culture);

            // Update display for second person's data
            this.lbl_person2_salary.Text = this.state.Person2.Salary.ToString("C", culture);
            this.lbl_person2_contribution.Text = this.state.Person2.Contribution.ToString("C", culture);
            this.lbl_person2_contribution_weight.Text = this.state.Person2.ContributionWeight.ToString("P");
            this.lbl_person2_leftover.Text = (((this.state.Person2.Salary * this.state.Person2.Tax) / 12) - this.state.Person2.Contribution).ToString("C", culture);
        }

        public FormMain()
        {
            InitializeComponent();
            culture.NumberFormat.CurrencyNegativePattern = 1;

            state = new State(); //Initialize the state
            state.OnStateUpdate += this.OnStateUpdate;
            //UpdateHousingCostLabel();
            lbl_housing_total.Text = state.housingExpenses.Total().ToString("C", culture);
            lbl_utilities_total.Text = state.utilityExpenses.Total().ToString("C", culture);
            lbl_food_total.Text = state.foodExpenses.Total().ToString("C", culture);
            lbl_misc_total.Text = state.otherExpenses.Total().ToString("C", culture);

         
        }


        private void btn_person2_Click(object sender, EventArgs e)
        {
            this.state.Person2.Name = tb_person2_name.Text;
            this.state.Person2.Salary = num_person2_salary.Value;
            this.state.Person2.Tax = 1-(num_person2_tax_rate.Value * (decimal)0.01);
            Console.WriteLine(this.state.Person2);
            gb_person2_stats.Text = this.state.Person2.Name;
            updateResults();
        }

        private void btn_person1_Click_1(object sender, EventArgs e)
        {
            this.state.Person1.Name = tb_person1_name.Text;
            this.state.Person1.Salary = num_person1_salary.Value;
            this.state.Person1.Tax = 1 - (num_person1_tax_rate.Value * (decimal)0.01);
            Console.WriteLine(this.state.Person1);
            gb_person1_stats.Text = this.state.Person1.Name;
            this.lbl_person1_salary.Text = this.state.Person1.Salary.ToString("C", culture);
            updateResults();
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




        // The following should be refactored into a generic method for arbitrary expense list editing
        /// <summary>
        /// Housing expense edit button click event handler. Brings up dialog box to edit housing expenses.
        /// </summary>
        /// <param name="sender"> Object sending the event. </param>
        /// <param name="e"> EventArgs </param>
        private void btn_housing_expenses_Click(object sender, EventArgs e)
        {
            using (MonthlyExpenseDialog dialog = new MonthlyExpenseDialog(state.housingExpenses))
            {
                //Open dialog box to edit the expenses
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    state.housingExpenses = dialog.expenseState;
                    UpdateCostLabel(lbl_housing_total, state.housingExpenses);
                    updateResults();
                }
            }
        }

        /// <summary>
        /// Utility expense edit button click event handler. Brings up dialog box to edit utility expenses.
        /// </summary>
        /// <param name="sender"> Object sending the event. </param>
        /// <param name="e"> EventArgs </param>
        private void btn_util_edit_Click(object sender, EventArgs e)
        {
            using (MonthlyExpenseDialog dialog = new MonthlyExpenseDialog(state.utilityExpenses))
            {
                //Open dialog box to edit the expenses
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    state.utilityExpenses = dialog.expenseState;
                    UpdateCostLabel(lbl_utilities_total, state.utilityExpenses);
                    updateResults();
                }
            }
        }

        /// <summary>
        /// Food expense edit button click event handler. Brings up dialog box to edit food expenses.
        /// </summary>
        /// <param name="sender"> Object sending the event. </param>
        /// <param name="e"> EventArgs </param>
        private void btn_food_edit_Click(object sender, EventArgs e)
        {
            using (MonthlyExpenseDialog dialog = new MonthlyExpenseDialog(state.foodExpenses))
            {
                //Open dialog box to edit the expenses
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    state.foodExpenses = dialog.expenseState;
                    UpdateCostLabel(lbl_food_total, state.foodExpenses);
                    updateResults();
                }
            }
        }

        /// <summary>
        /// Misc. expense edit button click event handler. Brings up dialog box to edit misc expenses.
        /// </summary>
        /// <param name="sender"> Object sending the event. </param>
        /// <param name="e"> EventArgs </param>
        private void btn_misc_edit_Click(object sender, EventArgs e)
        {
            using (MonthlyExpenseDialog dialog = new MonthlyExpenseDialog(state.otherExpenses))
            {
                //Open dialog box to edit the expenses
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    state.otherExpenses = dialog.expenseState;
                    UpdateCostLabel(lbl_misc_total, state.otherExpenses);
                    updateResults();
                }
            }
        }

        private void UpdateCostLabel(Label lbl, ExpenseList list)
        {
            lbl.Text = list.Total().ToString("C", culture);
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
