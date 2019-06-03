using Newtonsoft.Json;
using System;

namespace Democracy.Common.Models
{
    public class VotingEvent
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("imageFullPath")]
        public Uri ImageFullPath { get; set; }

        [JsonProperty("eventName")]
        public string EventName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("startDate")]
        public DateTime? StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime? EndDate { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("candidates")]
        public Candidate[] Candidates { get; set; }

        [JsonProperty("numberCandidates")]
        public int NumberCandidates { get; set; }
    } 
}
