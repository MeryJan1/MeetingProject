using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static CalenderForProject.FormCalenderJoinedWithCode;
using static CalenderForProject.FormJoinwithCode;

namespace CalenderForProject
{
    public partial class FormCalenderJoinedWithCode : Form
    {
        public static List<string> Tarihlistesi = new List<string> { };
        public static string Static_Day, Static_Month, Static_Year;
        int year, month; // for calendar

        public FormCalenderJoinedWithCode()
        {
            InitializeComponent();
        }

        private void FormCalenderJoinedWithCode_Load(object sender, EventArgs e)
        {
            loadDays();
            loadBox();
        }

        private void loadBox()
        {
            string filePath = $"C:\\Users\\lenovo\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\GirişYapanlar.txt";//GİRİŞ YAPMIŞ KİŞİLER LİSTELENECEK 
            string file = $"C:\\Users\\lenovo\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\Description.txt";
            txtBoxTitle.Text = FormCalendar.title;
            if (File.Exists(file))
            {
                // Dosyadan içeriği oku
                string content = File.ReadAllText(file);

                // RichTextBox'a yaz description burada gözükecek
                richBoxDescription.Text = content;
            }
           
            // Dosya var mı kontrolü
            if (File.Exists(filePath))
            {
                // Dosyadan satırları oku ve ListBox'a ekle toplantı günlerini seçenler burada gözükecek.
                string[] lines = File.ReadAllLines(filePath);
                lstBoxPlans.Items.AddRange(lines);
            }
            txtBoxTitle.Text = Başlık;

        }

        private void loadDays()
        {

            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            LBDATE.Text = monthname + " " + year;

            Static_Year = year.ToString();
            Static_Month = month.ToString();
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
               UserControlDaysJoinWithCode userControlDaysJoinWithCode = new UserControlDaysJoinWithCode();
                userControlDaysJoinWithCode.days(i);
                daycontainer.Controls.Add(userControlDaysJoinWithCode);

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

            Static_Year = year.ToString();
            Static_Month = month.ToString();

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
                UserControlDaysJoinWithCode userControlDaysJoinWithCode = new UserControlDaysJoinWithCode();
                userControlDaysJoinWithCode.days(i);
                daycontainer.Controls.Add(userControlDaysJoinWithCode);

            }

        }

        private void buttonOkey_Click(object sender, EventArgs e)
        {

            // tarihlistesinde bulunan stringler TümTarihler.txt klasında da bulunuyorsa o stringdeğişkeni.txt dosyasına gidip KullanıcıAdı stringini yazdıran kod.
            
            string dosyaKlasoru = $"C:\\Users\\lenovo\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\Dates";
            string tümTarihler = $"C:\\Users\\lenovo\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\Dates\\TümTarihler.txt";
            string tümKullanıcılarDosyaYolu = $"C:\\Users\\lenovo\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\GirişYapanlar.txt";

            // Tüm tarihleri oku
            string[] tarihler = File.ReadAllLines(tümTarihler);

            foreach (string tarih in tarihler)
            {
                // TarihListesi içindeki tarihleri kontrol et
                if (Tarihlistesi.Contains(tarih))
                {
                    string tarihDosyaYolu = $"C:\\Users\\lenovo\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\Dates\\{tarih}.txt";

                    // Dosya varsa ve daha önce bu kullanıcı eklenmemişse
                    if (File.Exists(tarihDosyaYolu) && !File.ReadAllText(tarihDosyaYolu).Contains(İsim))
                    {
                        // Tarih dosyasına ekle
                        using (StreamWriter sw = File.AppendText(tarihDosyaYolu))
                        {
                            sw.WriteLine(İsim);
                        }
                    }
                    if(File.Exists(tümKullanıcılarDosyaYolu) && !File.ReadAllText(tümKullanıcılarDosyaYolu).Contains(İsim))
                    {
                        // Tüm kullanıcılar dosyasına da ekle
                        using (StreamWriter sw = File.AppendText(tümKullanıcılarDosyaYolu))
                        {
                            sw.WriteLine(İsim);
                        }

                    }

                }
            }

            MessageBox.Show("Your information saved!");
            this.Close();
            FormCalenderJoinedWithCode formCalenderJoinedWithCode = new FormCalenderJoinedWithCode();
            formCalenderJoinedWithCode.Show();


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

            Static_Year = year.ToString();
            Static_Month = month.ToString();

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
                UserControlDaysJoinWithCode userControlDaysJoinWithCode = new UserControlDaysJoinWithCode();
                userControlDaysJoinWithCode.days(i);
                daycontainer.Controls.Add(userControlDaysJoinWithCode);

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
