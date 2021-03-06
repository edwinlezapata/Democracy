﻿namespace Democracy.Common.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Country
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cities")]
        public List<City> Cities { get; set; }

        [JsonProperty("numberCities")]
        public int NumberCities { get; set; }

        public override string ToString() => this.Name;
    }


}
