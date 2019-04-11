namespace Democracy.Web.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Candidate : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain a maximum {1} characters")]
        [Display(Name = "Name Candidate")]
        public string NameCandidate { get; set; }

        [Required]
        [MaxLength(ErrorMessage = "The field {0} only can contain a maximum {1} characters")]
        [Display(Name = "Proposal")]
        public string Proposal { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public ICollection<Candidate> Candidates { get; set; }

        public VotingEvent VotingEvent { get; set; }
    }
}
