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
using static CalenderForProject.FormCalenderInformationPlaning;
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

        public void days(int numdays)
        {
            lbdays.Text = numdays + "";

            string tarih = numdays+"."+FormCalenderJoinedWithCode.Static_Month+"."+FormCalenderJoinedWithCode.Static_Year;
            string file = $"C:\\Users\\lenovo\\Documents\\create\\{userNameSurname}\\{title}\\Dates\\TümTarihler.txt";;
            string path = $"C:\\Users\\lenovo\\Documents\\create\\{userNameSurname}\\{title}\\Dates\\{tarih}.txt";


            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (tarih == line)
                        {
                            // FileStream ile dosya yazma ve okuma
                            using (FileStream fsPath = new FileStream(path, FileMode.Open, FileAccess.Read))
                            {
                                using (StreamReader readerPath = new StreamReader(fsPath, Encoding.UTF8))
                                {
                                    rBox.Text = readerPath.ReadToEnd();
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            //Burada label üzerlerine kullanıcılar yazdırılacak.
            //eğer TümTarihler.txt dosyasının içinde bu tarih varsa o tarihin dosyasına git aynı dizinde olmalı ve
            //içindeki kullanıcıları tek tek labela yazdır.

        }

        private void UserControlInformationPlaning_Load(object sender, EventArgs e)
        {

        }
    }
}
