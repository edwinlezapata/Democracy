using System.ComponentModel.DataAnnotations;

namespace Democracy.Web.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

}
