namespace Democracy.Common.Models
{
    public class RecoverPasswordRequest
    {
        [Required]
        public string Email { get; set; }
    }

}
