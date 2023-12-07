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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static CalenderForProject.Form1;

namespace CalenderForProject
{
    public partial class FormTitle : Form
    {
        public static string rastgeleKod;
        public static string TitleMeet;

        public FormTitle()
        {
            InitializeComponent();
        }

        private void buttonSaveMeet_Click(object sender, EventArgs e)
        {
            TitleMeet = txtBoxTitleMeet.Text;
            string modifiedText = TitleMeet.Replace(" ", "_");
            TitleMeet = modifiedText;

            string DescriptionMeet = richBoxDescribtion.Text;
            //kod oluştu
            int kodUzunlugu = 10;
            rastgeleKod = GenerateRandomCode(kodUzunlugu);
            //Başlık isminde bir dosya oluştu            
            string targetDirectory = $"{userProfilePath}\\Documents\\create\\{userNameSurname}\\{TitleMeet}";
            Directory.CreateDirectory(targetDirectory);

            //Description.txt oluştu ve içine DescriptionMeet yazıldı.
            string baslikFilePath = $"{targetDirectory}\\Description.txt";
            File.WriteAllText(baslikFilePath, DescriptionMeet);

            //GirişYapanlar.txt oluşturulsun
            string GirişYapanlar= $"{targetDirectory}\\GirişYapanlar.txt";
            File.Create(GirişYapanlar).Close();

            //Dates clası oluşacak
            string target = $"{targetDirectory}\\Dates" ;
            Directory.CreateDirectory(target);

            //Tümtarihler.txt dosyası oluşacak
            string Tümtarihler = $"{target}\\TümTarihler.txt";
            File.Create(Tümtarihler).Close();
            //Tarihleri Tümtarihler.txt dosyasına yazacak
            
            // StreamWriter kullanarak dosyaya yazma işlemi yapılıyor

            using (StreamWriter writer = new StreamWriter(Tümtarihler))
            {
                foreach (string tarih in TarihListesi)
                {
                    writer.WriteLine(tarih);
                }
            }

            string path = $"{userProfilePath}\\Documents\\create\\{userNameSurname}\\başlık.txt";

            if (File.Exists(path))
            {
                string existingContent = File.ReadAllText(path);
                
                // Mevcut içeriği ve TitleMeet değerini kullanarak yeni içeriği oluştur
                string newContent = existingContent + Environment.NewLine + TitleMeet;

                // Dosyayı güncelle
                File.WriteAllText(path, newContent);
            }

            CreateTxtFiles(TarihListesi);

            ////////////////////////////////////
            FormCreateCodecs formCreateCodecs = new FormCreateCodecs();  
            formCreateCodecs.Show();

            this.Close();
        }


        static void CreateTxtFiles(List<string> fileNames)
        {
            foreach (string fileName in fileNames)
            {
                string directoryPath = $"{userProfilePath}\\Documents\\create\\{userNameSurname}\\{TitleMeet}\\Dates";
                Directory.CreateDirectory(directoryPath);
                string filePath = Path.Combine(directoryPath, $"{fileName}.txt");
                File.Create(filePath).Close();
                
            }
        }

        static string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder randomCode = new StringBuilder();
            
           
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                randomCode.Append(chars[index]);
            }
            string filePath = $"{userProfilePath}\\Documents\\create\\code.txt";
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine(randomCode);
            }



            string Path = $"{userProfilePath}\\Documents\\create\\Dictionary\\KullanıcıAdı.txt";
            using (StreamWriter writer = File.AppendText(Path))
            {
                writer.WriteLine(randomCode+"*"+userNameSurname);
            }


            string path = $"{userProfilePath}\\Documents\\create\\Dictionary\\başlık.txt";
            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine(randomCode + "*" + TitleMeet);
            }
            return randomCode.ToString();
        }

    }
}
