using HP.API.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace HP.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(User user, List<string> Roles);
    }
}
