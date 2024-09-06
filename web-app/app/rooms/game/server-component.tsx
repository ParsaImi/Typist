'use server'
import { User } from 'next-auth';
import React from 'react';
import { getCurrentUser } from "@/app/actions/authAction"
import Home from "./page"


type Props = {
    user : User
}

export default async function GetUserSession() {
  const session = await getCurrentUser()
  const name = session?.name || "guest"
  return name;
}

