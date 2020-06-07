

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CupidLogic
{
    /// <summary>
    /// This class stores the overall state of the budget. It contains the data and operates on it.
    /// </summary>

    [Serializable]
    public class State
    {
        public enum BudgetStyle { Weighted, EqualSpend, EqualContribution }
        public ExpenseList housingExpenses;
        public ExpenseList utilityExpenses;
        public ExpenseList foodExpenses;
        public ExpenseList otherExpenses;
        public List<Expense> OtherExpenses { get; set; }
        public Person Person1 { get; set; }
        public Person Person2 { get; set; }
        private BudgetStyle _currentBudgetStyle;

        public event EventHandler OnStateUpdate;
        public BudgetStyle CurrentBudgetStyle
        {
            get => _currentBudgetStyle;
            set
            {
                _currentBudgetStyle = value;
                UpdateBudget(_currentBudgetStyle);
            }
        }

        public void UpdateBudget(BudgetStyle style)
        {
       
            switch (CurrentBudgetStyle)
            {
                case BudgetStyle.Weighted:  // Each person contributes to the monthly bills proportionally to their post-tax salary
                   // var combinedSalary = (Person1.Salary * (Person1.Tax)) + (Person2.Salary * (Person2.Tax));
                    var combinedSalary = GetCombinedSalary();
                    Person1.ContributionWeight = combinedSalary == 0 ? (decimal)1 : (Person1.Salary * Person1.Tax) / combinedSalary;
                    Person2.ContributionWeight = combinedSalary == 0 ? (decimal)1 : (Person2.Salary * Person2.Tax) / combinedSalary;

                    var totalExpenses = GetTotalExpenses();
                    Person1.Contribution = Person1.ContributionWeight * totalExpenses;
                    Person2.Contribution = Person2.ContributionWeight * totalExpenses;
                    break;

                case BudgetStyle.EqualSpend:
                    var eachLeft = ((GetCombinedSalary()/12) - GetTotalExpenses()) / 2;
                    Console.WriteLine(eachLeft);
                    Person1.Contribution = ((Person1.Salary * Person1.Tax)/12) - eachLeft;
                    Person2.Contribution = ((Person2.Salary * Person2.Tax)/12) - eachLeft;

                    Person1.ContributionWeight = Person1.Contribution / GetTotalExpenses();
                    Person2.ContributionWeight = Person2.Contribution / GetTotalExpenses();
                    break;

                case BudgetStyle.EqualContribution:
                    Person1.Contribution = GetTotalExpenses() * (decimal)0.5;
                    Person2.Contribution = GetTotalExpenses() * (decimal)0.5;

                    // Update contribution weight to keep state valid
                    Person1.ContributionWeight = Person1.Contribution / GetTotalExpenses();
                    Person2.ContributionWeight = Person2.Contribution / GetTotalExpenses();
                    break;
            }
           
            OnStateUpdate?.Invoke(this,EventArgs.Empty);
        }

 
        public State()
        {
            housingExpenses = new ExpenseList();
            utilityExpenses = new ExpenseList();
            foodExpenses = new ExpenseList();
            otherExpenses = new ExpenseList();
            housingExpenses.Add(new Expense("Rent", 1000));
            utilityExpenses.Add(new Expense("Electric", 150));
            utilityExpenses.Add(new Expense("Internet", 40));
            otherExpenses.Add(new Expense("Alex Student Loans", 300));
            otherExpenses.Add(new Expense("Savings", 300));
            Person1 = new Person();
            Person2 = new Person();
            CurrentBudgetStyle = BudgetStyle.Weighted;
        }

        private Decimal GetTotalExpenses()
        {
            return housingExpenses.Total() + utilityExpenses.Total() + otherExpenses.Total() + foodExpenses.Total();
        }

        private Decimal GetCombinedSalary()
        {
            return (Person1.Salary * (Person1.Tax)) + (Person2.Salary * (Person2.Tax));
        }


    }

}
