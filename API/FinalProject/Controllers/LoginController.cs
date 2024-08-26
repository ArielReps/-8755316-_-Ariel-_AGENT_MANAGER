using FinalProjectAPI.Models.AuxiliaryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FinalProjectAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] string userString)
        {
            // כאן צריך לייצר לו טוקן
            LoginModel model = new LoginModel();
            model.Token = userString + " login";
            return Ok(model);
        } 
    }
}
