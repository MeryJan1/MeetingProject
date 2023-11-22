using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CalenderForProject.FormLogin;
using static CalenderForProject.FormCalendar;

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
            label2.Text = numdays.ToString();
            Yükle(numdays);
           
        }

        private void Yükle(int numdays)
        {
            string tarih = numdays + "." + FormCalenderInformationPlaning. static_month + "." + FormCalenderInformationPlaning. static_year;
            string file = $"C:\\Users\\lenovo\\Documents\\create\\{userNameSurname}\\{title}\\Dates\\TümTarihler.txt"; ;
            string path = $"C:\\Users\\lenovo\\Documents\\create\\{userNameSurname}\\{title}\\Dates\\{tarih}.txt";

            string[] tarihler = File.ReadAllLines(file);
            if (tarihler.Contains(tarih))
            {
                ChangeBackColor(Color.LightGreen);
                lBox.BackColor = Color.LightGreen;
                string[] lines = File.ReadAllLines(path);
                // Her bir satırı ListBox'a ekle
                foreach (string line in lines)
                {
                    lstBox.Items.Add(line);
                }
            }
        }
        private void UserControlInformationPlaning_Load(object sender, EventArgs e)
        {

        }
    }
}
