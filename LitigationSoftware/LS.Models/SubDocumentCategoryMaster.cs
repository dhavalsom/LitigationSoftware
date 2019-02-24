namespace LS.Models
{
    public class SubDocumentCategoryMaster : BaseEntity
    {
        public int DocumentCategoryId { get; set; }
        public string Description { get; set; }

        #region Display Properties
        public string DocumentCategoryName { get; set; }
        #endregion
    }

    public class SubDocumentCategoryMasterResponse
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
