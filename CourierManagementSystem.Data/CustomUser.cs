using Microsoft.AspNetCore.Identity;

namespace CourierManagementSystem.Data
{
    public class CustomUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
