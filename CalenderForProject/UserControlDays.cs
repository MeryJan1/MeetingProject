using System;
using System.Windows.Forms;

namespace CalenderForProject
{
    public partial class ucDays : UserControl
    {
        public ucDays()
        {
            InitializeComponent();
        }

        

        public void days(int numdays)
        {
            lbdays.Text = numdays + "";

        }

        private void UserControlDays_Click(object sender, EventArgs e)
        {

        }
    }
}
