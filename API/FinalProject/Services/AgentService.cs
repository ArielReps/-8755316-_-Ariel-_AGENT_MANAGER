using FinalProjectAPI.Data;
using FinalProjectAPI.Models;
using FinalProjectAPI.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace FinalProjectAPI.Services
{
    public class AgentService : IAgentService
    {
        private readonly AppDbContext _context;
        private readonly ControlService _controlService;
        public AgentService(AppDbContext db, ControlService controlService)
        {
            _context = db;
            _controlService = controlService;
        }
        public async Task<int> Create(string name, string image)
        {
            Agent agent = new()
            {
                Name = name,
                Image = image
            };
            await _context.AddAsync(agent);
            await _context.SaveChangesAsync();
            return agent.Id;
        }

        public async Task<int> Create(Agent agent)
        {
            await _context.AddAsync(agent);
            await _context.SaveChangesAsync();
            return agent.Id;
        }

        public async Task<bool> Delete(int id)
        {
            Agent? agent = await _context.Agents.FindAsync(id);
            if (agent == null) return false;
            _context.Agents.Remove(agent);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Agent?> GetById(int id)
        {
            return await _context.Agents.FindAsync(id);
        }

        public async Task<IEnumerable<Agent>> GetAllAgents()
        {
            List<Agent> agents = await _context.Agents.ToListAsync();
            return agents;
        }

        public async Task InitializeLocation(Agent agent, Point point)
        {
            agent.Location = point;
            await _context.SaveChangesAsync();
            List<Target> targets = await _context.Targets.ToListAsync();
            List<Agent> agents = await _context.Agents.ToListAsync();
            List<Mission> missions = await _context.Missions.ToListAsync();
            _controlService.SetOffers(_controlService.GetSuitabilities(agents, targets, missions));
        }

        public async Task Move(int id, int x, int y)
        {
            Agent? agent = await _context.Agents.FindAsync(id);
            if (agent == null) return;
            // מוודא שאנחנו לא מוציאים אותו מהגבולות
            if (agent.LocationX + x > 1000 || agent.LocationY + y > 1000) return;
            if (agent.LocationX + x < 1 || agent.LocationY + y < 1) return;
            agent.LocationX += x;
            agent.LocationY += y;
            await _context.SaveChangesAsync();
            List<Target> targets = await _context.Targets.ToListAsync();
            List<Agent> agents = await _context.Agents.ToListAsync();
            List<Mission> missions = await _context.Missions.ToListAsync();
            _controlService.SetOffers(_controlService.GetSuitabilities(agents, targets, missions));
        }

        public async Task<bool> Update(Agent agent)
        {
            Agent? old = await _context.Agents.FindAsync(agent.Id);
            if (old == null) return false;
            agent.Location = old.Location;
            agent.Name = old.Name;
            agent.Image = old.Image;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
