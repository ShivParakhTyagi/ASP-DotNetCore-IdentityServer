using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace Server.SqlServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            return Task.Run(() =>
            {
                if (context.UserName != "admin")
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "username must be admin");
                }

                context.Result = new GrantValidationResult("100", "username is valid");
            });
        }
    }
}