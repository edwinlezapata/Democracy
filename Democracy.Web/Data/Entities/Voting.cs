using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Democracy.Web.Data.Entities
{
    public class Voting : IEntity
    {
        public int Id { get; set; }

        [Required]
        public VotingEvent VotingEvent { get; set; }

        [Required]
        public Candidate Candidate { get; set; }

        [Required]
        public User User { get; set; }

        public int Vote { get; set; }

    }
}
