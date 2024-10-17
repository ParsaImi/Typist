import React, { useState, useEffect } from 'react';

interface CountdownProps {
  duration: number;
  onFinish: () => void;
}

function Countdown({ duration, onFinish }: CountdownProps) {
  const [secondsLeft, setSecondsLeft] = useState(duration);

  useEffect(() => {
    const intervalId = setInterval(() => {
      if (secondsLeft > 0) {
        setSecondsLeft(secondsLeft - 1);
      } else {
        clearInterval(intervalId);
        onFinish();
      }
    }, 1000);

    return () => clearInterval(intervalId);
  }, [secondsLeft, onFinish]);

  return (
    <div>
      {secondsLeft} seconds left
    </div>
  );
}

export default Countdown;