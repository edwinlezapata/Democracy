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
        [MaxLength(100, ErrorMessage = "The field {0} only can contain a maximum {1} characters")]
        [Display(Name = "Proposal")]
        public string Proposal { get; set; }

        [Display(Name = "Photo")]
        public string ImageUrl { get; set; }

        public ICollection<Voting> Votings { get; set; }

        [Display(Name = "# Votes")]
        public int NumberVotes => this.Votings == null ? 0 : this.Votings.Count;


        public VotingEvent VotingEvent { get; set; }

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
    }
}
