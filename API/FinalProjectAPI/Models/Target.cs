using FinalProjectAPI.Models.BaseModels;

namespace FinalProjectAPI.Models
{
    public class Target : EntityInSpace
    {
        public string Role { get; set; }
        public TargetStatus Status { get; set; } = TargetStatus.Living;
    }
}
