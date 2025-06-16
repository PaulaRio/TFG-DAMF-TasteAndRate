using Microsoft.AspNetCore.Identity;

namespace TasteAndRateAPI.Models.Entity
{
    public class AppUser : IdentityUser
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public ICollection<ValoracionEntity> Valoraciones { get; set; }
    }
}
