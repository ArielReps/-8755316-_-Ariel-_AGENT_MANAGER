using FinalProjectAPI.Models.BaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectAPI.Models
{
    public class Mission
    {
        public int Id { get; set; }
        public Agent Agent { get; set; }
        public int AgentId { get; set; }
        public Target Target { get; set; }
        public int TargetId { get; set; }
        public MissionStatus Status { get; set; } = MissionStatus.Offerd;
        
        public DateTime StartDate { get; set; }
        public DateTime ExecutionDate { get; set; }
        
        [NotMapped]
        public TimeSpan EstimatedTime { get; set; }
    }
}
