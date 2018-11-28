using System;
using System.IO;
using WebSocketSharp;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        { 
            using (var ws = new WebSocket("ws://192.168.0.231:9999/ws"))
            {
                ws.OnMessage += (sender, e) =>
                {
                    if (e.IsText)
                    {
                        Console.WriteLine("Received from server: " + e.Data);
                        return;
                    }
                    else if (e.IsBinary)
                    {
                        Console.WriteLine($"Received {e.RawData.Length} bytes from server");
                        return;
                    }
                };

                ws.Connect();
                //ws.Send("MESSAGGIO DI TEST");
                byte[] fileBytes = File.ReadAllBytes("image.jpg");
                Action<bool> printCompleted = delegate (bool completed)
                {
                    if (completed)
                        Console.WriteLine("Send completed");
                    else
                        Console.WriteLine("Send not completed");
                };

                ws.Send(fileBytes);

                Console.ReadKey(true);
            }
        }
        
    }

    
}
