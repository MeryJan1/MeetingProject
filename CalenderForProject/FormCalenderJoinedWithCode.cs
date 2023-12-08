using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CalenderForProject.Form1;
using static CalenderForProject.FormJoinwithCode;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using static Google.Apis.Drive.v3.DriveService;


namespace CalenderForProject
{
    public partial class FormCalenderJoinedWithCode : Form
    {
        public static List<string> Tarihlistesi = new List<string> { };
        public static string Static_Day, Static_Month, Static_Year;
        int year, month; // for calendar

        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = $"{KullanıcıAdı}";
        static string FilePath = $"{userProfilePath}\\Documents\\create"; // Yüklemek istediğiniz dosyanın yolu


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
            string filePath = $"{userProfilePath}\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\GirişYapanlar.txt";//GİRİŞ YAPMIŞ KİŞİLER LİSTELENECEK 
            string file = $"{userProfilePath}\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\Description.txt";
            txtBoxTitle.Text = FormCalendar.title;
            if (System.IO.File.Exists(file))
            {
                // Dosyadan içeriği oku
                string content = System.IO.File.ReadAllText(file);

                // RichTextBox'a yaz description burada gözükecek
                richBoxDescription.Text = content;
            }
           
            // Dosya var mı kontrolü
            if (System.IO.File.Exists(filePath))
            {
                // Dosyadan satırları oku ve ListBox'a ekle toplantı günlerini seçenler burada gözükecek.
               ;
                string[] lines = System.IO.File.ReadAllLines(filePath);
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
            /*//DRİVE İŞLEMLERİ*****************
            UserCredential credential = GetCredential();

            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            string fileId = UploadFile(service, FilePath);

            // Dosyayı paylaşmak için Google Drive API'yi kullanın
            ShareFile(service, fileId);

            //*************************************
            */
            //client();
            // tarihlistesinde bulunan stringler TümTarihler.txt klasında da bulunuyorsa o stringdeğişkeni.txt dosyasına gidip KullanıcıAdı stringini yazdıran kod.

            string tümTarihler = $"{userProfilePath}\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\Dates\\TümTarihler.txt";
            string tümKullanıcılarDosyaYolu = $"{userProfilePath}\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\GirişYapanlar.txt";


            // Tüm tarihleri oku

            if (System.IO.File.Exists(tümTarihler) && System.IO.File.Exists(tümKullanıcılarDosyaYolu))
            {
                
                foreach (string tarih in System.IO.File.ReadAllLines(tümTarihler))
                {
                    // TarihListesi içindeki tarihleri kontrol et
                    if (Tarihlistesi.Contains(tarih))
                    {
                        string tarihDosyaYolu = $"{userProfilePath}\\Documents\\create\\{KullanıcıAdı}\\{Başlık}\\Dates\\{tarih}.txt";

                        // Dosya varsa ve daha önce bu kullanıcı eklenmemişse
                        if (System.IO.File.Exists(tarihDosyaYolu) && !System.IO.File.ReadAllText(tarihDosyaYolu).Contains(İsim))
                        {
                            // Tarih dosyasına ekle
                            using (StreamWriter sw = System.IO.File.AppendText(tarihDosyaYolu))
                            {
                                sw.WriteLine(İsim);
                            }
                        }
                        if (System.IO.File.Exists(tümKullanıcılarDosyaYolu) && !System.IO.File.ReadAllText(tümKullanıcılarDosyaYolu).Contains(İsim))
                        {
                            // Tüm kullanıcılar dosyasına da ekle
                            using (StreamWriter sw = System.IO.File.AppendText(tümKullanıcılarDosyaYolu))
                            {
                                sw.WriteLine(İsim);
                            }
                        }

                    }
                    MessageBox.Show("Your information saved!");
                    this.Close();
                    FormCalenderJoinedWithCode formCalenderJoinedWithCode = new FormCalenderJoinedWithCode();
                    formCalenderJoinedWithCode.Show();
                }

                

            }
            else
            {
                MessageBox.Show("This path didn't found.");
            }



        }
        // ///////////////DRİVE İLE DOSYA GÖNDERME//////////////////////////

        static UserCredential GetCredential()
        {
            using (var stream = new FileStream($"{userProfilePath}\\Documents\\create\\credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart-", FormLogin.userNameSurname, ".json");

                return GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
        }

        static string UploadFile(DriveService service, string filePath)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(filePath),
            };

            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                request = service.Files.Create(fileMetadata, stream, "application/octet-stream");
                request.Upload();
            }

            var file = request.ResponseBody;

            Console.WriteLine($"File ID: {file.Id}");
            return file.Id;
        }

        static void ShareFile(DriveService service, string fileId)
        {
            var permission = new Permission()
            {
                Type = "anyone",
                Role = "reader",
            };

            service.Permissions.Create(permission, fileId).Execute();

            Console.WriteLine("File shared!");
        }
        //////////////////////////////////////////////////////////////////////////////


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

            ExampleSocket exampleSocket = new ExampleSocket(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));
            exampleSocket.Start();

            // File PATH
            string folderPath = $"{userProfilePath}\\Documents\\create";
            string zipFilePath = $"{userProfilePath}\\Documents\\create.zip";

            // Klasörü zip dosyası olarak sıkıştır
            ZipFile.CreateFromDirectory(folderPath, zipFilePath);

            ExampleDTO exampleDTO = new ExampleDTO()
            {
                Message = string.Format("{0} ip numaralı client üzerinden geliyorum!", GetLocalIPAddress()),
                FileName = Path.GetFileName(zipFilePath),
                FileData = System.IO.File.ReadAllBytes(zipFilePath)
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

                    _Socket.Send(serializedData, serializedData.Length, SocketFlags.None);

                    // Send the length of the serialized data first
                    // byte[] lengthBytes = BitConverter.GetBytes(serializedData.Length);

                    /*     _Socket.Send(lengthBytes);

                         // Send the serialized data in chunks
                         int chunkSize = 1024;
                         for (int i = 0; i < serializedData.Length; i += chunkSize)
                         {
                             int remainingBytes = Math.Min(chunkSize, serializedData.Length - i);
                             _Socket.Send(serializedData, i, remainingBytes, SocketFlags.None);
                         }
                    */

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
                    MessageBox.Show("Servera bağlanılamıyor!");
                }
            }

            void OnBeginReceive(IAsyncResult asyncResult)
            {
                // Almayı bitiriyoruz ve geriye gelen byte array'in boyutunu vermektedir.
                int receivedDataLength = _Socket.EndReceive(asyncResult, out socketError);

                if (receivedDataLength <= 0 || socketError != SocketError.Success)
                {
                    // Gelen byte array verisi boş ise bağlantı kopmuş demektir. Burayı istediğiniz gibi handle edebilirsiniz.
                    MessageBox.Show("Server bağlantısı koptu!");
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
