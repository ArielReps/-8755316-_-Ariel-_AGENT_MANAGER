using FinalProjectAPI.Data;
using FinalProjectAPI.Models;
using FinalProjectAPI.Models.BaseModels;
using FinalProjectAPI.Services.IServices;

namespace FinalProjectAPI.Services
{
    public class MissionService : IMissionService
    {
        private readonly AppDbContext _context;
        public MissionService(AppDbContext db)
        {
            _context = db;
        }
        public Task ChangeStatus(MissionStatus status)
        {
            throw new NotImplementedException();
        }

        public Task DirectMission()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Mission>> GetMissions()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Mission>> OfferMissions()
        {
            throw new NotImplementedException();
        }
    }
}
