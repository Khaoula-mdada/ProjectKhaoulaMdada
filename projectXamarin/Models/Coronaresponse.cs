using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace projectXamarin.Models
{
    public class CoronaResponse
    {
        //Informations du pays
        [JsonProperty("countrytimelinedata")]
        public List<CountryTimeLineData> CountryData { get; set; }
        public partial class CountryTimeLineData
        {
            [JsonProperty("info")]
            public CountryInfo Info { get; set; }
        }
        public partial class CountryInfo
        {
            [JsonProperty("title")]
            public string Name { get; set; }

            [JsonProperty("code")]
            public string Code { get; set; }
        }

        //Statistiques du pays
        [JsonProperty("timelineitems")]
        public List<object> CountryStats { get; set; }
    }

    public class CountryStatistic
    {
        [JsonProperty("new_daily_cases")]
        public string NewCases { get; set; }

        [JsonProperty("new_daily_deaths")]
        public string NewDeaths { get; set; }

        [JsonProperty("total_cases")]
        public string TotalCases { get; set; }

        [JsonProperty("total_recoveries")]
        public string TotalRecoveries { get; set; }

        [JsonProperty("total_deaths")]
        public string TotalDeaths { get; set; }
    }
}
