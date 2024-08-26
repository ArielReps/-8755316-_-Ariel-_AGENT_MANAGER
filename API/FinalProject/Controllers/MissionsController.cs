using FinalProjectAPI.Data;
using FinalProjectAPI.Models;
using FinalProjectAPI.Models.BaseModels;
using FinalProjectAPI.Services;
using FinalProjectAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private readonly IMissionService _service;
        private readonly ILogger<MissionsController> _logger;
        public MissionsController(IMissionService ms, ILogger<MissionsController> logger)
        {
            _service = ms;
            _logger = logger;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update()
        {
            // רענון הסוכנים לכיוון המטרה, אם יש להם כזו
            await _service.DirectMission();
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // שליפת כל המשימות ממסד הנתונים
            IEnumerable<Mission> missions = await _service.GetMissions();
            return Ok(missions);
        }

        [HttpGet]
        [Route("offers")]
        public async Task<IActionResult> GetOffers()
        {
            
            // שליפת כל המשימות ממסד הנתונים
            IEnumerable<Mission> missions = await _service.OfferMissions();
            return Ok(missions);
        }

        [HttpPut]
        [Route("{aid}/{tid}")]
        public async Task<IActionResult> MissionConfirmation([FromBody] MissionStatus status, [FromRoute] int aid, [FromRoute] int tid)
        {
            // המשתמש שולח עדכון סטטוס ובכך מאשר יציאה למשימה
            bool success = await _service.UpdateStatus(aid, tid, status);

            return NoContent();
        }
    }
}