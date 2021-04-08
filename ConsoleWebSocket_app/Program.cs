using Newtonsoft.Json;
using System;
using WebServices;

using WebSocketSharp;

namespace ConsoleWebSocket_app
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press to execute First Task");
            Console.ReadKey();

            //FirstTask
            using (WebSocket ws = new WebSocket("ws://localhost:7890/Information"))
            {
                ws.Connect();

                InformationModel model = new InformationModel();
                model.ComputerName = Environment.MachineName;
                model.TimeZone = TimeZoneInfo.Local.DaylightName;
                model.OS_Name = Environment.OSVersion.ToString();
                model.NetVersion = Environment.Version.ToString();

                string json = JsonConvert.SerializeObject(model);

                ws.Send(json);
            }

            Console.WriteLine("Done!\n\n");
            Console.WriteLine("Press to execute Second Task");
            Console.ReadKey();

            //Second Task
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(5);

            var timer = new System.Threading.Timer((e) =>
            {
                using (WebSocket ws = new WebSocket("ws://localhost:7890/Online"))
                {
                    ws.Connect();
                    ws.Send("Online");
                }
            }, null, startTimeSpan, periodTimeSpan);


            Console.WriteLine("Done!\n\n");
            Console.WriteLine("Press to execute Third Task");
            Console.ReadKey();

            //ThirdTask
            //follow the link to track changes
            //http://www.testsite.somee.com 
            using (WebSocket ws = new WebSocket("ws://localhost:7890/SQL"))
            {
                ws.Connect();

                dynamic model = new { Id = 1, Name = "Tom", Age = 14 };

                string json = JsonConvert.SerializeObject(model);

                ws.Send(json);
            }

            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}
