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
        public AgentService(AppDbContext db)
        {
            _context = db;
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
        }

        public async Task Move(int id, int x, int y)
        {
            Agent? agent = await _context.Agents.FindAsync(id);
            if (agent == null) return;
            agent.LocationX += x;
            agent.LocationY += y;
            await _context.SaveChangesAsync();
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
