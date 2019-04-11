namespace Democracy.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class VotingEvent:IEntity
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [Required]
        [Display(Name = "Event Description")]
        public string Description { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        public User User { get; set; }
    }

}
