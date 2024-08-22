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
        public MissionsController(IMissionService ms)
        {
            _service = ms;
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

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> MissionConfirmation([FromBody] MissionStatus status, [FromRoute] int id)
        {
            // המשתמש שולח עדכון סטטוס ובכל מאשר יציאה למשימה
            bool success = await _service.UpdateStatus(id, status);
            return NoContent();
        }
    }
}