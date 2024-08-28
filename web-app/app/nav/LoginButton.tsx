'use client'

import { signIn } from "next-auth/react"
import React from "react"

export default function LoginButton(){
    return (
        <button onClick={() => signIn('id-server', {callbackUrl: '/'})}>login</button>
    )
}