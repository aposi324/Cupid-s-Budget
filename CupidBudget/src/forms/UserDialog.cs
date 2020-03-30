using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CupidBudget
{
    public partial class UserDialog : Form
    {
        public String UserName { get; set; }
        public Decimal Salary { get; set; }
        public Decimal Tax { get; set; }
        public UserDialog()
        {
            InitializeComponent();
        }

        private void UserDialog_Load(object sender, EventArgs e)
        {

        }

        private void userName_TextChanged(object sender, EventArgs e)
        {
            UserName = userName.Text;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
