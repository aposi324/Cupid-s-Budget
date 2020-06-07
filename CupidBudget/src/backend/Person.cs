using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CupidLogic
{
    public class Person
    {
       
        public string Name { get; set; }
        private decimal tax;
        public decimal Tax {
            get
            {
                return tax;
            }
            set
            {
                tax = value;
                MonthlyGross = (Salary * Tax) / 12;
            }
        }

        private decimal salary;
        public decimal Salary { 
            get
            {
                return salary;
            }
            set
            {
                salary = value;
                MonthlyGross = (Salary * Tax) / 12;
            }
        }


        private decimal contribution;
        public decimal Contribution { 
            get
            {
                return contribution;
            }
            set
            {
                contribution = value;
                LeftOver = ((Salary * Tax) / 12) - Contribution;
            }
        }

        public decimal LeftOver { get; set; }
        public decimal MonthlyGross { get; set; }

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
