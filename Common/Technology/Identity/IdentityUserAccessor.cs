using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Common.Adapters.Identity.Data.Model;

namespace Common.Technology.Identity;
public sealed class IdentityUserAccessor(
    UserManager<User> userManager,
    IdentityRedirectManager redirectManager)
{
    public async Task<User> GetRequiredUserAsync(HttpContext context)
    {
        var user = await userManager.GetUserAsync(context.User);

        if (user is null)
        {
            redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
        }

        return user;
    }
}
