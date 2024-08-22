using FinalProjectAPI.Data;
using FinalProjectAPI.Models;
using FinalProjectAPI.Services.IServices;
using System.Drawing;

namespace FinalProjectAPI.Services
{
    public class TargetService : ITargetService
    {
        private readonly AppDbContext _context;
        public TargetService(AppDbContext db)
        {
            _context = db;
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
        }

        public async Task Move(int id, int x, int y)
        {
            Target? target = await _context.Targets.FindAsync(id);
            if (target == null) return;
            target.LocationX += x; target.LocationY += y;
            await _context.SaveChangesAsync();
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
