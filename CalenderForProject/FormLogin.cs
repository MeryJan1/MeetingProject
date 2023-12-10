using System;
using System.Windows.Forms;
using System.IO;
using static CalenderForProject.Form1;



namespace CalenderForProject
{
    public partial class FormLogin : Form
    {
        public static string userNameSurname;

        public FormLogin()
        {
            InitializeComponent();
        }


        private async void BtnLogin_ClickAsync(object sender, EventArgs e)
        {
            DateTime accessTime = DateTime.Now; // Şu anki tarih ve saat
            string accessTimeString = accessTime.ToString("dd.MM.yyyy HH:mm:ss");

            userNameSurname = txtName.Text;
            string modifiedText = userNameSurname.Replace(" ", "_");
            userNameSurname = modifiedText;

            string filePath = $"{userProfilePath}\\create\\isim.txt";
            if (File.Exists(filePath))
            {
                // Dosya içeriğini okuyun
                string fileContent = File.ReadAllText(filePath);

                // Dosya içeriğinde kullanıcı adınızı ve soyadınızı arayın
                if (!fileContent.Contains(userNameSurname))
                {
                    // create/userNameSurname dizinini oluşturun
                    string directoryPath = $"{userProfilePath}\\create\\{userNameSurname}";
                    Directory.CreateDirectory(directoryPath);

                    if (Directory.Exists(directoryPath)) 
                    {
                        string newFilePath = $"{userProfilePath}\\create\\{userNameSurname}\\başlık.txt";
                        using (FileStream fs = File.Create(newFilePath))
                        {
                            // Dosya işlemleri burada yapılabilir
                        }
                    }
                   

                    using (StreamWriter writer = File.AppendText(filePath))
                    {
                        writer.WriteLine(userNameSurname);
                    }
                }
                
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please do not forget to enter your name and surname!");
            }
            else
            {
                // İsim ve soyisim girişi yapıldığında bu kısım çalışır.


                string LoginMassage = $"Welcome {userNameSurname}! Login Date : {accessTimeString} \n Select the days by clicking on the days. Then press OK to confirm.";
                FormCalendar formCalendar = new FormCalendar();
                formCalendar.Show();
                MessageBox.Show(LoginMassage);
                this.Close();
            }


        }
        
    }
}
