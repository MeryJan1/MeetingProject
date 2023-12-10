﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static CalenderForProject.FormLogin;
using static CalenderForProject.FormCalendar;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Drive.v3;

namespace CalenderForProject
{
    public partial class UserControlInformationPlaning : UserControl
    {
        public UserControlInformationPlaning()
        {
            InitializeComponent();
        }

        public void ChangeBackColor(Color newColor)
        {
            this.BackColor = newColor;
        }

        public void days(int numdays)
        {
            lblDays.Text = numdays.ToString();
            Yükle(numdays);
           
        }

        private void Yükle(int numdays)
        {
            string tarih = numdays + "." + FormCalenderInformationPlaning. static_month + "." + FormCalenderInformationPlaning. static_year;
            string file = $"{Form1.userProfilePath}\\create\\{userNameSurname}\\{title}\\Dates\\TümTarihler.txt"; ;
            string path = $"{Form1.userProfilePath}\\create\\{userNameSurname}\\{title}\\Dates\\{tarih}.txt";
            string[] tarihler = File.ReadAllLines(file);

            if (tarihler.Contains(tarih))
            {
                ChangeBackColor(Color.LightGreen);
                lBox.BackColor = Color.LightGreen;
                string[] lines = File.ReadAllLines(path);
                // Her bir satırı ListBox'a ekle
                foreach (string line in lines)
                {
                    lBox.Items.Add(line);
                }
            }
        }

        private void UserControlInformationPlaning_Click(object sender, EventArgs e)
        {
            FormCalenderInformationPlaning.static_day = lblDays.Text;

            GoogleCalender googleCalender = new GoogleCalender();
            googleCalender.Show();

            Form parentForm = this.FindForm(); // Kullanıcı kontrolünün bağlı olduğu formu bul
            parentForm.Close();

        }
    }
}
