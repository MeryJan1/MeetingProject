using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using static CalenderForProject.Form1;

namespace CalenderForProject
{
    public partial class FormCalenderInformationPlaning : Form
    {

        public static string static_day, static_month, static_year, description;
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
            
            string filePath = Path.Combine(userProfilePath,"create", FormLogin.userNameSurname, FormCalendar.title, "GirişYapanlar.txt");//GİRİŞ YAPMIŞ KİŞİLER LİSTELENECEK 
            string file = Path.Combine(userProfilePath,"create", FormLogin.userNameSurname , FormCalendar.title, "Description.txt");
            txtBoxTitle.Text = FormCalendar.title;
            string path = $"{userProfilePath}\\create\\Dictionary\\Başlık.txt";
            Dictionary<string, string> DicCode = new Dictionary<string, string>();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    // Satırı yıldıza göre ayır ve key, value olarak kullan
                    string[] parts = line.Split('*');
                    if (parts.Length == 2)
                    {
                        string value = parts[0];
                        string key = parts[1];

                        // Dictionary'e ekle
                        DicCode[key] = value;
                    }
                }
            }

            txtBoxCode.Text = DicCode[FormCalendar.title];

            if (File.Exists(file))
            {
                // Dosyadan içeriği oku
                description = File.ReadAllText(file);
                richBoxDescription.Text = description;
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

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtBoxCode.Text);
            MessageBox.Show("The text has been copied to the clipboard.", "İnformation", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
