using System;
using System.Windows.Forms;
using static CalenderForProject.FormCalendar;

namespace CalenderForProject
{
    public partial class ucDays : UserControl
    {
        

        public static string Date;
        public ucDays()
        {
            InitializeComponent();
        }

        

        public void days(int numdays)
        {
            lbdays.Text = numdays + "";

        }



        private void ucDays_Click(object sender, EventArgs e)
        {
            static_day =lbdays.Text;

            string date = static_day + "." + static_month + "." + static_year;
            if (!TarihListesi.Contains(date))
            {
                TarihListesi.Add(date);
                label1.Text = "Selected";
            }
            else
            {
                TarihListesi.Remove(date);
                label1.Text = "";
            }
        }

        private void ucDays_Load(object sender, EventArgs e)
        {

        }
    }
}
