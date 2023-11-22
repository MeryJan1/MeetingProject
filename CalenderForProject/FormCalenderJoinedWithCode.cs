using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static CalenderForProject.FormCalenderJoinedWithCode;
using static CalenderForProject.FormJoinwithCode;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace CalenderForProject
{
    public partial class FormCalenderJoinedWithCode : Form
    {
        public static List<string> Tarihlistesi = new List<string> { };
        public static string Static_Day, Static_Month, Static_Year;
        int year, month; // for calendar

        public FormCalenderJoinedWithCode()
        {
            InitializeComponent();
        }

        private void FormCalenderJoinedWithCode_Load(object sender, EventArgs e)
        {
            loadDays();
            loadBox();
        }

        private void loadBox()
        {
            string filePath = $"C:\\Users\\lenovo\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\GirişYapanlar.txt";//GİRİŞ YAPMIŞ KİŞİLER LİSTELENECEK 
            string file = $"C:\\Users\\lenovo\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\Description.txt";
            txtBoxTitle.Text = FormCalendar.title;
            if (File.Exists(file))
            {
                // Dosyadan içeriği oku
                string content = File.ReadAllText(file);

                // RichTextBox'a yaz description burada gözükecek
                richBoxDescription.Text = content;
            }
           
            // Dosya var mı kontrolü
            if (File.Exists(filePath))
            {
                // Dosyadan satırları oku ve ListBox'a ekle toplantı günlerini seçenler burada gözükecek.
                string[] lines = File.ReadAllLines(filePath);
                lstBoxPlans.Items.AddRange(lines);
            }
            txtBoxTitle.Text = Başlık;

        }

        private void loadDays()
        {

            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            LBDATE.Text = monthname + " " + year;

            Static_Year = year.ToString();
            Static_Month = month.ToString();
            // Get first day of the month
            DateTime startofthemonth = new DateTime(year, month, 1);

            // get the count of dates of the month
            int days = DateTime.DaysInMonth(year, month);

            //convert the start of the month to integer
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d"));

            //create a blank user control
            for (int i = 1; i <= dayoftheweek; i++)
            {
                ucBlank ucBlank = new ucBlank();
                daycontainer.Controls.Add(ucBlank);
            }

            // user control for days
            for (int i = 1; i <= days; i++)
            {
               UserControlDaysJoinWithCode userControlDaysJoinWithCode = new UserControlDaysJoinWithCode();
                userControlDaysJoinWithCode.days(i);
                daycontainer.Controls.Add(userControlDaysJoinWithCode);

            }

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {

            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            month--;
            // clear container
            daycontainer.Controls.Clear();

            if (monthname == "January")
            {
                year--;
                month = 12;
            }

            Static_Year = year.ToString();
            Static_Month = month.ToString();

            monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            LBDATE.Text = monthname + " " + year;

            DateTime startofthemonth = new DateTime(year, month, 1);

            // get the count of dats of the month
            int days = DateTime.DaysInMonth(year, month);

            //convert the startofthe month to integer
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            //create a blank user control
            for (int i = 1; i < dayoftheweek; i++)
            {
                ucBlank ucBlank = new ucBlank();
                daycontainer.Controls.Add(ucBlank);
            }

            // user control for days
            for (int i = 1; i <= days; i++)
            {
                UserControlDaysJoinWithCode userControlDaysJoinWithCode = new UserControlDaysJoinWithCode();
                userControlDaysJoinWithCode.days(i);
                daycontainer.Controls.Add(userControlDaysJoinWithCode);

            }

        }

        private void buttonOkey_Click(object sender, EventArgs e)
        {
            client();
            // tarihlistesinde bulunan stringler TümTarihler.txt klasında da bulunuyorsa o stringdeğişkeni.txt dosyasına gidip KullanıcıAdı stringini yazdıran kod.
            
            string tümTarihler = $"C:\\Users\\lenovo\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\Dates\\TümTarihler.txt";
            string tümKullanıcılarDosyaYolu = $"C:\\Users\\lenovo\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\GirişYapanlar.txt";

            // Tüm tarihleri oku
            string[] tarihler = File.ReadAllLines(tümTarihler);

            foreach (string tarih in tarihler)
            {
                // TarihListesi içindeki tarihleri kontrol et
                if (Tarihlistesi.Contains(tarih))
                {
                    string tarihDosyaYolu = $"C:\\Users\\lenovo\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\Dates\\{tarih}.txt";

                    // Dosya varsa ve daha önce bu kullanıcı eklenmemişse
                    if (File.Exists(tarihDosyaYolu) && !File.ReadAllText(tarihDosyaYolu).Contains(İsim))
                    {
                        // Tarih dosyasına ekle
                        using (StreamWriter sw = File.AppendText(tarihDosyaYolu))
                        {
                            sw.WriteLine(İsim);
                        }
                    }
                    if(File.Exists(tümKullanıcılarDosyaYolu) && !File.ReadAllText(tümKullanıcılarDosyaYolu).Contains(İsim))
                    {
                        // Tüm kullanıcılar dosyasına da ekle
                        using (StreamWriter sw = File.AppendText(tümKullanıcılarDosyaYolu))
                        {
                            sw.WriteLine(İsim);
                        }

                    }

                }
            }

            MessageBox.Show("Your information saved!");
            this.Close();
            FormCalenderJoinedWithCode formCalenderJoinedWithCode = new FormCalenderJoinedWithCode();
            formCalenderJoinedWithCode.Show();


        }
        //*************Client*************************************************************************************************
        [Serializable]
        public class ExampleDTO
        {
            public byte[] FileData { get; set; }
            public string FileName { get; set; }
            public string Message { get; internal set; }
        }

        private void client()
        {

            int port = 5555;
            Console.WriteLine(string.Format("Client Başlatıldı. Port: {0}", port));
            Console.WriteLine("-----------------------------");

            ExampleSocket exampleSocket = new ExampleSocket(new IPEndPoint(IPAddress.Parse("127 .0.0.1"), port));
            exampleSocket.Start();

            // File PATH
            string folderPath = @"C:\Path\To\Your\Folder";
            string zipFilePath = @"C:\Path\To\Your\Folder.zip";

            // Klasörü zip dosyası olarak sıkıştır
            ZipFile.CreateFromDirectory(folderPath, zipFilePath);

            ExampleDTO exampleDTO = new ExampleDTO()
            {
                Message = string.Format("{0} ip numaralı client üzerinden geliyorum!", GetLocalIPAddress()),
                FileName = Path.GetFileName(zipFilePath),
                FileData = File.ReadAllBytes(zipFilePath)
            };

            exampleSocket.SendData(exampleDTO);

            Console.ReadLine();
        }

        static string GetLocalIPAddress()
        {
            string localIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault().ToString();

            return localIP;
        }

        public class ExampleSocket
        {
            #region Variables
            Socket _Socket;
            IPEndPoint _IPEndPoint;

            // Socket işlemleri sırasında oluşabilecek errorları bu enum ile handle edebiliriz.
            SocketError socketError;
            byte[] tempBuffer = new byte[1024];
            #endregion

            #region Constructor
            public ExampleSocket(IPEndPoint ipEndPoint)
            {
                _IPEndPoint = ipEndPoint;

                // Socket'i tanımlıyoruz IPv4, socket tipimiz stream olacak ve TCP Protokolü ile haberleşeceğiz. 
                // TCP Protokolünde server belirlenen portu dinler ve gelen istekleri karşılar oysaki UDP Protokolünde tek bir socket üzerinden birden çok client'a ulaşmak mümkündür.
                _Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            #endregion

            #region Public Methods
            public void Start()
            {
                // BeginConnect ile asenkron olarak bir bağlantı başlatıyoruz.
                _Socket.BeginConnect(_IPEndPoint, OnBeginConnect, null);
            }

            public void SendData(ExampleDTO exampleDTO)
            {


                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    // Serialize the ExampleDTO object
                    new BinaryFormatter().Serialize(ms, exampleDTO);

                    // Get the byte array from MemoryStream
                    byte[] serializedData = ms.ToArray();

                    // Send the length of the serialized data first
                    byte[] lengthBytes = BitConverter.GetBytes(serializedData.Length);
                    _Socket.Send(lengthBytes);

                    // Send the serialized data in chunks
                    int chunkSize = 1024;
                    for (int i = 0; i < serializedData.Length; i += chunkSize)
                    {
                        int remainingBytes = Math.Min(chunkSize, serializedData.Length - i);
                        _Socket.Send(serializedData, i, remainingBytes, SocketFlags.None);
                    }
                }
                ////bu kısım örnek için kalmıştı kodun parçası değil
                /*  using (var ms = new MemoryStream())
                 {
                     // İlgili object'imizi binary'e serialize ediyoruz.
                     new BinaryFormatter().Serialize(ms, exampleDTO);
                     IList<ArraySegment<byte>> data = new List<ArraySegment<byte>>();

                     data.Add(new ArraySegment<byte>(ms.ToArray()));

                     // Gönderme işlemine başlıyoruz.
                     _Socket.BeginSend(data, SocketFlags.None, out socketError, (asyncResult) =>
                     {
                         // Gönderme işlemini bitiriyoruz.
                         int length = _Socket.EndSend(asyncResult, out socketError);

                         if (length <= 0 || socketError != SocketError.Success)
                         {
                             Console.WriteLine("Server bağlantısı koptu!");
                             return;
                         }
                     }, null);

                     if (socketError != SocketError.Success)
                         Console.WriteLine("Server bağlantısı koptu!");
                 }*/

            }
            #endregion

            #region Private Methods
            void OnBeginConnect(IAsyncResult asyncResult)
            {
                try
                {
                    // Bağlanma işlemini bitiriyoruz.
                    _Socket.EndConnect(asyncResult);

                    // Bağlandığımız socket üzerinden datayı dinlemeye başlıyoruz.
                    _Socket.BeginReceive(tempBuffer, 0, tempBuffer.Length, SocketFlags.None, OnBeginReceive, null);
                }
                catch (SocketException)
                {
                    // Servera bağlanamama durumlarında bize SocketException fırlatıcaktır. Hataları burada handle edebilirsiniz.
                    Console.WriteLine("Servera bağlanılamıyor!");
                }
            }

            void OnBeginReceive(IAsyncResult asyncResult)
            {
                // Almayı bitiriyoruz ve geriye gelen byte array'in boyutunu vermektedir.
                int receivedDataLength = _Socket.EndReceive(asyncResult, out socketError);

                if (receivedDataLength <= 0 || socketError != SocketError.Success)
                {
                    // Gelen byte array verisi boş ise bağlantı kopmuş demektir. Burayı istediğiniz gibi handle edebilirsiniz.
                    Console.WriteLine("Server bağlantısı koptu!");
                    return;
                }

                // Tekrardan socket üzerinden datayı dinlemeye başlıyoruz.
                _Socket.BeginReceive(tempBuffer, 0, tempBuffer.Length, SocketFlags.None, OnBeginReceive, null);
            }
            #endregion
        }

        //**********************************************************************************************************************

        private void btnNext_Click(object sender, EventArgs e)
        {
            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            // clear container
            daycontainer.Controls.Clear();

            if (monthname == "December")
            {
                year++;
                month = 0;
            }

            month++;

            Static_Year = year.ToString();
            Static_Month = month.ToString();

            monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            LBDATE.Text = monthname + " " + year;

            DateTime startofthemonth = new DateTime(year, month, 1);

            // get the count of dats of the month
            int days = DateTime.DaysInMonth(year, month);

            //convert the startofthe month to integer
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            //create a blank user control
            for (int i = 1; i < dayoftheweek; i++)
            {
                ucBlank ucBlank = new ucBlank();
                daycontainer.Controls.Add(ucBlank);
            }

            // user control for days
            for (int i = 1; i <= days; i++)
            {
                UserControlDaysJoinWithCode userControlDaysJoinWithCode = new UserControlDaysJoinWithCode();
                userControlDaysJoinWithCode.days(i);
                daycontainer.Controls.Add(userControlDaysJoinWithCode);

            }

            for (int i = 1; i <= 42 - (days + dayoftheweek); i++)
            {
                ucBlank ucBlank = new ucBlank();
                daycontainer.Controls.Add(ucBlank);
            }
        }




        //**************************************************************
    }


}
