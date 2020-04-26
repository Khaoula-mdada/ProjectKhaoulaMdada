using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace projectXamarin.Models
{
    public class Coronaresponse
    {
        [JsonProperty("countrytimelinedata")]
        public List<Info> Countrytimelinedata { get; set; }

        [JsonProperty("coronaTimeData")]
        public CoronaTimeData CoronaTime { get; set; }

           
        public partial class Info
        {

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("code")]
            public string Code { get; set; }

        }
        public partial class CoronaTimeData
        {
            [JsonProperty("new_daily_cases ")]
            public string New_daily_cases { get; set; }

            [JsonProperty("new_daily_deaths")]
            public string New_daily_deaths { get; set; }

            [JsonProperty("total_cases")]
            public string Total_cases { get; set; }
            [JsonProperty("total_recoveries")]
            public string Total_recoveries { get; set; }

            [JsonProperty("total_deaths")]
            public string Total_deaths { get; set; }
        }

    }
}
