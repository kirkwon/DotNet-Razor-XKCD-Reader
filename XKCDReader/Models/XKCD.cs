using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;

namespace XKCDReader.Models
{
    // Derived from result of call to https://xkcd.com/{id}/info.0.json

    public class XKCD
	{
        [JsonProperty("month")]
        public long Month { get; set; }

        [JsonProperty("num")]
        public long Num { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("year")]
        public long Year { get; set; }

        [JsonProperty("news")]
        public string News { get; set; }

        [JsonProperty("safe_title")]
        public string SafeTitle { get; set; }

        [JsonProperty("transcript")]
        public string Transcript { get; set; }

        [JsonProperty("alt")]
        public string Alt { get; set; }

        [JsonProperty("img")]
        public Uri Img { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("day")]
        public long Day { get; set; }
    }
}

