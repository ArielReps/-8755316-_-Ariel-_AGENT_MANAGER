using FinalProjectAPI.Data;
using FinalProjectAPI.Models;
using FinalProjectAPI.Models.AuxiliaryModels;
using FinalProjectAPI.Services;
using FinalProjectAPI.Services.IServices;
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
        public TargetsController(ITargetService ts, ILogger<TargetsController> logger)
        {
            _service = ts;
            _logger = logger;
        }

        // סימולטור
        // מייצר מטרה חדשה
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTarget target)
        {
            // הסרוויס מייצר מטרה ללא מיקום התחלתי
            int id = await _service.Create(target.Name, target.Position, target.PhotoUrl);

            return Created("", new { Id = id }); // מחזיר את המזהה שנוצר
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Target> targets = await _service.GetAllTargets();

            return Ok(targets);  // להחזיר כאן את אובייקט הרשימה
        }

        [HttpPut]
        [Route("{id}/pin")]
        public async Task<IActionResult> Pin([FromBody] Location location, [FromRoute] int id)
        {
            // לוגיקה שקובעת מטרה שזה עתה נוצרה ונותנת לה נקודות על הצירים
            Target target = await _service.GetById(id);
            await _service.InitializeLocation(target, new System.Drawing.Point(location.X, location.Y));
            return Ok();
        }

        [HttpPut]
        [Route("{id}/move")]
        public async Task<IActionResult> Move([FromBody] Direction direction, [FromRoute] int id)
        {
            // לוגיקה שמעדכנת את המיקום לפי המזהה שנשלח
            await _service.Move(id, direction.Horizontal, direction.Vertical);

            return Ok();
        }
    }
}