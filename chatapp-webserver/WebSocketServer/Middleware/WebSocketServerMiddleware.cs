using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Routing.Tree;

namespace WebSocketServer.Middleware
{
    public class WebSocketServerMiddleware
    {
        public class MessageData
        {
            public string name { get; set; }
            public string message { get; set; }
        }
        private readonly RequestDelegate _next;

        private readonly WebSocketServerConnectionManager _manager;

        public WebSocketServerMiddleware(RequestDelegate next , WebSocketServerConnectionManager manager)
        {
            _next = next;
            _manager = manager;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
                {   
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    Console.WriteLine("WebSocket Connected");
                    string ConnID = _manager.AddSocket(webSocket);
                    await SendConnIDAsync(webSocket , ConnID);
                    await ReceiveMessage(webSocket, async (result , buffer) =>
                    {
                        if(result.MessageType == WebSocketMessageType.Text)
                        {
                    
                            Console.WriteLine("message recieved");
                            Console.WriteLine($"Message : {Encoding.UTF8.GetString(buffer , 0 , result.Count)}");
                            var message = Encoding.UTF8.GetString(buffer , 0 , result.Count);
                            var data = JsonConvert.DeserializeObject<MessageData>(message);
                            if (data.message == "endmatchdone"){
                                Console.WriteLine("the winner is");
                                Console.WriteLine(data.name);
                                string winner_message = $"{{\"name\":\"{data.name}\",\"message\":\"{"thewinneris"}\"}}";
                                await RouteJSONMessageAsync(winner_message);
                            }else{
                                string word_message = $"{{\"name\":\"{data.name}\",\"message\":\"{data.message}\"}}";
                                await RouteJSONMessageAsync(word_message);
                            }
                            return;
                        }
                        else if (result.MessageType == WebSocketMessageType.Close)
                        {
                            Console.WriteLine("recieved close message");
                            string id = _manager.GetAllSocket().FirstOrDefault(s => s.Value == webSocket).Key;
                            _manager.GetAllSocket().TryRemove(id , out WebSocket sock);
                            await sock.CloseAsync(result.CloseStatus.Value,result.CloseStatusDescription,CancellationToken.None);
                            return;
                        }
                    });
                }
                else
                {
                    await _next(context);
                }
        }

        private async Task ReceiveMessage(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 *4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer : new ArraySegment<byte>(buffer), cancellationToken : CancellationToken.None);
                handleMessage(result , buffer);
            }

        }

        private async Task SendConnIDAsync(WebSocket socket , string ConnID)
        {
            var buffer = Encoding.UTF8.GetBytes("ConnID: " + ConnID);
            await socket.SendAsync(buffer , WebSocketMessageType.Text , true , cancellationToken : CancellationToken.None);
        }
    
        // the blow Task is just work for broadcast message till next updates‌ !
        public async Task RouteJSONMessageAsync(string message)
        {
            Console.WriteLine(message);
            var bytes = Encoding.UTF8.GetBytes(message);

        // Create an ArraySegment from the byte array
            var buffer = new ArraySegment<byte>(bytes);

            // if(Guid.TryParse(routeOb.To.ToString(), out Guid guidOutput))
            // {
            //     // implement for next updates
            // }
            // else
            // {
                
                foreach(var sock in _manager.GetAllSocket())
                {
                    if(sock.Value.State == WebSocketState.Open)
                    {
                        await sock.Value.SendAsync(buffer , WebSocketMessageType.Text , true , CancellationToken.None);
                    }
                    // if(routeOb.Message.ToString() == "goingdark")
                    // {
                    //     Console.WriteLine(sock);
                    //     Console.WriteLine(sock.Value.State);
                    // }
                }
            // }
        }
    
    }
}