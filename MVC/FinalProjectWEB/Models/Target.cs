using FinalProjectWEB.Models.BaseModels;

namespace FinalProjectWEB.Models
{
    public class Target : EntityInSpace
    {
        public string Role { get; set; }
        public TargetStatus Status { get; set; } = TargetStatus.Living;
    }
}
