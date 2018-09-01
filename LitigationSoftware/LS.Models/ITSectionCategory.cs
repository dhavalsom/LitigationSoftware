namespace LS.Models
{
    public class ITSectionCategory : BaseEntity
    {
        public string Description { get; set; }
        public bool IsDefault { get; set; }
    }

    public class ITSectionCategoryResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
