using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using ExampleDataTransferObjects;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using System.Linq;


namespace ExampleDataTransferObjects
{
    /// <summary>
    /// Serialize edebilmek için Serializable attributü ile işaretliyoruz.
    /// </summary>
    [Serializable]
    public class ExampleDTO
    {
        public string Status { get; set; }
        public byte[] FileData { get; set; }
        public string FileName { get; set; }
        public string Message { get; set; }
    }
}

namespace CalenderForProject
{
    internal static class Program
    {
        public delegate void OnExampleDTOReceived(ExampleDTO eDTO);

        public class Client
        {
            public OnExampleDTOReceived _OnExampleDTOReceived;
            Socket _Socket;
            SocketError socketError;
            byte[] tempBuffer = new byte[1024];

            public Client(Socket socket)
            {
                _Socket = socket;
            }

            public void Start()
            {
                _Socket.BeginReceive(tempBuffer, 0, tempBuffer.Length, SocketFlags.None, OnBeginReceiveCallback, null);
            }

            void OnBeginReceiveCallback(IAsyncResult asyncResult)
            {
                int receivedDataLength = _Socket.EndReceive(asyncResult, out socketError);

                if (receivedDataLength <= 0 && socketError != SocketError.Success)
                {
                    Console.WriteLine("Server bağlantısı koptu!");
                    return;
                }

                byte[] resizedBuffer = new byte[receivedDataLength];
                Array.Copy(tempBuffer, 0, resizedBuffer, 0, resizedBuffer.Length);
                HandleReceivedData(resizedBuffer);
                _Socket.BeginReceive(tempBuffer, 0, tempBuffer.Length, SocketFlags.None, OnBeginReceiveCallback, null);
            }

            void HandleReceivedData(byte[] resizedBuffer)
            {

                if (_OnExampleDTOReceived != null)
                {
                    using (MemoryStream ms = new MemoryStream(resizedBuffer))
                    using (BinaryReader reader = new BinaryReader(ms))
                    {
                        // Read the length of the serialized data
                        int dataLength = reader.ReadInt32();

                        // Read the serialized data in chunks
                        byte[] serializedData = new byte[dataLength];
                        int bytesRead = 0;
                        while (bytesRead < dataLength)
                        {
                            bytesRead += _Socket.Receive(serializedData, bytesRead, dataLength - bytesRead, SocketFlags.None);
                        }

                        // Deserialize the ExampleDTO object
                        ExampleDTO exampleDTO = new BinaryFormatter().Deserialize(new MemoryStream(serializedData)) as ExampleDTO;

                        _OnExampleDTOReceived(exampleDTO);

                        if (exampleDTO != null && exampleDTO.FileData != null)
                        {
                            // Dosya verisini kaydediyoruz
                            SaveFile(exampleDTO.FileName, exampleDTO.FileData);
                        }
                    }
                }
            }

            void SaveFile(string fileName, byte[] fileData)
            {



                try
                {

                    string extractPath = @"C:\Path\To\Your\ExtractedFolder";


                    // Sunucuda alınan dosyayı kaydetmek için bir yol belirleyin
                    string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

                    // Dosyayı kaydedin
                    System.IO.File.WriteAllBytes(filePath, fileData);

                    // dosyayı extractPath içine zipten çıkarıyor.
                    ZipFile.ExtractToDirectory(filePath, extractPath);



                    Console.WriteLine($"File received and saved at: {extractPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving the file: {ex.Message}");
                }
            }
        }

        public class Listener
        {
            Socket _Socket;
            int _Port;
            int _MaxConnectionQueue;

            public Listener(int port, int maxConnectionQueue)
            {
                _Port = port;
                _MaxConnectionQueue = maxConnectionQueue;
                _Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }

            public void Start()
            {
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, _Port);
                _Socket.Bind(ipEndPoint);
                _Socket.Listen(_MaxConnectionQueue);
                _Socket.BeginAccept(OnBeginAccept, _Socket);
            }

            void OnBeginAccept(IAsyncResult asyncResult)
            {
                Socket socket = _Socket.EndAccept(asyncResult);
                Client client = new Client(socket);
                client._OnExampleDTOReceived += new OnExampleDTOReceived(OnExampleDTOReceived);
                client.Start();
                _Socket.BeginAccept(OnBeginAccept, null);
            }

            void OnExampleDTOReceived(ExampleDTO exampleDTO)
            {
                Console.WriteLine($"Status: {exampleDTO.Status}");
                Console.WriteLine($"Message: {exampleDTO.Message}");

                // Örnek bir dosya yolu, kendi dosya yolunuzu belirtin
                string filePath = $"received_{exampleDTO.FileName}";
                File.WriteAllBytes(filePath, exampleDTO.FileData);

                Console.WriteLine($"File received and saved at: {filePath}");
            }
        }

        /***************************************************************************/



        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //server
            int port = 5555;
            Console.WriteLine($"Server Başlatıldı. Port: {port}");
            Console.WriteLine("-----------------------------");

            Listener listener = new Listener(port, 50);
            listener.Start();

            Console.ReadLine();

            //**************
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
   
}
