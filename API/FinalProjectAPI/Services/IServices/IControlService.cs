using FinalProjectAPI.Models;
using FinalProjectAPI.Models.AuxiliaryModels;
using System.Drawing;

namespace FinalProjectAPI.Services.IServices
{
    public interface IControlService
    {
        List<Mission> GetMissionOffers();
        void SetOffers(Dictionary<Agent, Target> offers);
        Dictionary<Agent, Target> GetSuitabilities(List<Agent> agents, List<Target> targets, List<Mission> missions);
        double Distance(Point agentPoint, Point targetPoint);
        RecDirection DirectAgent(Mission mission);
        TimeSpan GetTime(double distance);
        // סקופט שמוזרק לקונטרולרים ומוזרק לו הדאטה בייס
        // מה צריך חוץ מקראד
        // צריך לשמור מערך סטטי שיתעדכן מדי פעם
        // פונקציה סטטית שמחזירה את המטרה הכי קרובה לכל סוכן
        // פונקציה שמוסיפה למערך את כל ההצעות
        // לחשב כיוון לסוכן ולהזיז בהתאם
        // חישוב זמן
    }
}
