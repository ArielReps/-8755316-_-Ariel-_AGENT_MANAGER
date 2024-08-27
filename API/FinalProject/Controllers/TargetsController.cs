using FinalProjectAPI.Data;
using FinalProjectAPI.Models;
using FinalProjectAPI.Models.AuxiliaryModels;
using FinalProjectAPI.Services;
using FinalProjectAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TargetsController : ControllerBase
    {
        private readonly ITargetService _service;
        private readonly ILogger<TargetsController> _logger;
        private static readonly string _errorMessage = "There was a problem while we tried connect to the DB...";
        public TargetsController(ITargetService ts, ILogger<TargetsController> logger)
        {
            _service = ts;
            _logger = logger;
        }

        // סימולטור
        // מייצר מטרה חדשה
        [Authorize(Roles = "Simulator,Test")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTarget target)
        {
            try
            {
                // הסרוויס מייצר מטרה ללא מיקום התחלתי
                int id = await _service.Create(target.Name, target.Position, target.PhotoUrl);
                _logger.LogInformation("");
                return Created("A new target is created, with no initial location", new { Id = id }); // מחזיר את המזהה שנוצר
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating new target");
                return Problem(_errorMessage, statusCode: 500);
            }
        }

        // מחזיר רשימה של כל המטרות
        [Authorize(Roles = "Simulator,Test,MVC-Server")] // מוסיף הרשאה גם לשרת MVC
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<Target> targets = await _service.GetAllTargets();
                _logger.LogInformation("A client requested the list of targets");
                return Ok(targets);  // מחזיר כאן את אובייקט הרשימה
            }
            catch (Exception ex)
            {
                _logger.LogError("Error retrieving the target list");
                return Problem(_errorMessage, statusCode: 500);
            }
        }

        // קובע מיקום למטרה. סימולטור בלבד
        [Authorize(Roles = "Simulator,Test")]
        [HttpPut]
        [Route("{id}/pin")]
        public async Task<IActionResult> Pin([FromBody] Location location, [FromRoute] int id)
        {
            try
            {
                // לוגיקה שקובעת מטרה שזה עתה נוצרה ונותנת לה נקודות על הצירים
                Target target = await _service.GetById(id);
                await _service.InitializeLocation(target, new System.Drawing.Point(location.X, location.Y));
                _logger.LogInformation("Target received location");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error setting location for target");
                return Problem(_errorMessage, statusCode: 500);
            }
        }

        // מזיז מטרה לאחד משמנה כיוונים. סימולטור בלבד
        [Authorize(Roles = "Simulator,Test")]
        [HttpPut]
        [Route("{id}/move")]
        public async Task<IActionResult> Move([FromBody] Direction direction, [FromRoute] int id)
        {
            try
            {
                // לוגיקה שמעדכנת את המיקום לפי המזהה שנשלח
                await _service.Move(id, direction.Horizontal, direction.Vertical);
                _logger.LogInformation("Target has been moved");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error moving the target");
                return Problem(_errorMessage, statusCode: 500);
            }
        }
    }
}