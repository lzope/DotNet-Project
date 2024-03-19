using Microsoft.AspNetCore.Identity;

namespace WebApplication5.Models
{
    public class AppUser : IdentityUser
    {
        public string firstname {  get; set; }
        public string lastname { get; set; }
    }
}
