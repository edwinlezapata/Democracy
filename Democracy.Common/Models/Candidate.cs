using Newtonsoft.Json;
using System;

namespace Democracy.Common.Models
{
    public class Candidate
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("proposal")]
        public string Proposal { get; set; }

        [JsonProperty("numberVotes")]
        public int NumberVotes { get; set; }

        [JsonProperty("photo")]
        public string ImageUrl { get; set; }

        [JsonProperty("imageFullPath")]
        public string ImageFullPath { get; set; }

        public string NumberVotesText => $"Total of votes: {Convert.ToString(this.NumberVotes)}";
    }
}
