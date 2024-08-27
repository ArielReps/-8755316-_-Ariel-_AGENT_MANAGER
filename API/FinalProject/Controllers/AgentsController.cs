using FinalProjectAPI.Data;
using FinalProjectAPI.Models.AuxiliaryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProjectAPI.Services;
using FinalProjectAPI.Services.IServices;
using FinalProjectAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace FinalProjectAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentService _service;
        private readonly ILogger<AgentsController> _logger;
        private static readonly string _errorMessage = "There was a problem while we tried connect to the DB...";
        public AgentsController(IAgentService ag, ILogger<AgentsController> logger)
        {
            _service = ag;
            _logger = logger;
        }

        // יוצר סוכן חדש ללא מיקום התחלתי. סימולטור בלבד
        [Authorize(Roles = "Simulator,Test")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAgent agent)
        {
            try
            {
                int id = await _service.Create(agent.Nickname, agent.PhotoUrl);
                _logger.LogInformation("A new agent is created, with no initial location");
                return Created("", new { Id = id }); // מחזיר את המזהה שנוצר
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating new agent");
                return Problem(_errorMessage, statusCode: 500);
            }
        }

        // מחזיר רשימה של כל הסוכנים
        [Authorize(Roles = "Simulator,Test,MVC-Server")] // מוסיף הרשאה גם לשרת MVC
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<Agent> agents = await _service.GetAllAgents();
                _logger.LogInformation("A client requested the list of agents");
                return Ok(agents);  // מחזיר כאן את אובייקט הרשימה
            }
            catch (Exception ex)
            {
                _logger.LogError("Error retrieving the agent list");
                return Problem(_errorMessage, statusCode: 500);
            }
        }

        // קובע מיקום לסוכן, סימולטור בלבד
        [Authorize(Roles = "Simulator,Test")]
        [HttpPut]
        [Route("{id}/pin")]
        public async Task<IActionResult> Pin([FromBody] Location location, [FromRoute] int id)
        {
            try
            {
                // לוגיקה שקובעת סוכן שזה עתה נוצר ונותנת לו נקודות על הצירים
                Agent agent = await _service.GetById(id);
                await _service.InitializeLocation(agent, new System.Drawing.Point(location.X, location.Y));
                _logger.LogInformation("Agent received location");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error setting location for agent");
                return Problem(_errorMessage, statusCode: 500);
            }
        }

        // מזיז סוכן לאחד משמנה כיוונים. סימולטור בלבד
        [Authorize(Roles = "Simulator,Test")]
        [HttpPut]
        [Route("{id}/move")]
        public async Task<IActionResult> Move([FromBody] Direction direction, [FromRoute] int id)
        {
            try
            {
                // לוגיקה שמעדכנת את המיקום לפי המזהה שנשלח
                await _service.Move(id, direction.Horizontal, direction.Vertical);
                _logger.LogInformation("Agent has been moved");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error moving the agent");
                return Problem(_errorMessage, statusCode: 500);
            }
        }
    }
}