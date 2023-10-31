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

        private void ucDays_DoubleClick(object sender, EventArgs e)
        {
            // Tıklanan tarihi bir olay ile ana forma iletiyoruz
            string date = MyListSingleton.static_day + "/" + MyListSingleton.static_month + "/" + MyListSingleton.static_year;
            if (!MyListSingleton.Instance.MyList.Contains(date))
            {
                MyListSingleton.Instance.MyList.Add(date);
                label1.Text = "Selected";
            }
            else {
                MyListSingleton.Instance.MyList.Remove(date);
                label1.Text = "";
            }
                   
            
            
        }

}
}
