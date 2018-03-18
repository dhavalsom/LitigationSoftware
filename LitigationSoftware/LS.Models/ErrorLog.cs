namespace LS.Models
{
    public class ErrorLog : BaseEntity
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string ExceptionType { get; set; }
        public string Source { get; set; }
    }
}
