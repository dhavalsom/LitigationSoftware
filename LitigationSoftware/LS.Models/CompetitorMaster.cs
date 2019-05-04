namespace LS.Models
{
    public class CompetitorMaster : BaseEntity
    {
        public int CompanyId { get; set; }
        public string Description { get; set; }

        #region Display Properties
        #endregion

        #region Serialization
        public bool ShouldSerializeDescription()
        {
            return !string.IsNullOrEmpty(Description);
        }
        #endregion
    }

    public class CompetitorResponse
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}

