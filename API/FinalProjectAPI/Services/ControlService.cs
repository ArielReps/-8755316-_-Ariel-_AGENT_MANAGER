using FinalProjectAPI.Models;
using FinalProjectAPI.Models.AuxiliaryModels;
using FinalProjectAPI.Services.IServices;
using System.Drawing;

namespace FinalProjectAPI.Services
{
    public class ControlService : IControlService
    {
        public List<Mission> MissionOffers { get; set; } = new List<Mission>();
        public RecDirection DirectAgent(Mission mission)
        {
            int x = 0;
            int y = 0;
            if (mission.Agent.LocationX < mission.Target.LocationX) x = 1;
            else if (mission.Agent.LocationX > mission.Target.LocationX) x = -1;
            if (mission.Agent.LocationY < mission.Target.LocationY) y = 1;
            else if (mission.Agent.LocationY > mission.Target.LocationY) y = -1;
            RecDirection direction = new RecDirection(x, y);
            return direction;
        }

        public double Distance(Point a, Point t)
        {
            return Math.Sqrt(Math.Pow(t.X - a.X, 2) + Math.Pow(t.Y - a.Y, 2));
        }

        public Dictionary<Agent, Target> GetSuitabilities(List<Agent> agents, List<Target> targets, List<Mission> missions)
        {
            foreach (var item in missions)
            {
                agents.Remove(item.Agent);
                targets.Remove(item.Target);
            }
            // מוודא שיש לפחות אחד מכל אחד
            if (agents.Count == 0 || targets.Count == 0) return new Dictionary<Agent, Target>();
            Dictionary<Agent, Target> matches = new Dictionary<Agent, Target>();
            foreach (Agent agent in agents)
            {
                Target nearest = null;
                double distance = 0;
                foreach (Target target in targets)
                {
                    if (nearest == null)
                    {
                        nearest = target;
                        distance = Distance(agent.Location, target.Location);
                        continue;
                    }
                    double other = Distance(agent.Location, target.Location);
                    if (other < distance)
                    {
                        nearest = target;
                        distance = other;
                    }
                }
                if (distance <= 200)
                {
                    matches.Add(agent, nearest!);
                }
            }
            return matches;
        }

        // מאתחל את ההצעות ומסדר מחדש
        public void SetOffers(Dictionary<Agent, Target> offers)
        {
            MissionOffers.Clear();
            foreach (var pair in offers)
            {
                MissionOffers.Add(new()
                {
                    Agent = pair.Key,
                    Target = pair.Value,
                    AgentId = pair.Key.Id,
                    TargetId = pair.Value.Id
                });
            }
        }

        public TimeSpan GetTime(double distance)
        {
            throw new NotImplementedException();
        }

        public List<Mission> GetMissionOffers()
        {
            return MissionOffers;
        }
    }
}
