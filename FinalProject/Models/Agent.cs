using FinalProjectAPI.Models.BaseModels;

namespace FinalProjectAPI.Models
{
    public class Agent : EntityInSpace
    {
        public AgentStatus Status { get; set; } = AgentStatus.Dormant;
    }
}
