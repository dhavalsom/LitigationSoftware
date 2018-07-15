namespace LS.Models
{
    public class UserLogin : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        #region Display Properties
        public string Message { get; set; }
        #endregion
    }
}
