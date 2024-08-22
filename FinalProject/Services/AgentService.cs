using FinalProjectAPI.Data;
using FinalProjectAPI.Models;
using FinalProjectAPI.Services.IServices;
using System.Drawing;

namespace FinalProjectAPI.Services
{
    public class AgentService : IAgentService
    {
        private readonly AppDbContext _context;
        public AgentService(AppDbContext db)
        {
            _context = db;
        }
        public Task<int> Create(string name, string image)
        {
            throw new NotImplementedException();
        }

        public Task<int> Create(Agent agent)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Agent> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Agent>> GetAllAgents()
        {
            throw new NotImplementedException();
        }

        public Task InitializeLocation(Agent agent, Point point)
        {
            throw new NotImplementedException();
        }

        public Task Move(int x, int y)
        {
            throw new NotImplementedException();
        }

        public Task Update(Agent agent)
        {
            throw new NotImplementedException();
        }
    }
}
