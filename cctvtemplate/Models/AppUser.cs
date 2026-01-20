using Microsoft.AspNetCore.Identity;

namespace cctvtemplate.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName = string.Empty;
    }
}
