using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CupidLogic
{
    public class Person
    {
        public decimal Tax { get; set; }
        public decimal Salary { get; set; }
        public string Name { get; set; }

        public decimal Contribution { get; set; }

        public decimal ContributionWeight { get; set; }
        public override string ToString()
        {
            string str = "";
            str += "Name: " + Name + "\n";
            str += "Salaray: " + Salary + "\n";
            str += "Tax: " + Tax;
            return str;
        }
    }
}
