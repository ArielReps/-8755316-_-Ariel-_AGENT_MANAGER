using FinalProjectAPI.Models;
using System.Drawing;

namespace FinalProjectAPI.Services.IServices
{
    public interface IAgentService
    {
        Task<int> Create(string name, string image);
        Task<int> Create(Agent agent);
        Task Update(Agent agent);
        Task Delete(int id);

        Task<IEnumerable<Agent>> GetAllAgents();
        Task<Agent> GetById(int id);
        Task Move(int x, int y);
        Task InitializeLocation(Agent agent, Point point);
    }
}
