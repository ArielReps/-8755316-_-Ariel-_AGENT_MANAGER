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
        private readonly ControlService _controlService;

        
        public MissionService(AppDbContext db, ControlService controlService)
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

        public async Task<bool> UpdateStatus(int aid, int tid, MissionStatus status)
        {
            Agent agent = await _context.Agents.FindAsync(aid);
            agent.Status = AgentStatus.Active;
            Target target = await _context.Targets.FindAsync(tid);
            Mission n = new()
            {
                Agent = agent,
                AgentId = aid,
                Target = target,
                TargetId = tid,
                Status = status,
                StartDate = DateTime.Now
            };
            _context.Missions.Add(n);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DirectMission()
        {
            List<Mission> missions = await _context.Missions.Include(x => x.Agent).Include(x => x.Target).ToListAsync();
            foreach (var mission in missions)
            {
                RecDirection dir = _controlService.DirectAgent(mission);
                mission.Agent.LocationX += dir.x;
                mission.Agent.LocationY += dir.y;
                if (mission.Agent.LocationX == mission.Target.LocationX
                && mission.Agent.LocationY == mission.Target.LocationY)
                {
                    mission.Status = MissionStatus.Done;
                    mission.Agent.Status = AgentStatus.Dormant;
                    mission.Target.Status = TargetStatus.Eliminated;
                    mission.ExecutionDate = DateTime.Now;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Mission>> GetMissions()
        {
            IEnumerable<Mission> missions = await _context.Missions.ToListAsync();
            foreach (var item in missions)
            {
                item.Agent = await _context.Agents.FindAsync(item.AgentId);
                item.Target = await _context.Targets.FindAsync(item.TargetId);
                item.EstimatedTime = _controlService.GetTime(item);
            }
            return missions;
        }

        public async Task<IEnumerable<Mission>> OfferMissions()
        {
            List<Target> targets = await _context.Targets.ToListAsync();
            List<Agent> agents = await _context.Agents.ToListAsync();
            List<Mission> missions = await _context.Missions.ToListAsync();
            _controlService.SetOffers(_controlService.GetSuitabilities(agents, targets, missions));
            IEnumerable<Mission> offeredMissions = _controlService.GetMissionOffers();
            foreach (var item in offeredMissions)
            {
                item.EstimatedTime = _controlService.GetTime(item);
            }
            return offeredMissions;
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
