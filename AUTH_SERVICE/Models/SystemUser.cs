using Microsoft.AspNetCore.Identity;

namespace AUTH_SERVICE.Models
{
    public class SystemUser : IdentityUser
    {
        
        public string name { get; set; } = string.Empty;
       
    }
}
