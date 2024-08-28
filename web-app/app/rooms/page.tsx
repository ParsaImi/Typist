'use client'
import Image from "next/image";

export default function Rooms() {
  return(
    <div className="container pd-5">
    <div className="border border-gray-300 rounded-lg p-4 flex justify-between items-center mt-10">
    <span className="text-lg text-white-700">Typist Server 1</span>
    <div>
    <span className="mr-10">Players : 2/2</span>
    <button className="bg-white text-black px-7 py-2 rounded-lg hover:bg-white" data-id="1">
      <a href="/rooms/game">
      Join
      </a>
    </button>
    </div>
    </div>
    <div className="border border-gray-300 rounded-lg p-4 flex justify-between items-center mt-10">
    <span className="text-lg text-white-700">Typist Server 2</span>
    <div>
    <span className="mr-10">Players : 2/2</span>
    <button className="bg-white text-black px-7 py-2 rounded-lg hover:bg-white" data-id="1">
      <a href="/rooms/game">
      Join
      </a>
    </button>
    </div>
    </div>
    <div className="border border-gray-300 rounded-lg p-4 flex justify-between items-center mt-10">
    <span className="text-lg text-white-700">Typist Server 3</span>
    <div>
    <span className="mr-10">Players : 2/2</span>
    <button className="bg-white text-black px-7 py-2 rounded-lg hover:bg-white" data-id="1">
      <a href="/rooms/game">
      Join
      </a>
    </button>
    </div>
    </div>
    </div>
    
    
  );
}
