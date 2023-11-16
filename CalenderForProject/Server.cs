using ExampleDataTransferObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;



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
    internal class Server
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
                    // Sunucuda alınan dosyayı kaydetmek için bir yol belirleyin
                    string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

                    // Dosyayı kaydedin
                    System.IO.File.WriteAllBytes(filePath, fileData);

                    Console.WriteLine($"File received and saved at: {filePath}");
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

        class Program
        {
            static void Main(string[] args)
            {
                int port = 5555;
                Console.WriteLine($"Server Başlatıldı. Port: {port}");
                Console.WriteLine("-----------------------------");

                Listener listener = new Listener(port, 50);
                listener.Start();

                Console.ReadLine();
            }
        }
    }
    
    /*
      static class Program
      {

          /// <summary>
          /// The main entry point for the application.
          /// </summary>
           [STAThread]
          private static void Main(string[] args)
          {
              int port = 5555;
              Console.WriteLine(string.Format("Server Başlatıldı. Port: {0}", port));
              Console.WriteLine("-----------------------------");

              Listener listener = new Listener(port, 50);

              listener.Start();

              Console.ReadLine();

          }
      }

      internal class Server
      {
          public delegate void OnExampleDTOReceived(ExampleDTO eDTO);

          public class Client
          {
              #region Variables
              public OnExampleDTOReceived _OnExampleDTOReceived;
              Socket _Socket;

              // Socket işlemleri sırasında oluşabilecek errorları bu enum ile handle edebiliriz.
              SocketError socketError;
              byte[] tempBuffer = new byte[1024]; // 1024 boyutunda temp bir buffer, gelen verinin boyutu kadarıyla bunu receive kısmında handle edeceğiz.
              #endregion

              #region Constructor
              public Client(Socket socket)
              {
                  _Socket = socket;
              }
              #endregion

              #region Public Methods
              public void Start()
              {
                  // Socket üzerinden data dinlemeye başlıyoruz.
                  _Socket.BeginReceive(tempBuffer, 0, tempBuffer.Length, SocketFlags.None, OnBeginReceiveCallback, null);
              }
              #endregion

              #region Private Methods
              void OnBeginReceiveCallback(IAsyncResult asyncResult)
              {
                  // Almayı bitiriyoruz ve gelen byte array'in boyutunu vermektedir.
                  int receivedDataLength = _Socket.EndReceive(asyncResult, out socketError);

                  if (receivedDataLength <= 0 && socketError != SocketError.Success)
                  {
                      // Gelen byte array verisi boş ise bağlantı kopmuş demektir. Burayı istediğiniz gibi handle edebilirsiniz.
                      Console.WriteLine("Server bağlantısı koptu!");
                      return;
                  }

                  // Gelen byte array boyutunda yeni bir byte array oluşturuyoruz.
                  byte[] resizedBuffer = new byte[receivedDataLength];

                  Array.Copy(tempBuffer, 0, resizedBuffer, 0, resizedBuffer.Length);

                  // Gelen datayı burada ele alacağız.
                  HandleReceivedData(resizedBuffer);

                  // Tekrardan socket üzerinden data dinlemeye başlıyoruz.
                  // Start();

                  // Socket üzerinden data dinlemeye başlıyoruz.
                  _Socket.BeginReceive(tempBuffer, 0, tempBuffer.Length, SocketFlags.None, OnBeginReceiveCallback, null);
              }

              /// <summary>
              /// Gelen datayı handle edeceğimiz nokta.
              /// </summary>
              /// <param name="resizedBuffer"></param>
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
                      }
                  }
                  #endregion
              }




          }
          public class Listener
          {
              #region Variables
              Socket _Socket;
              int _Port;
              int _MaxConnectionQueue;
              #endregion

              #region Constructor
              public Listener(int port, int maxConnectionQueue)
              {
                  _Port = port;
                  _MaxConnectionQueue = maxConnectionQueue;

                  // Socket'i tanımlıyoruz IPv4, socket tipimiz stream olacak ve TCP Protokolü ile haberleşeceğiz. 
                  // TCP Protokolünde server belirlenen portu dinler ve gelen istekleri karşılar oysaki UDP Protokolünde tek bir socket üzerinden birden çok client'a ulaşmak mümkündür.
                  _Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
              }
              #endregion

              #region Public Methods
              public void Start()
              {
                  IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, _Port);

                  // Socket'e herhangi bir yerden ve belirttiğimiz porttan gelecek olan bağlantıları belirtmeliyiz.
                  _Socket.Bind(ipEndPoint);

                  // Socketten gelecek olan bağlantıları dinlemeye başlıyoruz ve maksimum dinleyeceği bağlantıyı belirtiyoruz.
                  _Socket.Listen(_MaxConnectionQueue);

                  // BeginAccept ile asenkron olarak gelen bağlantıları kabul ediyoruz.
                  _Socket.BeginAccept(OnBeginAccept, _Socket);
              }
              #endregion

              #region Private Methods
              void OnBeginAccept(IAsyncResult asyncResult)
              {
                  Socket socket = _Socket.EndAccept(asyncResult);
                  Client client = new Client(socket);

                  // Client tarafından gönderilen datamızı işleyeceğimiz kısım.
                  client._OnExampleDTOReceived += new OnExampleDTOReceived(OnExampleDTOReceived);
                  client.Start();

                  // Tekrardan dinlemeye devam diyoruz.
                  _Socket.BeginAccept(OnBeginAccept, null);
              }

              void OnExampleDTOReceived(ExampleDTO exampleDTO)
              {
                  / Client tarafından gelen data, istediğiniz gibi burada handle edebilirsiniz senaryonuza göre.
                  Console.WriteLine(string.Format("Status: {0}", exampleDTO.Status));
                  Console.WriteLine(string.Format("Message: {0}", exampleDTO.Message));
                  Console.WriteLine($"Status: {exampleDTO.Status}");

                  // Gelen dosyayı kaydet
                  string receivedFileName = exampleDTO.FileName;
                  string receivedFilePath = $"received_files/{receivedFileName}";

                  File.WriteAllBytes(receivedFilePath, exampleDTO.FileData);

                  Console.WriteLine($"Received file saved to '{receivedFilePath}'");
              }
              #endregion
          }



      }

      */

}
