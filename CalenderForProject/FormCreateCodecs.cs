using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using static CalenderForProject.Form1;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Util.Store;
using System.Threading;

namespace CalenderForProject
{
    public partial class FormCreateCodecs : Form

    {
        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = $"{FormLogin.userNameSurname}";

        [Serializable]
        public class ExampleDTO
        {
            public byte[] FileData { get; set; }
            public string FileName { get; set; }
            public string Message { get; set; }
        }

        public FormCreateCodecs()
        {
            InitializeComponent();

        }


        //********************************CLİENT********

        public void clienter()
        {
            int port = 5555;

            ExampleSocket exampleSocket = new ExampleSocket(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));
            exampleSocket.Start();

            // File PATH
            string folderPath = $"{userProfilePath}\\Documents\\create";
            /*string zipFilePath = $"{userProfilePath}\\Documents\\Create.zip";
            if (File.Exists(zipFilePath))
            {
                File.Delete(zipFilePath);
            }
            // Klasörü zip dosyası olarak sıkıştır
            ZipFile.CreateFromDirectory(folderPath, zipFilePath);
            */
            ExampleDTO exampleDTO = new ExampleDTO()
            {
                Message = string.Format("{0} ip numaralı client üzerinden geliyorum!", GetLocalIPAddress()),
                FileName = Path.GetFileName(folderPath),
                FileData = System.IO.File.ReadAllBytes(folderPath)
            };

            exampleSocket.SendData(exampleDTO);

            Console.ReadLine();
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
                using (var ms = new MemoryStream())
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
                }
                /*   using (MemoryStream ms = new MemoryStream())
                 using (BinaryWriter writer = new BinaryWriter(ms))
                 {
                     // Serialize the ExampleDTO object
                     new BinaryFormatter().Serialize(ms, exampleDTO);

                     // Get the byte array from MemoryStream
                     byte[] serializedData = ms.ToArray();

                     _Socket.Send(serializedData, serializedData.Length, SocketFlags.None);

                     // Send the length of the serialized data first
                     // byte[] lengthBytes = BitConverter.GetBytes(serializedData.Length);

                        _Socket.Send(lengthBytes);

                          // Send the serialized data in chunks
                          int chunkSize = 1024;
                          for (int i = 0; i < serializedData.Length; i += chunkSize)
                          {
                              int remainingBytes = Math.Min(chunkSize, serializedData.Length - i);
                              _Socket.Send(serializedData, i, remainingBytes, SocketFlags.None);
                          }
                     

            }
*/
                ////bu kısım örnek için kalmıştı kodun parçası değil
                /*   using (MemoryStream ms = new MemoryStream())
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
                   }*/
                ////bu kısım örnek için kalmıştı kodun parçası değil
                /*   using (var ms = new MemoryStream())
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
        
        static string GetLocalIPAddress()
        {
            string localIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault().ToString();

            return localIP;
        }

        //*******************************************************

        private void FormCreateCodecs_Load(object sender, EventArgs e)
        {

            tBoxCode.Text = FormTitle.rastgeleKod;
        }
       

        private void btnCopy_Click(object sender, EventArgs e)
        {
            
            Clipboard.SetText(tBoxCode.Text);
            MessageBox.Show("The text has been copied to the clipboard.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // clienter();
           // WhichFile();





            /*
            UserCredential credential = GetCredential();

            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            string fileId = UploadFile(service, FilePath);

            // Dosyayı paylaşmak için Google Drive API'yi kullanın
            ShareFile(service, fileId);

            */
            ////********************************
            FormCalendar formCalendar = new FormCalendar();
            formCalendar.Show();
            this.Hide();
        }

        ///////////////DRİVE İLE DOSYA GÖNDERME//////////////////////////

        public static string newFolderId;
        public static string MainFolderId = "1Bnb3oqI6vvnEMa8clK0WMtn_hHcPP9DL";
        static void WhichFile()
        {

            string folderPath = $"{userProfilePath}\\Documents\\create\\isim.txt";
            string path = $"{userProfilePath}\\Documents\\create\\code.txt";

            

            UserCredential credential = GetCredential();

            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            UploadFile(service, folderPath, MainFolderId);
            UploadFile(service, path, MainFolderId);

            string FolderName = "Dictionary";
            string KlasörID;
            FileExist(FolderName);
            string fPath = $"{userProfilePath}\\Documents\\create\\Dictionary\\KullanıcıAdı.txt";
            UploadFile(service, fPath, newFolderId);

            fPath = $"{userProfilePath}\\Documents\\create\\Dictionary\\Başlık.txt";
            UploadFile(service, fPath, newFolderId);

        }

       static string CreateFolder(string parentFolderId, string folderName)
        {
            var folderMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder",
                Parents = new[] { parentFolderId },
            };
            UserCredential credential = GetCredential();

            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var request = service.Files.Create(folderMetadata);
            var createdFolder = request.Execute();

            return createdFolder.Id;
        }

        static void FileExist(string fileName)
        {

            UserCredential credential = GetCredential();
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            try
            {
                // Dosya adını içeren dosyaları listele
                var fileList = service.Files.List().Execute().Files;
                var matchingFiles = fileList.Where(file => file.Name == fileName).ToList();

                if (matchingFiles.Any())
                {
                    Console.WriteLine($"File with name '{fileName}' exists.");
                }
                else
                {

                    newFolderId = CreateFolder(MainFolderId, fileName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking file existence: {ex.Message}");
            }
        }

        static void UploadFile(DriveService service, string filePath, string ID)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(filePath),
                Parents = new[] { ID },
            };

            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                request = service.Files.Create(fileMetadata, stream, "application/octet-stream");
                request.Upload();
            }

        }

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
        /* 

        static UserCredential GetCredential()
        {
            using (var stream = new FileStream( $"{userProfilePath}\\Documents\\create\\credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart-",FormLogin.userNameSurname,".json");

                return GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore (credPath, true)).Result;
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
/////////////////////////////////////////////////////////////////////////////*/

    }
}
