using System;
using System.Drawing;
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

        public void ChangeBackgroundColor(Color newColor)
        {
            this.BackColor = newColor;
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
                ChangeBackgroundColor(Color.LightGreen);
                
            }
            else
            {
                TarihListesi.Remove(date);
                ChangeBackgroundColor(Color.LightSteelBlue);
            }
        }

    }
}
