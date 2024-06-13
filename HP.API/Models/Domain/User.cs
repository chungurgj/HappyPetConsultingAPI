using Microsoft.AspNetCore.Identity;

namespace HP.API.Models.Domain
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
