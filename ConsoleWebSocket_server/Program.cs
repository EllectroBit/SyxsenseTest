using System;
using System.Net.Http;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace ConsoleWebSocket_server
{
    class Program
    {
        static void Main(string[] args)
        {
            WebSocketServer server = new WebSocketServer("ws://localhost:7890");

            server.Start();
            Console.WriteLine("Server started");

            server.AddWebSocketService<SystemInformation>("/Information");
            server.AddWebSocketService<CheckOnline>("/Online");
            server.AddWebSocketService<SendRequestToDB>("/SQL");

            Console.ReadKey();
            server.Stop();
        }
    }

    public class SystemInformation : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            using(localhost.WebService1 proxy = new localhost.WebService1())
            {
                var response = proxy.SendInformationModelJSON(e.Data);
                Console.WriteLine("Computer Name: " + response.ComputerName + "\nTime Zone: " + response.TimeZone + "\nOS: " 
                    + response.OS_Name + "\n.Net Version: " + response.NetVersion);
            }
        }
    }

    public class CheckOnline : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            using (localhost.WebService1 proxy = new localhost.WebService1())
            {
                proxy.SendOnlineStatus();
            }
        }
    }

    public class SendRequestToDB : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            using (localhost.WebService1 proxy = new localhost.WebService1())
            {
                proxy.SendRequestToDB(e.Data);
            }
        }
    }
}
