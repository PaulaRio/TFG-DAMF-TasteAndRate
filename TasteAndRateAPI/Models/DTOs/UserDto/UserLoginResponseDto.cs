using TasteAndRateAPI.Models.Entity;

namespace TasteAndRateAPI.Models.DTOs.UserDto
{
    public class UserLoginResponseDto
    {
        public AppUser User { get; set; }
        public string Token { get; set; }

  

    }
}
