using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CalenderForProject
{
    public partial class FormJoinwithCode : Form
    {
        public FormJoinwithCode()
        {
            InitializeComponent();
        }
        public static string KullanıcıAdı, Başlık;
        

        private async void BtnLogin_ClickAsync(object sender, EventArgs e)
        {
            string enteredCode = txtBoxCode.Text;
            Dictionary<string, string> DicName = new Dictionary<string, string>();
            Dictionary<string, string> DicTitle = new Dictionary<string, string>();
            // Dosya yolu
            string filePath = $"C:\\Users\\lenovo\\Documents\\create\\Dictionary\\KullanıcıAdı.txt";
            // Dosyadan okuma işlemi
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    // Satırı yıldıza göre ayır ve key, value olarak kullan
                    string[] parts = line.Split('*');
                    if (parts.Length == 2)
                    {
                        string key = parts[0];
                        string value = parts[1];

                        // Dictionary'e ekle
                        DicName[key] = value;
                    }
                }
            }


            // Dosya yolu
            string path = $"C:\\Users\\lenovo\\Documents\\create\\Dictionary\\başlık.txt";
            // Dosyadan okuma işlemi
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    // Satırı yıldıza göre ayır ve key, value olarak kullan
                    string[] parts = line.Split('*');
                    if (parts.Length == 2)
                    {
                        string key = parts[0];
                        string value = parts[1];

                        // Dictionary'e ekle
                        DicTitle[key] = value;
                    }
                }
            }

            Başlık = DicTitle[enteredCode];
            KullanıcıAdı = DicName[enteredCode];



            DateTime accessTime = DateTime.Now; // Şu anki tarih ve saat
            string accessTimeString = accessTime.ToString("dd.MM.yyyy HH:mm:ss");

            string[] fileLines = File.ReadAllLines("C:\\Users\\lenovo\\Documents\\create\\code.txt");
            

            if (string.IsNullOrEmpty(txtName.Text)&&string.IsNullOrEmpty(txtBoxCode.Text))
            {
                MessageBox.Show("Please make sure you enter all information correctly and completely.");
            }
            else
            {
                string userNameSurname = txtName.Text;
                
                bool isCodeFound = false;
                foreach (string line in fileLines)
                {
                

                    // Kontrol et
                    if (line.Contains(enteredCode))
                    {
                        isCodeFound = true;
                        break; // Eğer bulduysa döngüden çık
                    }
                }

                if (isCodeFound)
                {
                    string loginMessage = $"Welcome {userNameSurname}! Login Date {accessTimeString}\n Select the days by clicking on the days. Then press OK to confirm.";
                    FormCalenderJoinedWithCode formCalenderJoinedWithCode = new FormCalenderJoinedWithCode();
                    formCalenderJoinedWithCode.Show();
                    MessageBox.Show(loginMessage);
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid code. Please check the entered code and try again.");
                }
                
            }


        }

        
    }
}
