using System;
using System.Collections.Generic;
using System.Text;

namespace Democracy.Common.Models
{
    using Newtonsoft.Json;

    public class Voting
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("candidate")]
        public int Candidate { get; set; }

        [JsonProperty("votingEvent")]
        public int VotingEvent { get; set; }
    }
}
