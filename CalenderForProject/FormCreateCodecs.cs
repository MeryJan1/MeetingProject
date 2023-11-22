using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using static CalenderForProject.Form1;


namespace CalenderForProject
{
    public partial class FormCreateCodecs : Form

    {
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
            Console.WriteLine(string.Format("Client Başlatıldı. Port: {0}", port));
            Console.WriteLine("-----------------------------");

            ExampleSocket exampleSocket = new ExampleSocket(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));
            exampleSocket.Start();

            // File PATH
            string folderPath = $"{userProfilePath}\\Documents\\create";
            string zipFilePath = $"{userProfilePath}\\Documents\\Create.zip";
            if (File.Exists(zipFilePath))
            {
                File.Delete(zipFilePath);
            }
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
                {
                    // Use BinaryFormatter to serialize the ExampleDTO object
                    new BinaryFormatter().Serialize(ms, exampleDTO);

                    // Get the serialized data as a byte array
                    byte[] serializedData = ms.ToArray();

                    // Send the length of the serialized data first
                    byte[] lengthBytes = BitConverter.GetBytes(serializedData.Length);
                    _Socket.Send(lengthBytes);

                    // Send the serialized data in chunks
                    int chunkSize = 1024;
                    int offset = 0;

                    while (offset < serializedData.Length)
                    {
                        int remainingBytes = Math.Min(chunkSize, serializedData.Length - offset);

                        // Use ArraySegment to get a portion of the byte array
                        ArraySegment<byte> segment = new ArraySegment<byte>(serializedData, offset, remainingBytes);

                        // Send the segment of data
                        _Socket.Send(segment.Array, segment.Offset, segment.Count, SocketFlags.None);

                        // Move the offset to the next portion of data
                        offset += remainingBytes;
                    }
                }


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
            MessageBox.Show("Metin panoya kopyalandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            clienter();
            FormCalendar formCalendar = new FormCalendar();
            formCalendar.Show();
            this.Hide();
        }

        
    }
}
