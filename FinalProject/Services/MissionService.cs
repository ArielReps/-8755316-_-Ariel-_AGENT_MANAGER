using FinalProjectAPI.Data;
using FinalProjectAPI.Models;
using FinalProjectAPI.Models.AuxiliaryModels;
using FinalProjectAPI.Models.BaseModels;
using FinalProjectAPI.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAPI.Services
{
    public class MissionService : IMissionService
    {
        private readonly AppDbContext _context;
        public MissionService(AppDbContext db)
        {
            _context = db;
        }
        public async Task<bool> UpdateStatus(int id, MissionStatus status)
        {
            Mission? mission = await _context.Missions.FindAsync(id);
            if (mission == null) return false;
            mission.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatus(Mission mission, MissionStatus status)
        {
            mission.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public Task DirectMission()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Mission>> GetMissions()
        {
            return await _context.Missions.ToListAsync();
        }

        public Task<IEnumerable<Mission>> OfferMissions()
        {
            throw new NotImplementedException();
        }

        public async Task<MissionPK> Create(int agentId, int targetId)
        {
            Agent? agent = await _context.Agents.FindAsync(agentId);
            Target? target = await _context.Targets.FindAsync(targetId);
            if (agent == null || target == null) return new() { AgentId = 0, TargetId = 0 };
            Mission mission = new()
            {
                Agent = agent,
                AgentId = agent.Id,
                Target = target,
                TargetId = target.Id,
                StartDate = DateTime.Now,
                Status = MissionStatus.Active
            };
            _context.Missions.Add(mission);
            await _context.SaveChangesAsync();
            return new() { AgentId = mission.AgentId, TargetId = mission.TargetId };
        }

        public async Task<MissionPK> Create(Agent agent, Target target)
        {
            Mission mission = new()
            {
                Agent = agent,
                AgentId = agent.Id,
                Target = target,
                TargetId = target.Id,
                StartDate = DateTime.Now,
                Status = MissionStatus.Active
            };
            _context.Missions.Add(mission);
            await _context.SaveChangesAsync();
            return new() { AgentId = mission.AgentId, TargetId = mission.TargetId };
        }

        
    }
}
