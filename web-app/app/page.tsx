'use client'
import Image from "next/image";

export default function Home() {
  return (
   <div className="flex flex-col justify-center items-center">
    <div style={{ fontSize: '60px'}}>
    TYPING
    </div>
    <div style={{ fontSize: '60px'}}>
    COMPETITION
    </div>
    <div style={{ fontSize: '60px'}}> 
      IN TYPIST
    </div>
    <div>
      <a href="/rooms">
      <button style={{ padding: '8px 16px', backgroundColor: 'white', color: 'black', borderRadius: '4px', marginTop: "20px" }}>online rooms</button>
      </a>
    </div>
   </div>
  );
}

