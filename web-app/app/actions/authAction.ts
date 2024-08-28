import { getServerSession } from "next-auth";
import { authOptions } from "../api/auth/[...nextauth]";

export async function getSession(){
    return await getServerSession(authOptions);
}

export async function getCurrentUser(){
    try {
        const session = await getSession()
        console.log(session + "ppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppp")
        if(!session) return null
        return session.user
    } catch (error) {
        return null
    }
}