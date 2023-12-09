using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using static CalenderForProject.Form1;
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
            string filePath = $"{userProfilePath}\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\GirişYapanlar.txt";//GİRİŞ YAPMIŞ KİŞİLER LİSTELENECEK 
            string file = $"{userProfilePath}\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\Description.txt";
            txtBoxTitle.Text = FormCalendar.title;
            if (System.IO.File.Exists(file))
            {
                // Dosyadan içeriği oku
                string content = System.IO.File.ReadAllText(file);

                // RichTextBox'a yaz description burada gözükecek
                richBoxDescription.Text = content;
            }
           
            // Dosya var mı kontrolü
            if (System.IO.File.Exists(filePath))
            {
                // Dosyadan satırları oku ve ListBox'a ekle toplantı günlerini seçenler burada gözükecek.
               ;
                string[] lines = System.IO.File.ReadAllLines(filePath);
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
           
            
            string tümTarihler = $"{userProfilePath}\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\Dates\\TümTarihler.txt";
            string tümKullanıcılarDosyaYolu = $"{userProfilePath}\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\GirişYapanlar.txt";


            // Tüm tarihleri oku

            if (System.IO.File.Exists(tümTarihler) && System.IO.File.Exists(tümKullanıcılarDosyaYolu))
            {
                
                foreach (string tarih in System.IO.File.ReadAllLines(tümTarihler))
                {
                    // TarihListesi içindeki tarihleri kontrol et
                    if (Tarihlistesi.Contains(tarih))
                    {
                        string tarihDosyaYolu = $"{userProfilePath}\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\Dates\\{tarih}.txt";

                        // Dosya varsa ve daha önce bu kullanıcı eklenmemişse
                        if (System.IO.File.Exists(tarihDosyaYolu) && !System.IO.File.ReadAllText(tarihDosyaYolu).Contains(İsim))
                        {
                            // Tarih dosyasına ekle
                            using (StreamWriter sw = System.IO.File.AppendText(tarihDosyaYolu))
                            {
                                sw.WriteLine(İsim);
                            }
                        }
                        if (System.IO.File.Exists(tümKullanıcılarDosyaYolu) && !System.IO.File.ReadAllText(tümKullanıcılarDosyaYolu).Contains(İsim))
                        {
                            // Tüm kullanıcılar dosyasına da ekle
                            using (StreamWriter sw = System.IO.File.AppendText(tümKullanıcılarDosyaYolu))
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



        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();

            this.Close();

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




      
    }


}
