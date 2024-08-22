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
        private readonly IControlService _controlService;

        
        public MissionService(AppDbContext db, IControlService controlService)
        {
            _context = db;
            _controlService = controlService;
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

        public async Task DirectMission()
        {
            List<Mission> missions = await _context.Missions.ToListAsync();
            foreach (var mission in missions)
            {
                RecDirection dir = _controlService.DirectAgent(mission);
                mission.Agent.LocationX += dir.x;
                mission.Agent.LocationY += dir.y;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Mission>> GetMissions()
        {
            return await _context.Missions.ToListAsync();
        }

        public async Task<IEnumerable<Mission>> OfferMissions()
        {
            return _controlService.GetMissionOffers();
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

        public async Task Save(Mission mission)
        {
            _context.Missions.Add(mission);
            _context.SaveChanges();
        }
    }
}
