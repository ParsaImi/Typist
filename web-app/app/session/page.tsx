import Link from "next/link"
import React from "react"
import { getCurrentUser } from "../actions/authAction"
import { Button } from "flowbite-react"
import { signOut } from "next-auth/react"
import { User } from "next-auth"




type Props = {
    user : User
}
export default async function Session(){
    const session = await getCurrentUser()
    const name = JSON.stringify(session?.name,null,2)
    const image = JSON.stringify(session?.image,null,2)
    const username = JSON.stringify(session?.username,null,2)
    return (
        <div>
            <div>   
                <h3 className="text-4xl mb-10">your profile</h3>
                <h2 className="text-lg">Hey {name} !</h2>
                <p>players will see your name as {username}</p>
            </div>
        </div>
    )
}