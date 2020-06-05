using System;
using System.Collections.Generic;
using System.Collections;

namespace CupidLogic
{
    /// <summary>
    /// This struct contains key value pairs for a single expense in a list.
    /// </summary>
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

    /// <summary>
    /// Enumerable list of expenses with operations to interface with that data.
    /// </summary>
    [Serializable]
    public class ExpenseList : IEnumerable<Expense>
    {
        List<Expense> l { get; set; }

        public ExpenseList()
        {
            l = new List<Expense>();
        }

        // Return the sum of all monthly expenses
        public decimal Total()
        {
            decimal total = 0;
            foreach (Expense exp in l)
            {
                total += exp.Value;
            }
            return total;
        }

        /// <summary>
        /// Add an expense to the end of the list
        /// </summary>
        /// <param name="e"> The expense to be added to the list.</param>
        public void Add(Expense e)
        {
            l.Add(e);
        }

        /// <summary>
        /// Access expense with [] operator
        /// </summary>
        /// <param name="i">The index to be accessed.</param>
        // TODO: Exception handling
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
