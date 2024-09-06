
import { Card } from "flowbite-react";
import { useEffect, useRef, useState } from 'react';
import React from 'react';
console.log("test from websockettt!")
export const useWebSocket = (url: string) => {
  const [receivedMessages, setReceivedMessages] = useState<string[]>([]);
  const ws = useRef<WebSocket | null>(null);
  console.log("test from wwwwebsocket!")
  useEffect(() => {
    console.log("test from websocket!")
    
    ws.current = new WebSocket(url);

    ws.current.onopen = () => {
      console.log('Connected to WebSocket server');
    };

    ws.current.onmessage = (event: MessageEvent) => {
      setReceivedMessages((prevMessages) => [...prevMessages, event.data]);
      console.log(event.data);
      
    };

    ws.current.onclose = () => {
      console.log('Disconnected from WebSocket server');
    };

    return () => {
      if (ws.current) {
        ws.current.close();
      }
    };
  }, [url]);

  const sendMessage = (message: string) => {
    if (ws.current && ws.current.readyState === WebSocket.OPEN) {
      ws.current.send(message);
    }
  };

  return { receivedMessages, sendMessage };
};