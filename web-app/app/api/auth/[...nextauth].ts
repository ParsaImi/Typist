import NextAuth, { NextAuthOptions }  from "next-auth";

import DuendeIdentityServer6 from "next-auth/providers/duende-identity-server6";

export const authOptions: NextAuthOptions = {
    session: {
        strategy: 'jwt'
    },
    providers: [
        DuendeIdentityServer6({
            id: 'id-server',
            clientId: 'nextApp',
            clientSecret: process.env.CLIENT_SECRET!,
            issuer: process.env.IDENTITY_URL,
            authorization: {params: {scope: 'openid profile TypistApi'}},
            idToken: true

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
                session.user.username = token.username
            }
            return session
        }
    }

}

const handler = NextAuth(authOptions);
export { handler as GET, handler as POST};