using FinalProjectAPI.Data;
using FinalProjectAPI.Models;
using FinalProjectAPI.Models.BaseModels;
using FinalProjectAPI.Services;
using FinalProjectAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
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
        private static readonly string _errorMessage = "There was a problem while we tried connect to the DB...";
        public MissionsController(IMissionService ms, ILogger<MissionsController> logger)
        {
            _service = ms;
            _logger = logger;
        }

        // מזיז את כל הסוכנים שפבעולה צעד קדימה לכיוון המטרה. סימולטור בלבד
        [Authorize(Roles = "Simulator,test")]
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update()
        {
            try
            {
                // רענון הסוכנים לכיוון המטרה, אם יש להם כזו
                await _service.DirectMission();
                _logger.LogInformation("Updates the location of the agents towards the target");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating agent location");
                return Problem(_errorMessage, statusCode: 500);
            }
        }

        [Authorize(Roles = "Simulator,Test,MVC-Server")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // שליפת כל המשימות ממסד הנתונים
            IEnumerable<Mission> missions = await _service.GetMissions();
            return Ok(missions);
        }

        [Authorize(Roles = "Simulator,Test,MVC-Server")]
        [HttpGet]
        [Route("offers")]
        public async Task<IActionResult> GetOffers()
        {
            
            // שליפת כל המשימות ממסד הנתונים
            IEnumerable<Mission> missions = await _service.OfferMissions();
            return Ok(missions);
        }

        [Authorize(Roles = "MVC-Server,test")]
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