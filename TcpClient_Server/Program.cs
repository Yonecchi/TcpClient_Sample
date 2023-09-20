using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TcpClient_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // サーバのIPアドレスとポートを設定
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 12345;

            TcpListener server = new TcpListener(ipAddress, port);

            try
            {
                // サーバを起動
                server.Start();
                Console.WriteLine("サーバが起動しました。クライアントの接続を待機中...");

                // クライアントからの接続を待機
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("クライアントが接続しました。");

                // ネットワークストリームを取得
                NetworkStream stream = client.GetStream();

                // データの受信と送信
                byte[] data = new byte[1024];
                int bytesRead;
                while ((bytesRead = stream.Read(data, 0, data.Length)) != 0)
                {
                    string receivedMessage = Encoding.ASCII.GetString(data, 0, bytesRead);
                    Console.WriteLine("クライアントから受信: " + receivedMessage);

                    // クライアントにデータを送信
                    string responseMessage = "サーバからの応答: " + receivedMessage;
                    byte[] responseData = Encoding.ASCII.GetBytes(responseMessage);
                    stream.Write(responseData, 0, responseData.Length);
                }

                // 接続を閉じる
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("エラー: " + ex.Message);
            }
            finally
            {
                server.Stop();
            }
        }
    }
}
