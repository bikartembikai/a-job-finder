using System.Text.Json.Serialization;

namespace a_job_finder.Models
{
    public class JobResponse
    {
        [JsonPropertyName("jobs")]
        public List<Job> Jobs { get; set; } = new List<Job>();

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
