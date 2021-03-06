﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace MyDemo.ID4
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("api1", "我的第一个资源")
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                //1.注册客户端
                new Client{
                  ClientId="console client",
                  ClientName="console 测试程序",
                  AllowedGrantTypes= GrantTypes.ClientCredentials,
                  ClientSecrets={ new Secret("console client".Sha256()) },
                  AllowedScopes={ "api1"}

                },
                // client credentials flow client
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "api1" }
                },

                // MVC client using code flow + pkce
                //new Client
                //{
                //    ClientId = "mvc",
                //    ClientName = "MVC Client",

                //    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                //    RequirePkce = true,
                //    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                //    RedirectUris = { "http://localhost:5003/signin-oidc" },
                //    FrontChannelLogoutUri = "http://localhost:5003/signout-oidc",
                //    PostLogoutRedirectUris = { "http://localhost:5003/signout-callback-oidc" },

                //    AllowOfflineAccess = true,
                //    AllowedScopes = { "openid", "profile", "api1" }
                //},

                // SPA client using code flow + pkce
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA Client",
                    ClientUri = "http://identityserver.io",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris =
                    {
                        "http://localhost:5002/index.html",
                        "http://localhost:5002/callback.html",
                        "http://localhost:5002/silent.html",
                        "http://localhost:5002/popup.html",
                    },

                    PostLogoutRedirectUris = { "http://localhost:5002/index.html" },
                    AllowedCorsOrigins = { "http://localhost:5002" },

                    AllowedScopes = { "openid", "profile", "api1" }
                },new Client
                {
                     ClientId="mvc",
                     ClientName="mvc",
                     ClientSecrets= { new Secret("mvc".Sha256()) },
                       AllowedGrantTypes = GrantTypes.Code,
                        RequireConsent = false,
                        RequirePkce = true,
                       // where to redirect to after login
                        RedirectUris = { "http://localhost:5002/signin-oidc" },
                        FrontChannelLogoutUri = "http://localhost:5002/signout-oidc",
                        // where to redirect to after logout
                        PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                        AccessTokenLifetime=600,
                     
                        AlwaysIncludeUserClaimsInIdToken = true,
                        AllowOfflineAccess = true, // offline_access
                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "api1"
                        },
                       
                }
            };
    }
}