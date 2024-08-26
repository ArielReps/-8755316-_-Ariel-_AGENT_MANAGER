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
        public LoginController(AuthService auth)
        {
            _authService = auth;
        }
        
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // כאן צריך לייצר לו טוקן
            string token = _authService.GenerateToken(model.Token);
            return Ok(new { token });
        } 
    }
}
