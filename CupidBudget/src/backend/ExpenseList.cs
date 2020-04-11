using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CupidLogic
{
    [Serializable]
    public struct Expense
    {
        public String Key { get; set; }
        public Decimal Value { get; set; }

        public Expense(string s, decimal d)
        {
            Key = s;
            Value = d;
        }

        public override string ToString()
        {
            return Key + " , " + Value.ToString();
        }
    }

    [Serializable]
    public class ExpenseList : IEnumerable<Expense>
    {
        List<Expense> l { get; set; }

        public ExpenseList()
        {
            l = new List<Expense>();
        }

        // Return the some of all monthly expenses
        public decimal Total()
        {
            decimal total = 0;
            foreach (Expense exp in l)
            {
                total += exp.Value;
            }
            return total;
        }

        //Add and expense to the list
        public void Add(Expense e)
        {
            l.Add(e);
        }

        //Access expense with [] operator
        public Expense this[int i]
        {
            get { return l[i]; }
            set { l[i] = value; }
        }

        //To implement IEnumerable
        public IEnumerator<Expense> GetEnumerator()
        {
            for (int i = 0; i < l.Count; ++i)
                yield return l[i];
        }

        // Explicit interface implementation for nongeneric interface
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator(); // Just return the generic version
        }
    }
}
