using FinalProjectWEB.Data;
using FinalProjectWEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectWEB.Controllers
{
    public class DataController : Controller
    {
        private readonly IHttpService _service;
        public DataController(IHttpService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Agents()
        {
            IEnumerable<Agent> agents = await _service.GetAgentsAsync();
            return View(agents);
        }

        public async Task<IActionResult> Targets()
        {
            IEnumerable<Target> targets = await _service.GetTargetsAsync();
            return View(targets);
        }

        public async Task<IActionResult> Missions()
        {
            IEnumerable<Mission> missions = await _service.GetMissionsAsync();
            return View(missions);
        }
    }
}
