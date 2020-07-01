using Microsoft.AspNetCore.Identity;

namespace OdeToFood.Core
{
    public class OwnUser:IdentityUser
    {
        public string RestoRating { get; set; }

    }
    
}