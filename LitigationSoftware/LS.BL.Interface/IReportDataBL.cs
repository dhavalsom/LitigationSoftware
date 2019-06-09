using LS.Models;

namespace LS.BL.Interface
{
    public interface IReportDataBL
    {
        ABCReportResponse GetABCReportData(int companyId, bool? isAllowance);
    }
}
