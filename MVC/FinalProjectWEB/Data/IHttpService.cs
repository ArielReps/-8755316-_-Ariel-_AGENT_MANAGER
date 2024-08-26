using FinalProjectWEB.Models;
using FinalProjectWEB.Models.AuxiliaryModels;
using FinalProjectWEB.Models.BaseModels;

namespace FinalProjectWEB.Data
{
    public interface IHttpService
    {
        Task<IEnumerable<Agent>> GetAgentsAsync();
        Task<IEnumerable<Target>> GetTargetsAsync();
        Task<IEnumerable<Mission>> GetMissionsAsync();
        Task<IEnumerable<Mission>> GetMissionsOffersAsync();
        Task<bool> ConfirmMission(int aid, int tid, MissionStatus status);
        Task<LoginModel> Login(string userString); 
    }
}
