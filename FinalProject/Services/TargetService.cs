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
        public Task<int> Create(string name, string role, string image)
        {
            throw new NotImplementedException();
        }

        public Task<int> Create(Target target)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Target> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Target>> GetAllTargets()
        {
            throw new NotImplementedException();
        }

        public Task InitializeLocation(Target target, Point point)
        {
            throw new NotImplementedException();
        }

        public Task Move(int x, int y)
        {
            throw new NotImplementedException();
        }

        public Task Update(Target target)
        {
            throw new NotImplementedException();
        }
    }
}
