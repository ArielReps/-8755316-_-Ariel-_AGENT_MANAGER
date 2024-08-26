using FinalProjectAPI.Data;
using FinalProjectAPI.Models;
using FinalProjectAPI.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace FinalProjectAPI.Services
{
    public class TargetService : ITargetService
    {
        private readonly AppDbContext _context;
        private readonly ControlService _controlService;
        public TargetService(AppDbContext db, ControlService controlService)
        {
            _context = db;
            _controlService = controlService;
        }
        public async Task<int> Create(string name, string role, string image)
        {
            Target target = new()
            {
                Name = name,
                Role = role,
                Image = image
            };
            _context.Targets.Add(target);
            await _context.SaveChangesAsync();
            return target.Id;
        }

        public async Task<int> Create(Target target)
        {
            _context.Targets.Add(target);
            await _context.SaveChangesAsync();
            return target.Id;
        }

        public async Task<bool> Delete(int id)
        {
            Target? target = await _context.Targets.FindAsync(id);
            if (target == null) return false;
            _context.Targets.Remove(target);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Target?> GetById(int id)
        {
            Target? target = await _context.Targets.FindAsync(id);
            return target;
        }

        public async Task<IEnumerable<Target>> GetAllTargets()
        {
            return _context.Targets.ToList();
        }

        public async Task InitializeLocation(Target target, Point point)
        {
            target.Location = point;
            await _context.SaveChangesAsync();
            List<Target> targets = await _context.Targets.ToListAsync();
            List<Agent> agents = await _context.Agents.ToListAsync();
            List<Mission> missions = await _context.Missions.ToListAsync();
            _controlService.SetOffers(_controlService.GetSuitabilities(agents, targets, missions));
        }

        public async Task Move(int id, int x, int y)
        {
            Target? target = await _context.Targets.FindAsync(id);
            if (target == null) return;
            if (target.Status == Models.BaseModels.TargetStatus.Eliminated) return;
            // מוודא שאנחנו לא מוציאים אותו מהגבולות
            if (target.LocationX + x > 1000 ||  target.LocationY + y > 1000) return;
            if (target.LocationX + x < 1 || target.LocationY + y < 1) return;
            target.LocationX += x; target.LocationY += y;
            await _context.SaveChangesAsync();
            List<Target> targets = await _context.Targets.ToListAsync();
            List<Agent> agents = await _context.Agents.ToListAsync();
            List<Mission> missions = await _context.Missions.ToListAsync();
            _controlService.SetOffers(_controlService.GetSuitabilities(agents, targets, missions));
        }

        public async Task<bool> Update(Target target)
        {
            Target? old = await _context.Targets.FindAsync(target.Id);
            if (old == null) return false;
            old.Name = target.Name;
            old.Status = target.Status;
            old.Role = target.Role;
            old.Image = target.Image;
            old.Location = target.Location;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
