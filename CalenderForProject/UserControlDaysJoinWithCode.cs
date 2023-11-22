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
        public static string Date;

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
                ChangeBackgroundColor(Color.PeachPuff);
                string[] lines = File.ReadAllLines(path);
                lstBox.Items.Clear();
                // Her bir satırı ListBox'a ekle
                foreach (string line in lines)
                {
                    lstBox.Items.Add(line);
                }
            }

          
                //Burada label üzerlerine kullanıcılar yazdırılacak.
                //eğer TümTarihler.txt dosyasının içinde bu tarih varsa o tarihin dosyasına git aynı dizinde olmalı ve
                //içindeki kullanıcıları tek tek labela yazdır.

        }

        private void UserControlDaysJoinWithCode_Click(object sender, EventArgs e)
        {
            FormCalenderJoinedWithCode.Static_Day = lbdays.Text;
            // Tıklanan tarihi bir olay ile ana forma iletiyoruz
            string date = FormCalenderJoinedWithCode.Static_Day + "." + FormCalenderJoinedWithCode.Static_Month + "." + FormCalenderJoinedWithCode.Static_Year;
            if (!Tarihlistesi.Contains(date))
            {
                Tarihlistesi.Add(date);
                label1.Text = "Selected";
            }
            else
            {
                Tarihlistesi.Remove(date);
                label1.Text = "";
            }



        }

        private void lbdays_Click(object sender, EventArgs e)
        {
            FormCalenderJoinedWithCode.Static_Day = lbdays.Text;
            // Tıklanan tarihi bir olay ile ana forma iletiyoruz
            string date = FormCalenderJoinedWithCode.Static_Day + "." + FormCalenderJoinedWithCode.Static_Month + "." + FormCalenderJoinedWithCode.Static_Year;
            if (!Tarihlistesi.Contains(date))
            {
                Tarihlistesi.Add(date);
                label1.Text = "Selected";
            }
            else
            {
                Tarihlistesi.Remove(date);
                label1.Text = "";
            }
        }

        private void UserControlDaysJoinWithCode_Load(object sender, EventArgs e)
        {

        }
    }
}
