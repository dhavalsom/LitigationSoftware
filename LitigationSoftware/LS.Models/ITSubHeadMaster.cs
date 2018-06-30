namespace LS.Models
{
    public class ITSubHeadMaster : BaseEntity
    {
        public int ITHeadId { get; set; }
        public string Description { get; set; }
        public bool? IsAllowance { get; set; }
    }
}
