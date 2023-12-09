﻿using System;
using System.IO;
using System.Windows.Forms;


namespace CalenderForProject
{
    public partial class Form1 : Form //server form
    {
        public static string userProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        public Form1()
        {
            InitializeComponent();
            Form();
        }


        private void btnJoin_Click(object sender, EventArgs e)
        {
            FormJoinwithCode joinwithCode = new FormJoinwithCode();
            joinwithCode.Show();
            this.Hide();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            this.Hide();
        }

        private void Form()
        {
            string directoryPath = $"{userProfilePath}\\Documents\\create";

            // "create" dizini yoksa oluşturun
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                // "isim.txt" dosyasını oluşturun
                string isimFilePath = Path.Combine(directoryPath, "isim.txt");
                File.WriteAllText(isimFilePath, string.Empty);


                // "code.txt" dosyasını oluşturun
                string codeFilePath = Path.Combine(directoryPath, "code.txt");
                File.WriteAllText(codeFilePath, string.Empty);


                // "Dictionary" adlı bir dizin oluşturun
                string dictionaryPath = Path.Combine(directoryPath, "Dictionary");
                Directory.CreateDirectory(dictionaryPath);


                // "KullanıcıAdı.txt" dosyasını oluşturun
                string kullaniciAdiFilePath = Path.Combine(dictionaryPath, "KullanıcıAdı.txt");
                File.WriteAllText(kullaniciAdiFilePath, string.Empty);


                // "Başlık.txt" dosyasını oluşturun
                string baslikFilePath = Path.Combine(dictionaryPath, "Başlık.txt");
                File.WriteAllText(baslikFilePath, string.Empty);

            }
            

            
        }

    }
}
