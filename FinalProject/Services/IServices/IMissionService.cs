using FinalProjectAPI.Models;
using FinalProjectAPI.Models.AuxiliaryModels;
using FinalProjectAPI.Models.BaseModels;

namespace FinalProjectAPI.Services.IServices
{
    public interface IMissionService
    {
        Task<MissionPK> Create(int agentId, int targetId);
        Task<MissionPK> Create(Agent agent, Target target);
        Task DirectMission();
        Task<IEnumerable<Mission>> GetMissions();
        Task<IEnumerable<Mission>> OfferMissions();
        Task<bool> UpdateStatus(int id, MissionStatus status);
        Task<bool> UpdateStatus(Mission mission, MissionStatus status);
    }
}
