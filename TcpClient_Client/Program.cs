using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TcpClient_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // サーバのIPアドレスとポートを設定
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 12345;

            TcpClient client = new TcpClient();

            try
            {
                // サーバに接続
                client.Connect(ipAddress, port);
                Console.WriteLine("サーバに接続しました。");

                // ネットワークストリームを取得
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    // メッセージの入力
                    Console.Write("メッセージを入力してください (exitで終了): ");
                    string message = Console.ReadLine();

                    if (message == "exit")
                        break;

                    // メッセージをサーバに送信
                    byte[] data = Encoding.ASCII.GetBytes(message);
                    stream.Write(data, 0, data.Length);

                    // サーバからの応答を受信
                    data = new byte[1024];
                    int bytesRead = stream.Read(data, 0, data.Length);
                    string receivedMessage = Encoding.ASCII.GetString(data, 0, bytesRead);
                    Console.WriteLine("サーバからの応答: " + receivedMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("エラー: " + ex.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }
}
