namespace LS.Models
{
    public class ComplianceMaster : BaseEntity
    {
        public int SrNo { get; set; }
        public string Description { get; set; }
    }

    public class ComplianceMasterResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
