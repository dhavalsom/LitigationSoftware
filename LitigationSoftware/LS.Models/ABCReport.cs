using System.Collections.Generic;

namespace LS.Models
{
    public class ABCReport : BaseEntity
    {
        public int ITSubHeadId { get; set; }
        public string ITSubHead { get; set; }
        public int ITHeadId { get; set; }
        public string ITHead { get; set; }
        public string SubHeadType { get; set; }
        public bool IsAllowance { get; set; }
        public decimal Total { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }

    public class ABCReportResponse
    {
        public List<ABCReport> ABCReportData { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}