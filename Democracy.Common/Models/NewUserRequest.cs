using System;

namespace Democracy.Common.Models
{
    public class NewUserRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Occupation { get; set; }

        [Required]
        public string Stratum { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime? BirthDay { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int CityId { get; set; }
    }

}
