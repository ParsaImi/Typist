'use server'
import { User, getServerSession } from 'next-auth';
import { authOptions } from '@/app/api/auth/[...nextauth]'; // Ensure you have the correct path
import React from 'react';
import { getSession } from "@/app/actions/authAction"
import Home from "./page"


type Props = {
    user : User
}

export default async function GetUserSession() {
  const session = await getSession()
  const name = session?.user?.name || "guest"
  return name;
}

