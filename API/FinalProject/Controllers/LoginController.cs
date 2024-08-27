using FinalProjectAPI.Models.AuxiliaryModels;
using FinalProjectAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FinalProjectAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ILogger<LoginController> _logger;
        public LoginController(AuthService auth, ILogger<LoginController> logger)
        {
            _authService = auth;
            _logger = logger;
        }
        
        // מחזיר טוקן ע"פ סוג המשתמש
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel userType)
        {
            AuthModel user = userType.Id switch
            {
                "SimulationServer" => new AuthModel { Name = "Simulator Client", Roles = ["Simulator"] },
                "MVCServer" => new AuthModel { Name = "WEB Server", Roles = ["MVC-Server"] },
                "test" => new AuthModel { Name = "Test", Roles = ["Test"] },
                _ => new AuthModel()
            };

            _logger.LogInformation($"{user.Name ?? "not"} registered");
            string token = _authService.Create(user); // אם נכנסים עם סוג אחר לא נצליח ליצור טוקן, ובמקרה זה תוחזר מחרוזת ריקה
            return Ok(new { token = token});
        } 
    }
}
