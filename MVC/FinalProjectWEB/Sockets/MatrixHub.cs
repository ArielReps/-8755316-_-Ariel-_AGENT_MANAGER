using Microsoft.AspNetCore.SignalR;

namespace FinalProjectWEB.Sockets
{
    public class MatrixHub : Hub
    {
        // לעשות סה"כ לולאה שרצה כל הזמן מרגע שנהיה חיבור ושולחת כל כמה שניות רשימות מעודכנות
        // לעשות שזה יהיה טאסק שאפשר לבטל ע"י הפונקציה שמופעלת ברגע הניתוק
    }
}
