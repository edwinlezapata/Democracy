namespace Democracy.Common.Models
{
    using Newtonsoft.Json;

    public class RegisterVote
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("event")]
        public int Event { get; set; }

        [JsonProperty("candidate")]
        public int Candidate { get; set; }

    }
}
