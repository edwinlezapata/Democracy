using Newtonsoft.Json;
using System;

namespace Democracy.Common.Models
{

    public class VotingEventsWithCandidate
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nameCandidate")]
        public string NameCandidate { get; set; }

        [JsonProperty("proposal")]
        public string Proposal { get; set; }

        [JsonProperty("imageFullPath")]
        public Uri ImageFullPath { get; set; }

        [JsonProperty("numberVotes")]
        public int NumberVotes { get; set; }

        [JsonProperty("eventId")]
        public int EventId { get; set; }

        [JsonProperty("eventName")]
        public string EventName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("startDate")]
        public DateTime? StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime? EndDate { get; set; }

        public bool ValidateUserVote { get; set; }

        public bool LetVote { get; set; }

        public string Paint { get; set; }

    }

}
