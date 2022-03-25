namespace BD.Models
{
    public partial class BeneficiarioAdjuntos : BaseAdjuntos
    {
        public int? IdBeneficiario { get; set; }
        public virtual Beneficiarios IdBeneficiarioNavigation { get; set; }
    }
}
