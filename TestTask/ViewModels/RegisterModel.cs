using System.ComponentModel.DataAnnotations;

namespace TestTask.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email is not specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is not specidied")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is incorrect")]
        public string ConfirmPassword { get; set; }
    }
}
