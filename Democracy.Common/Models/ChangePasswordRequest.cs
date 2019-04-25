namespace Democracy.Common.Models
{
    public class ChangePasswordRequest
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string Email { get; set; }
    }

}
