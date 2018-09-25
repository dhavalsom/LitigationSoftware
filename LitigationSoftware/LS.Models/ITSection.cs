namespace LS.Models
{
    public class ITSection : BaseEntity
    {
        public string Description { get; set; }
        public bool IsDefault { get; set; }
        public bool IsReturn { get; set; }
        public int? SectionCategoryId { get; set; }

        public bool ShouldSerializeSectionCategoryId()
        {
            return SectionCategoryId.HasValue;
        }
    }

    public class ITSectionResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
