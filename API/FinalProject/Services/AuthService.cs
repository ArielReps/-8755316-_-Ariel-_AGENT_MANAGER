using FinalProjectAPI.Models.AuxiliaryModels;

namespace FinalProjectAPI.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration con)
        {
            _configuration = con;
        }

        //public string GenerateToken(LoginModel login)
        //{

        //}
    }
}
