using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CupidLogic
{

    // Keeps the overall state of a budget
    [Serializable]
    public class State
    {
        //public ExpenseList MonthlyExpenses;
        public ExpenseList housingExpenses;
        public ExpenseList utilityExpenses;
        public ExpenseList FoodExpenses;
        public ExpenseList otherExpenses;
        public List<Expense> OtherExpenses { get; set; }
        public Person Person1 { get; set; }
        public Person Person2 { get; set; }
        public State()
        {
            housingExpenses = new ExpenseList();
            utilityExpenses = new ExpenseList();
            FoodExpenses = new ExpenseList();
            otherExpenses = new ExpenseList();
            housingExpenses.Add(new Expense("Rent", 1000));
            utilityExpenses.Add(new Expense("Electric", 150));
            utilityExpenses.Add(new Expense("Internet", 40));
            otherExpenses.Add(new Expense("Alex Student Loans", 300));
            otherExpenses.Add(new Expense("Savings", 300));
            Person1 = new Person();
            Person2 = new Person();
        }
    }

}
