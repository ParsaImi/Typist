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
                url: 'http://localhost:5001/connect/authorize'
            },
            token: {
                url: `http://localhost:5001/connect/token`
            },
            userinfo: {
                url: `http://localhost:5001/connect/token`
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
