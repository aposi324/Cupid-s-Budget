

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

        public BudgetStyle CurrentBudgetStyle
        {
            get => _currentBudgetStyle;
            set
            {
                _currentBudgetStyle = value;
                UpdateBudget();
           
            }
        }

        public void UpdateBudget()
        {
            switch (CurrentBudgetStyle)
            {
                case BudgetStyle.Weighted:  // Each person contributes to the monthly bills proportionally to their post-tax salary
                    var combinedSalary = (Person1.Salary * (Person1.Tax)) + (Person2.Salary * (Person2.Tax));
                    Console.WriteLine("Combined Salary: " + combinedSalary);
                    Person1.ContributionWeight = combinedSalary == 0 ? (decimal)1 : (Person1.Salary * Person1.Tax) / combinedSalary;
                    Person2.ContributionWeight = combinedSalary == 0 ? (decimal)1 : (Person2.Salary * Person2.Tax) / combinedSalary;
                    var totalExpenses = GetTotalExpenses();
                    Console.WriteLine("Total Expenses: " + totalExpenses);
                    Person1.Contribution = Person1.ContributionWeight * totalExpenses;
                    Person2.Contribution = Person2.ContributionWeight * totalExpenses;
                    Console.WriteLine(Person1.Name + ": " + Person1.Contribution);
                    Console.WriteLine(Person2.Name + ": " + Person2.Contribution);
                    break;

                case BudgetStyle.EqualSpend:
                    Person1.ContributionWeight = (Decimal)0.5;
                    Person2.ContributionWeight = (Decimal)0.5;
                    break;
                case BudgetStyle.EqualContribution: break;
            }
           
            Console.WriteLine(Person1.ContributionWeight);
            Console.WriteLine(Person2.ContributionWeight);
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
    }

}
