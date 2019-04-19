namespace Democracy.Web.Models
{
    using Democracy.Web.Data.Entities;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class CandidateViewModel 
    {
        public int VotingEventId { get; set; }

        public int CandidateId { get; set; }

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImageUrl))
                {
                    return null;
                }

                return $"https://democracy.azurewebsites.net{this.ImageUrl.Substring(1)}";
            }
        }

        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain a maximum {1} characters")]
        [Display(Name = "Name Candidate")]
        public string NameCandidate { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The field {0} only can contain a maximum {1} characters")]
        [Display(Name = "Proposal")]
        public string Proposal { get; set; }

    }
}
