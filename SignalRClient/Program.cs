using System;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Building the connection to SignalRHub
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/ChatHub")
                .Build();
            
            // Genered a random number between 0 - 100
            Random rnd = new Random();
            int ID   = rnd.Next(100);  

            // Subscribe to the event send to listen to the messages sent
            connection.On<string>("Send", (message)=>{
                System.Console.WriteLine(message);
            });

            // Stop conecction
            connection.StartAsync().Wait();

            while (true) {
                System.Console.WriteLine("Type a message:");
                // Get input message
                var message = Console.ReadLine();

                // Sending message of one client
                connection.SendAsync("Send", "Client with ID "+ID+" say: "+message);
            }
        }
    }
}
