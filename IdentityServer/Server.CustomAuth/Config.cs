using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Server.CustomAuth
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("test-api", "Test Api"),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client()
                {
                    ClientId = "customAuth",
                    ClientSecrets = new[] {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = new[] {"test-api"}
                },
            };
        }

    }
}