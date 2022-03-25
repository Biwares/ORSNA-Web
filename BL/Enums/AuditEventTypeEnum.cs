using System.ComponentModel;

namespace BL.Enums
{
    public enum AuditEventTypeEnum
    {
        [Description("Login")]
        LOGIN,
        [Description("Modificacion")]
        MODIFICACION,
        [Description("Alta")]
        ALTA,
        [Description("Baja")]
        BAJA,
        [Description("Consulta")]
        CONSULTA,
    }
}
