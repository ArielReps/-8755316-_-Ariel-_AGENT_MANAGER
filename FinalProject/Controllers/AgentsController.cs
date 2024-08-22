using FinalProjectAPI.Data;
using FinalProjectAPI.Models.AuxiliaryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProjectAPI.Services;
using FinalProjectAPI.Services.IServices;
using FinalProjectAPI.Models;

namespace FinalProjectAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentService _service;
        public AgentsController(IAgentService ag)
        {
            _service = ag;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAgent agent)
        {
            // הסרוויס מייצר מטרה ללא מיקום התחלתי
            int id = await _service.Create(agent.Nickname, agent.Photo_url);

            return Created("", new { Id = id }); // מחזיר את המזהה שנוצר
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Agent> agents = await _service.GetAllAgents();

            return Ok(agents);  // להחזיר כאן את אובייקט הרשימה
        }

        [HttpPut]
        [Route("{id}/pin")]
        public async Task<IActionResult> Pin([FromBody] Location location, [FromRoute] int id)
        {
            // לוגיקה שקובעת מטרה שזה עתה נוצרה ונותנת לה נקודות על הצירים
            Agent agent = await _service.GetById(id);
            await _service.InitializeLocation(agent, new System.Drawing.Point(location.X, location.Y));
            return Ok();
        }

        [HttpPut]
        [Route("{id}/move")]
        public async Task<IActionResult> Move([FromBody] Direction direction)
        {
            // לוגיקה שמעדכנת את המיקום לפי המזהה שנשלח
            await _service.Move(direction.Horizontal, direction.Vertical);

            return Ok();
        }
    }
}