'use client'

import React, { useState, useEffect} from 'react';
import { useWebSocket } from './WebSocketContext';
import { GetServerSideProps } from 'next';
import { User } from 'next-auth';
import { Card } from "flowbite-react";


  
interface MessageData {
  name: string;
  message: string;
}
type HomeProps = {
  username: string
}


export default function Home({
  children,
}: {
  children: React.ReactNode
}) {
  setTimeout(() => {  
  }, 8000); // 3000 milliseconds = 3 seconds
  const [message, setMessage] = useState<string>('');
  const { receivedMessages, sendMessage } = useWebSocket("ws://171.22.26.169:5227");
  console.log(process.env.NEXT_PUBLIC_WEBSOCKET_URL , "yooo this is websocket ip dowg");
  console.log(process.env.NEXT_PUBLIC_WEBSOCKET_URL_INTERNAL , "internal one");
  console.log(process.env.CLIENT_SECRET , "this is secret");
  // config the type area!
  const [typedLetters, setTypedLetters] = useState<{ [key: number]: string[] }>({});
  const [currentWordIndex, setCurrentWordIndex] = useState(0);
  const [expectedLetter, setExpectedLetter] = useState<string | null>(null);
  const [incorrectLetter, setIncorrectLetter] = useState<string | null>(null);

  const sentence = "The quick brown fox jumps over the lazy dog";
  const words = sentence.split(' ');

  useEffect(() => {
    const handleKeyPress = (e: KeyboardEvent) => {
      if (e.key.length === 1 && (e.key.match(/[a-zA-Z]/) || e.key === ' ')) {
        e.preventDefault(); // Prevent default action (e.g., scrolling)

        const currentWord = words[currentWordIndex];
        const currentTypedLetters = typedLetters[currentWordIndex] || [];
        const nextLetter = currentWord[currentTypedLetters.length] || '';
        const {_payload} = children as any
        const UserName = JSON.parse(_payload.value)
        
        if (e.key === ' ') {
          // Handle space character
          if (currentWord === [...currentTypedLetters].join('')) {
            
            sendMessage(JSON.stringify({name : UserName , message : currentWord}))
            // socket?.send("word completed")
            // Move to the next word if the current word is correctly typed
            console.log(words.length)
            console.log(currentWordIndex , "shit")
            if(words.length == currentWordIndex + 1){
              sendMessage(JSON.stringify({name : UserName , message : "endmatchdone"}))
            }
            setCurrentWordIndex(prevIndex => {
              const nextIndex = prevIndex + 1;
              if (nextIndex < words.length) {
                setExpectedLetter(words[nextIndex][0]);
                return nextIndex;
              }
              return prevIndex; // Stay at the last word if no more words
            });
            setIncorrectLetter(null); // Reset incorrect letter
          } else {
            // Incorrect space typed, highlight in red
            setIncorrectLetter(' ');
          }
        } else if (e.key === nextLetter) {
          // Handle correct letter
          setTypedLetters(prev => ({
            ...prev,
            [currentWordIndex]: [...currentTypedLetters, e.key],
          }));
          setIncorrectLetter(null); // Reset incorrect letter

          // Update the expected letter
          if (currentTypedLetters.length + 1 === currentWord.length) {
            // If the current word is fully typed, set space as the next expected character
            setExpectedLetter(' ');
          } else {
            setExpectedLetter(currentWord[currentTypedLetters.length + 1] || '');
          }
        } else {
          // Incorrect letter
          setIncorrectLetter(e.key);
        }
      }
    };

    window.addEventListener('keypress', handleKeyPress);
    return () => window.removeEventListener('keypress', handleKeyPress);
  }, [typedLetters, currentWordIndex, incorrectLetter, sendMessage, words],);

  

  const renderSentence = () => {
    return words.map((word, index) => {
      const letters = word.split('');
      const typedLettersInWord = typedLetters[index] || [];
      const isCurrentWord = index === currentWordIndex;
      const currentExpectedLetter = isCurrentWord ? expectedLetter : '';

      return (
        <span key={index}>
          {letters.map((letter, idx) => {
            let className = '';
            if (isCurrentWord && letter === currentExpectedLetter) {
              if (incorrectLetter === letter) {
                className = 'text-red-500 font-bold'; // Incorrect letter
              }
            } else if (typedLettersInWord[idx] === letter) {
              className = 'text-green-500 font-bold'; // Correctly typed letter
            }
            return (
              <span key={idx} className={className}>
                {letter}
              </span>
            );
          })}{' '}
        </span>
      );
    });
  };

  const renderMessage = (message: string, index: number) => {
    try {
      if(message.includes("thewinneris")){
        const winner = message.split(",")[0].split(":")[1]
        
        
        return (
          <p className='text-2xl mt-5 col text-yellow-500'>the winner is {winner}</p>
        );
      }
      
      // const data = JSON.parse(message); // Parse JSON string to JavaScript object
      // const { name, message: msg } = data; // Destructure properties

      // // Conditional rendering based on properties
      // if (message === 'thewinneris') {
      // }
    } catch (error) {
      console.error('Error parsing message:', error);
      return null;
    }
    return null;
  };

  return (
    <div className='flex flex-col items-center'>
      <p></p>
    <div className='mb-10 mt-2'>
        <h1 className="text-2xl mb-4">Type the following sentence: </h1>
    </div>
    <div className="flex flex-col items-center justify-center px-9 py-20 bg-gray-500 rounded-xl">
      <p className="text-3xl mb-4">{renderSentence()}</p>
    </div>
    <div className="text-xl mt-4">Press keys to start typing...</div>
    {receivedMessages.map((message, index) => 
        renderMessage(message , index)
)}
    </div>

  );
}







