using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalenderForProject
{
    public partial class Form1 : Form
    {
        
        public Form1()
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
              }
            

           
        }





    }
}
