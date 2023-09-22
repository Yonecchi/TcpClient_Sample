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
        /// <summary>
        /// メイン処理
        /// </summary>
        /// <param name="args">引数: ip port</param>
        static void Main(string[] args)
        {
            Client client = new Client();
            Task t1 = Task.Run(() => {
                client.client_exe(args);
            });

            while (true)
            {
                if (t1.IsCompleted)
                {
                    Console.WriteLine("タスクが正常終了しました。\n");
                    break;
                }
                if(t1.IsFaulted)
                {
                    Console.WriteLine("タスクが異常終了しました。IsFaulted : "+t1.Exception.Message);
                    break;
                }
                if(t1.IsCanceled)
                {
                    Console.WriteLine("タスクが取り消されました。IsCanceled");
                    break;
                }
            }

        }

    }
}
