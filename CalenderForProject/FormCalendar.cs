using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Sockets;
using System.Text;
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
           
            InitializeComponent();         
       
        }

        private void FormCalendar_Load(object sender, EventArgs e)
        {
            loadDays();
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

        private void buttonOkey_Click(object sender, EventArgs e)
        {
            FormTitle formTitle = new FormTitle();
            formTitle.Show();
        }

        private void lstBoxPlans_Click(object sender, EventArgs e)
        {
            if(lstBoxPlans.SelectedItems!=null)
            lstBoxPlans.Items.Remove(lstBoxPlans.SelectedItem);
            else
            {
                MessageBox.Show("No item selected.");
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
