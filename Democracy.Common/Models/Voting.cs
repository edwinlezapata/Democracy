using System;
using System.Collections.Generic;
using System.Text;

namespace Democracy.Common.Models
{
    using Newtonsoft.Json;

    public class Voting
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("candidate")]
        public Candidate Candidate { get; set; }

        [JsonProperty("votingEvent")]
        public VotingEvent VotingEvent { get; set; }
    }
}
