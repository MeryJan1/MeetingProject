using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CalenderForProject.FormCalenderJoinedWithCode;
using static CalenderForProject.FormJoinwithCode;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CalenderForProject
{
    public partial class UserControlDaysJoinWithCode : UserControl
    {
        
        public UserControlDaysJoinWithCode()
        {
            InitializeComponent();
        }

        public void ChangeBackgroundColor(Color newColor)
        {
            this.BackColor = newColor;
        }

        public void days(int numdays)
        {
            lbdays.Text = numdays + "";
            string tarih = numdays+"."+ FormCalenderJoinedWithCode.Static_Month + "." + FormCalenderJoinedWithCode.Static_Year;
            string file = $"C:\\Users\\lenovo\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\Dates\\TümTarihler.txt";
            string path = $"C:\\Users\\lenovo\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\Dates\\{tarih}.txt";

            string[] tarihler = File.ReadAllLines(file) ;

            if (tarihler.Contains(tarih))
            {
                ChangeBackgroundColor(Color.LightGreen);
                lstBox.BackColor = Color.LightGreen;
                string[] lines = File.ReadAllLines(path);
                lstBox.Items.Clear();
                // Her bir satırı ListBox'a ekle
                foreach (string line in lines)
                {
                    lstBox.Items.Add(line);
                }
            }

          

        }

        private void UserControlDaysJoinWithCode_Click(object sender, EventArgs e)
        {
            FormCalenderJoinedWithCode.Static_Day = lbdays.Text;

            string date = FormCalenderJoinedWithCode.Static_Day + "." + FormCalenderJoinedWithCode.Static_Month + "." + FormCalenderJoinedWithCode.Static_Year;
            if (!Tarihlistesi.Contains(date))
            {
                ChangeBackgroundColor(Color.Orange);
                lstBox.BackColor = Color.Orange;
                Tarihlistesi.Add(date);
            }
            else
            {
                ChangeBackgroundColor(Color.LightGreen);
                lstBox.BackColor = Color.LightGreen;
                Tarihlistesi.Remove(date);
            }



        }


        private void lstBox_Click(object sender, EventArgs e)
        {
            FormCalenderJoinedWithCode.Static_Day = lbdays.Text;
            string date = FormCalenderJoinedWithCode.Static_Day + "." + FormCalenderJoinedWithCode.Static_Month + "." + FormCalenderJoinedWithCode.Static_Year;

            if (!Tarihlistesi.Contains(date))
            {
                
                ChangeBackgroundColor(Color.Orange);
                lstBox.BackColor = Color.Orange;
                Tarihlistesi.Add(date);
            }
            else
            {
               
                ChangeBackgroundColor(Color.LightGreen);
                lstBox.BackColor = Color.LightGreen;
                Tarihlistesi.Remove(date);
            }

        }

        private void UserControlDaysJoinWithCode_Load(object sender, EventArgs e)
        {

        }
    }
}
