using System.ComponentModel.DataAnnotations;

namespace Smart_Egypt_Ticket.DTO
{
    public class LoginUserDTO
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
