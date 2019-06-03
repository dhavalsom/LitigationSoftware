using LS.Models;

namespace LS.DAL.Interface
{
    public interface IReportDataAccess
    {
        ABCReportResponse GetABCReportData(int companyId, bool? isAllowance);
    }
}
