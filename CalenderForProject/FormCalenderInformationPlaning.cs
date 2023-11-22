using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CalenderForProject.FormLogin;
using static CalenderForProject.FormCalendar;
using System.Globalization;
using static CalenderForProject.Form1;

namespace CalenderForProject
{
    public partial class FormCalenderInformationPlaning : Form
    {

        public static string static_day, static_month, static_year;
        int year, month;

        public FormCalenderInformationPlaning()
        {
            InitializeComponent();
        }


        private void FormCalenderInformationPlaning_Load(object sender, EventArgs e)
        {
            loadDays();
            loadTxtBox();
        }

        private void loadTxtBox()
        {
            
            string filePath = Path.Combine(userProfilePath,"Documents\\create", FormLogin.userNameSurname, FormCalendar.title, "GirişYapanlar.txt");//GİRİŞ YAPMIŞ KİŞİLER LİSTELENECEK 
            string file = Path.Combine(userProfilePath,"Documents\\create", FormLogin.userNameSurname , FormCalendar.title, "Description.txt");
            txtBoxTitle.Text = FormCalendar.title;
            if (File.Exists(file))
            {
                // Dosyadan içeriği oku
                string content = File.ReadAllText(file);

                // RichTextBox'a yaz description burada gözükecek
                richBoxDescription.Text = content;
            }
            else
            {
                // Dosya bulunamazsa bir uyarı verebilirsiniz.
                MessageBox.Show("The file does not exist.");
            }
            // Dosya var mı kontrolü
            if (File.Exists(filePath))
            {
                // Dosyadan satırları oku ve ListBox'a ekle toplantı günlerini seçenler burada gözükecek.
                string[] lines = File.ReadAllLines(filePath);
                lstBoxPlans.Items.AddRange(lines);
            }
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FormCalendar formCalendar = new FormCalendar();
            formCalendar.Show();

            this.Close();
        }
            
            

        // for calendar

        private void loadDays()
        {

            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            LBDATE.Text = monthname + " " + year;

            static_year = year.ToString();
            static_month = month.ToString();
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
                UserControlInformationPlaning userControlInformationPlaning = new UserControlInformationPlaning();
                userControlInformationPlaning.days(i);
                daycontainer.Controls.Add(userControlInformationPlaning);

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

            static_year = year.ToString();
            static_month = month.ToString();

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
                UserControlInformationPlaning userControlInformationPlaning = new UserControlInformationPlaning();
                userControlInformationPlaning.days(i);
                daycontainer.Controls.Add(userControlInformationPlaning);
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

            static_year = year.ToString();
            static_month = month.ToString();

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
                UserControlInformationPlaning userControlInformationPlaning = new UserControlInformationPlaning();
                userControlInformationPlaning.days(i);
                daycontainer.Controls.Add(userControlInformationPlaning);

            }

            
        }




    }

}
