using System.Text.Json.Serialization;

namespace a_job_finder.Models
{
    public class Job
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("jobSlug")]
        public string JobSlug { get; set; } = string.Empty;

        [JsonPropertyName("jobTitle")]
        public string JobTitle { get; set; } = string.Empty;

        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; } = string.Empty;

        [JsonPropertyName("companyLogo")]
        public string CompanyLogo { get; set; } = string.Empty;

        [JsonPropertyName("jobIndustry")]
        public string JobIndustry { get; set; } = string.Empty;

        [JsonPropertyName("jobType")]
        public string JobType { get; set; } = string.Empty;

        [JsonPropertyName("jobGeo")]
        public string JobGeo { get; set; } = string.Empty;

        [JsonPropertyName("jobLevel")]
        public string JobLevel { get; set; } = string.Empty;

        [JsonPropertyName("jobExcerpt")]
        public string JobExcerpt { get; set; } = string.Empty;

        [JsonPropertyName("jobDescription")]
        public string JobDescription { get; set; } = string.Empty;

        [JsonPropertyName("pubDate")]
        public DateTime PubDate { get; set; }
    }
}
