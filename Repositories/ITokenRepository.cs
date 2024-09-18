using Microsoft.AspNetCore.Identity;

namespace ExploreAPIs.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
