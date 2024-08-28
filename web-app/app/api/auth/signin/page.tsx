'use client'

import { signIn } from "next-auth/react"
import React from "react"

export default function Page({searchParams}: {searchParams: {callbackUrl: string}}){
    const clbk = searchParams.callbackUrl
    return (
        <div className="flex flex-col items-center">
        <p className="text-2xl">you are not loogged in!</p>
        <button onClick={() => signIn('id-server', {callbackUrl: '/'})} className="border px-5 py-2 mt-10 rounded">login</button>
        </div>
    )
}