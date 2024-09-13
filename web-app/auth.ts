import NextAuth from "next-auth";

import DuendeIdentityServer6 from "next-auth/providers/duende-identity-server6";

export const { handlers , signIn, signOut , auth } = NextAuth({
    session: {
        strategy: 'jwt'
    },
    providers: [
        DuendeIdentityServer6({
            id: 'id-server',
            clientId: 'nextApp',
            clientSecret: 'secret',
            issuer: 'identity-svc',
            authorization: {
                params: { scope: 'openid profile TypistApi' },
                url: `https://id.parsaimi.xyz/connect/authorize`
            },
            token: {
                url: `https://id.parsaimi.xyz/connect/token`
            },
            userinfo: {
                url: `https://id.parsaimi.xyz/connect/token`
            },


        })
    ],
    callbacks: {
        async jwt({token,profile}){
            if(profile){
                token.username = profile.username
            }
            return token;
        },
        async session({session,token}){
            if(token){
                session.user.username = token.username as string
            }
            return session
        }
    }

})
