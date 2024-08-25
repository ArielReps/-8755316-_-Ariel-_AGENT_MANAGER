using FinalProjectWEB.Data;
using FinalProjectWEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectWEB.Controllers
{
    public class MissionsController : Controller
    {
        private readonly IHttpService _service;
        public MissionsController(IHttpService service)
        {
            _service = service;
        }

        [Route("/missions/details/{aid}/{tid}")]
        public async Task<IActionResult> Details([FromRoute] int aid, [FromRoute] int tid)
        {
            IEnumerable<Mission> offers = await _service.GetMissionsOffersAsync();
            Mission mission = offers.FirstOrDefault(o => o.AgentId == aid && o.TargetId == tid);
            return View(mission);
        }

        [Route("/missions/confirm/{aid}/{tid}")]
        public IActionResult Confirm([FromRoute] int aid, [FromRoute] int tid)
        {
            _service.ConfirmMission(Models.BaseModels.MissionStatus.Active);
            return RedirectToAction("Offers");
        }

        public async Task<IActionResult> Offers()
        {
            IEnumerable<Mission> offers = await _service.GetMissionsOffersAsync();
            return View(offers);
        }
    }
}
