using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace Server.CustomAuth
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            //return Task.Run(() =>
            //{
                if (context.UserName != "admin" || context.Password != "admin")
                {
                    context.Result =
                        new GrantValidationResult(TokenRequestErrors.InvalidGrant, "username and password must be admin");
                }
                else
                {
                    context.Result = new GrantValidationResult("1", "username is valid");
                }
            //});
            return Task.FromResult(0);
        }
    }
}