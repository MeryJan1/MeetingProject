using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;



namespace CalenderForProject
{
    public partial class FormLogin : Form
    {
        
        public FormLogin()
        {
            InitializeComponent();
        }

        private void txtDate_TextChanged(object sender, EventArgs e)
        {
            // txtDate.Text = DateTime.Now.ToLongDateString();
           

        }
        

        private async void BtnLogin_ClickAsync(object sender, EventArgs e)
        {
              DateTime accessTime = DateTime.Now; // Şu anki tarih ve saat
              string accessTimeString = accessTime.ToString("dd.MM.yyyy HH:mm:ss");
              
              
              if (string.IsNullOrEmpty(txtName.Text))
              {
                  MessageBox.Show("Please don't forget to write your name and surname.");
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
