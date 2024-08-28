using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace WebSocketServer.Middleware
{
    public class WebSocketServerConnectionManager
    {
        private ConcurrentDictionary<string , WebSocket> _socket =new ConcurrentDictionary<string , WebSocket>();

        public ConcurrentDictionary<string , WebSocket> GetAllSocket()
        {
            return _socket;
        }
        public string AddSocket(WebSocket socket)
        {
            string ConnID = Guid.NewGuid().ToString();
            _socket.TryAdd(ConnID , socket);
            Console.WriteLine(ConnID + "created");
            return ConnID;
        }
    }
}