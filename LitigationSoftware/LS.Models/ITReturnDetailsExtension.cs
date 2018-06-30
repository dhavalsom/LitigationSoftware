namespace LS.Models
{
    public class ITReturnDetailsExtension : BaseEntity
    {
        public int ITReturnDetailsId { get; set; }
        public int ITSubHeadId { get; set; }
        public decimal? ITSubHeadValue { get; set; }
    }
}
