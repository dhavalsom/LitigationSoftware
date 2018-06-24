namespace LS.Models
{
    public class ITSection : BaseEntity
    {
        public string Description { get; set; }
        public bool IsDefault { get; set; }
    }

    public class ITSectionResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
