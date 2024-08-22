using FinalProjectAPI.Models;
using FinalProjectAPI.Models.BaseModels;

namespace FinalProjectAPI.Services.IServices
{
    public interface IMissionService
    {
        Task DirectMission();
        Task<IEnumerable<Mission>> GetMissions();
        Task<IEnumerable<Mission>> OfferMissions();
        Task ChangeStatus(MissionStatus status);
    }
}
