namespace FinalProjectWEB.Models.AuxiliaryModels
{
    public class MissionPK
    {
        public MissionPK() { }
        public MissionPK(int Aid, int Tid)
        {
            AgentId = Aid;
            TargetId = Tid;
        }
        public int AgentId { get; set; }
        public int TargetId { get; set; }
    }
}
