using System;
using System.Windows.Forms;
using static CalenderForProject.FormCalendar;

namespace CalenderForProject
{
    public partial class ucDays : UserControl
    {
        public event EventHandler<string> TarihTiklandi;

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
            MyListSingleton.static_day = lbdays.Text;
            MyListSingleton.Instance.MyList.Add(MyListSingleton.static_day + "/" + MyListSingleton.static_month + "/" + MyListSingleton.static_year);
            

        }

}
}
