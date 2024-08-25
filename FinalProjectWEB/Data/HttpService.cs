using FinalProjectWEB.Models;
using FinalProjectWEB.Models.AuxiliaryModels;
using FinalProjectWEB.Models.BaseModels;

namespace FinalProjectWEB.Data
{
    public class HttpService : IHttpService
    {
        public Task<int> ConfirmMission(MissionStatus status)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Agent>> GetAgentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Mission>> GetMissionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Mission>> GetMissionsOffersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Target>> GetTargetsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Token> Login(string userString)
        {
            throw new NotImplementedException();
        }
    }
}
