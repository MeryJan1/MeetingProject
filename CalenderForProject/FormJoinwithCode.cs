using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalenderForProject
{
    public partial class FormJoinwithCode : Form
    {
        public FormJoinwithCode()
        {
            InitializeComponent();
        }

        private async void BtnLogin_ClickAsync(object sender, EventArgs e)
        {
            DateTime accessTime = DateTime.Now; // Şu anki tarih ve saat
            string accessTimeString = accessTime.ToString("dd.MM.yyyy HH:mm:ss");


            if (string.IsNullOrEmpty(txtName.Text)&&string.IsNullOrEmpty(txtBoxCode.Text)/*DOSYADAN ÇEKTİĞİN CODUN İÇERİP İÇERİLMEDİĞİNİ BULMAN GEREKİR.*/)
            {
                MessageBox.Show("Please make sure you enter all information correctly and completely.");
            }
            else
            {
                // İsim ve soyisim girişi yapıldığında bu kısım çalışır.
                string userNameSurname = txtName.Text;

                string LoginMassage = $"Welcome {userNameSurname}! Login Date{accessTimeString} \n Select the days by clicking on the days. Then press OK to confirm.";
                FormCalendar formCalendar = new FormCalendar();
                formCalendar.Show();
                MessageBox.Show(LoginMassage);
                this.Hide();
            }



        }

        
    }
}
