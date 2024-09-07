using System.Net.Http.Headers;
using Duende.IdentityServer.Models;

namespace IdentityService;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("TypistApi" , "All of the apis for Typist"),
        };

    public static IEnumerable<Client> Clients(IConfiguration config) =>
        new Client[]
        {
            new Client
            {
                ClientId = "postman",
                ClientName = "postman",
                AllowedScopes = {"openid", "profile" , "TypistApi"},
                RedirectUris = {"https://www.google.com"},
                ClientSecrets = new[] {new Secret("NotASecret".Sha256())},
                AllowedGrantTypes = {GrantType.ResourceOwnerPassword}
            },
            new Client
            {
                ClientId = "nextApp",
                ClientName = "nextApp",
                ClientSecrets = {new Secret("secret".Sha256())},
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                AllowedScopes = {"openid", "profile" , "TypistApi"},
                RequirePkce = false,
                RedirectUris = {"http://171.22.26.169:3000/api/auth/callback/id-server"},
                AllowOfflineAccess = true,
                AccessTokenLifetime = 3600*24*30,
                AlwaysIncludeUserClaimsInIdToken = true 

            }
            
            
        };

  
}
