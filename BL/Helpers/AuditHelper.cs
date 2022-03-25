using BD.Models;
using BL.Audit;
using BL.Enums;

namespace BL.Helpers
{
    public class AuditHelper
    {
        public static bool AuditEnabled() /*Podemos modificarlo de ser necesario hacer algo dinamico*/
        {
            return true;
        }
        
        public static void logEvent(OrsnaDatabaseContext db, AuditEventTypeEnum eventType, string controller, 
            string method, string message, string details,string oldValue,string newValue,string userId)
        {
            BLAudit.logEvent(db, eventType, controller,method, message, details, oldValue, newValue, userId);
        }
    }
}
