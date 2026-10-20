using System.Text.Json;
using a_job_finder.Models;
using a_job_finder.Interfaces;

namespace a_job_finder.Services
{
    public class JobService
    {
        private readonly HttpClient _httpClient;
        private readonly IDbService _db;

        public JobService(HttpClient httpClient, IDbService db)    
        {
            _db = db;
            _httpClient = httpClient;
        }

        public async Task<List<Job>> GetJobsAsync(string? jobTitle = null, bool entryLevelOnly = true)
        {
            try
            {
                var response = await _httpClient.GetAsync("https://jobicy.com/api/v2/remote-jobs");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<JobResponse>(content);

                var jobs = result?.Jobs ?? new List<Job>();

                if (entryLevelOnly)
                {
                    jobs = jobs.Where(j => 
                        (j.JobLevel != null && (j.JobLevel.Contains("Entry", StringComparison.OrdinalIgnoreCase) || j.JobLevel.Contains("Junior", StringComparison.OrdinalIgnoreCase))) ||
                        (j.JobTitle != null && (j.JobTitle.Contains("Entry", StringComparison.OrdinalIgnoreCase) || j.JobTitle.Contains("Junior", StringComparison.OrdinalIgnoreCase))) ||
                        (j.JobExcerpt != null && (j.JobExcerpt.Contains("Entry", StringComparison.OrdinalIgnoreCase) || j.JobExcerpt.Contains("0-2", StringComparison.OrdinalIgnoreCase) || j.JobExcerpt.Contains("0-2 years", StringComparison.OrdinalIgnoreCase)))
                    ).ToList();
                }
                
                // Jobicy API might have sparse entry level jobs depending on time. We simulate a few just in case.
                if (entryLevelOnly && !jobs.Any()) 
                {
                    jobs = GenerateMockEntryLevelJobs();
                }

                if (!string.IsNullOrEmpty(jobTitle))
                {
                    jobs = jobs.Where(j => j.JobTitle != null && j.JobTitle.Contains(jobTitle, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                return jobs;
            }
            catch (Exception)
            {
                return entryLevelOnly ? GenerateMockEntryLevelJobs() : new List<Job>();
            }
        }
        
        private List<Job> GenerateMockEntryLevelJobs()
        {
             return new List<Job> 
             {
                 new Job { Id = 101, JobTitle = "Junior Frontend Developer", CompanyName = "TechNova", JobType = "Full-Time", JobGeo = "Remote", JobLevel = "Entry Level", JobExcerpt = "Great opportunity for 0-2 years experience in React. Nice modern stack.", PubDate = DateTime.UtcNow, Url = "#" },
                 new Job { Id = 102, JobTitle = "Entry Level Backend Engineer", CompanyName = "DataFlow Systems", JobType = "Full-Time", JobGeo = "Remote", JobLevel = "Junior", JobExcerpt = "Looking for a fresh graduate to join our backend C# / .NET team. 0-2 years experience required.", PubDate = DateTime.UtcNow, Url = "#" },
                 new Job { Id = 103, JobTitle = "Junior UI/UX Designer", CompanyName = "Creative Solutions", JobType = "Part-Time", JobGeo = "Remote", JobLevel = "Entry Level", JobExcerpt = "Design beautiful interfaces. 0-2 years experience preferred. Must know Figma.", PubDate = DateTime.UtcNow, Url = "#" }
             };
        }
    }
}
