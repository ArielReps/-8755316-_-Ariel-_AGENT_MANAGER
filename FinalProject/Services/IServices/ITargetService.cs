using FinalProjectAPI.Models;
using System.Drawing;

namespace FinalProjectAPI.Services.IServices
{
    public interface ITargetService
    {
        Task<int> Create(string name, string role, string image);
        Task<int> Create(Target target);
        Task<bool> Update(Target target);
        Task<bool> Delete(int id);

        Task<IEnumerable<Target>> GetAllTargets();
        Task<Target?> GetById(int id);
        Task Move(int id, int x, int y);
        Task InitializeLocation(Target target, Point point);
    }
}
