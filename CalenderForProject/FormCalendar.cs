using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace CalenderForProject
{
   

    public partial class FormCalendar : Form
    {
        public class MyListSingleton
        {
            
            public static string static_day, static_month, static_year;
            private static MyListSingleton instance;
            public List<string> MyList { get; }
            private MyListSingleton()
            {
                MyList = new List<string>();
            }

            public static MyListSingleton Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new MyListSingleton();
                    }
                    return instance;
                }
            }
        }

        int year, month; // for calendar

        public FormCalendar()
        {
            ListBox Selected_Days = new ListBox();
            Selected_Days.Items.Clear();
            foreach (string item in MyListSingleton.Instance.MyList)
            {
                Selected_Days.Items.Add(item);
            }

            InitializeComponent();
          
         /*  ucDays userControl = new ucDays();
            Controls.Add(userControl);
            userControl.Dock = DockStyle.Top;

            userControl.VeriIletildi += (sender, veri) =>
            {
                if(!Selected_Days.Items.Contains(veri))
                Selected_Days.Items.Add(veri);
            };
        */
        }

        private void FormCalendar_Load(object sender, EventArgs e)
        {
            loadDays();
            /*if (!Selected_Days.Items.Contains(ucDays.Date))
                Selected_Days.Items.Add(ucDays.Date);
           */

        }

        private void Selected_Days_SelectedIndexChanged(object sender, EventArgs e)
        {

            int seçilenIndex = Selected_Days.SelectedIndex;

            if (seçilenIndex >= 0)
            {
                if (seçilenIndex >= 0 && seçilenIndex < MyListSingleton.Instance.MyList.Count)
                {
                    string seçilenSayı = MyListSingleton.Instance.MyList[seçilenIndex];

                    DialogResult result = MessageBox.Show(
                        $"Do you want to delete this date? ({seçilenSayı})",
                        "Delete date",
                        MessageBoxButtons.YesNo
                    );
                    if (result == DialogResult.Yes)
                    {
                        MyListSingleton.Instance.MyList.RemoveAt(seçilenIndex); // List koleksiyonundan sayıyı siler
                        Selected_Days.Items.RemoveAt(seçilenIndex); // ListBox'tan sayıyı siler
                    }

                }
                else
                {
                    // İndeks geçerli değil, hata durumunu işle
                    MessageBox.Show("Invalid index value.");
                }

                

               
            }
        }

        //***********CALENDAR************************


        private void FormCalendar_Load_1(object sender, EventArgs e)
        {
            loadDays();

        }

        private void loadDays()
        {

            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            LBDATE.Text = monthname + " " + year;

            MyListSingleton.static_year = year.ToString();
            MyListSingleton.static_month = month.ToString();
            // Get first day of the month
            DateTime startofthemonth = new DateTime(year, month, 1);

            // get the count of dates of the month
            int days = DateTime.DaysInMonth(year, month);

            //convert the start of the month to integer
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d"));

            //create a blank user control
            for (int i = 1; i <= dayoftheweek; i++)
            {
                ucBlank ucBlank = new ucBlank();
                daycontainer.Controls.Add(ucBlank);
            }

            // user control for days
            for (int i = 1; i <= days; i++)
            {
                ucDays ucdays = new ucDays();
                ucdays.days(i);
                daycontainer.Controls.Add(ucdays);

            }
            
        }
        
        private void btnPrevious_Click(object sender, EventArgs e)
        {

            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            month--;
            // clear container
            daycontainer.Controls.Clear();
            
            if (monthname == "January")
            {
                year--;
                month = 12;
            }

            MyListSingleton.static_year = year.ToString();
            MyListSingleton.static_month = month.ToString();

            monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            LBDATE.Text = monthname + " " + year;

            DateTime startofthemonth = new DateTime(year, month, 1);

            // get the count of dats of the month
            int days = DateTime.DaysInMonth(year, month);

            //convert the startofthe month to integer
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            //create a blank user control
            for (int i = 1; i < dayoftheweek; i++)
            {
                ucBlank ucBlank = new ucBlank();
                daycontainer.Controls.Add(ucBlank);
            }

            // user control for days
            for (int i = 1; i <= days; i++)
            {
                ucDays ucdays = new ucDays();
                ucdays.days(i);
                daycontainer.Controls.Add(ucdays);

            }
            
        }

        

        private void btnNext_Click(object sender, EventArgs e)
        {
            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            // clear container
            daycontainer.Controls.Clear();

            if (monthname == "December")
             {
                 year++;
                month = 0;
            }
            
            month++;

            MyListSingleton.static_year = year.ToString();
            MyListSingleton.static_month = month.ToString();

            monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            LBDATE.Text = monthname + " " + year;

            DateTime startofthemonth = new DateTime(year, month, 1);

            // get the count of dats of the month
            int days = DateTime.DaysInMonth(year, month);

            //convert the startofthe month to integer
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            //create a blank user control
            for (int i = 1; i < dayoftheweek; i++)
            {
                ucBlank ucBlank = new ucBlank();
                daycontainer.Controls.Add(ucBlank);
            }

            // user control for days
            for (int i = 1; i <= days; i++)
            {
                ucDays ucdays = new ucDays();
                ucdays.days(i);
                daycontainer.Controls.Add(ucdays);

            }

            for (int i = 1; i <= 42 - (days + dayoftheweek); i++)
            {
                ucBlank ucBlank = new ucBlank();
                daycontainer.Controls.Add(ucBlank);
            }
        }
        
        
        
      
        //**************************************************************
    }
}
