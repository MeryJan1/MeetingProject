using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;


namespace CalenderForProject
{
    internal class Client
    {
        [Serializable]
        public class ExampleDTO
        {
            public byte[] FileData { get; set; }
            public string FileName { get; set; }
            public string Message { get; internal set; }
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


        class Program
        {
            static void Main(string[] args)
            {
                int port = 5555;
                Console.WriteLine(string.Format("Client Başlatıldı. Port: {0}", port));
                Console.WriteLine("-----------------------------");

                ExampleSocket exampleSocket = new ExampleSocket(new IPEndPoint(IPAddress.Parse("127git .0.0.1"), port));
                exampleSocket.Start();

                // File PATH
                string filePath = "example.txt";
                ExampleDTO exampleDTO = new ExampleDTO()
                {

                    Message = string.Format("{0} ip numaralı client üzerinden geliyorum!", GetLocalIPAddress()),
                    FileName = Path.GetFileName(filePath),
                    FileData = File.ReadAllBytes(filePath)
                };

                exampleSocket.SendData(exampleDTO);

                Console.ReadLine();


            }

            static string GetLocalIPAddress()
            {
                string localIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault().ToString();

                return localIP;
            }
        }
    }



}
