using System.ComponentModel.DataAnnotations;

namespace Smart_Egypt_Ticket.DTO
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Nationality is required")]
        //public string Nationality { get; set; } = string.Empty;
        //[Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;        
    }
}
