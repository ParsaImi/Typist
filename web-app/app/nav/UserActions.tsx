'use client'
import { Dropdown } from "flowbite-react";
import { signOut } from "next-auth/react";
import Link from "next/link"
import React from "react"
import { HiCog, HiCurrencyDollar, HiLogout, HiViewGrid } from "react-icons/hi";

export default function UserActions(){
    return (
        <Dropdown label="Dropdown" className="p-4">
          <a href="/session"><Dropdown.Item icon={HiViewGrid}>Dashboard</Dropdown.Item></a>
          <Dropdown.Divider />
          <Dropdown.Item icon={HiLogout} onClick={() => signOut({callbackUrl:'/'})}>Sign out</Dropdown.Item>
        </Dropdown>
      );
}