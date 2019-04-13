﻿using Newtonsoft.Json;

namespace Democracy.Common.Models
{
    public class VotingEvent
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("imageUrl")]
        public object ImageUrl { get; set; }

        [JsonProperty("eventName")]
        public string EventName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("startDate")]
        public object StartDate { get; set; }

        [JsonProperty("endDate")]
        public object EndDate { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    } 
}
