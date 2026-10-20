using Microsoft.AspNetCore.Mvc;
using a_job_finder.Services;

namespace a_job_finder.Controllers
{
    public class JobController : Controller
    {
        private readonly JobService _jobService;

        public JobController(JobService jobService)
        {
            _jobService = jobService;
        }
        [HttpGet("get")]
        public async Task<IActionResult> Index(string search = "", bool entryLevelOnly = true)
        {
            ViewBag.Search = search;
            ViewBag.EntryLevelOnly = entryLevelOnly;

            var jobs = await _jobService.GetJobsAsync(search, entryLevelOnly);
            return View(jobs);
        }
    }
}
