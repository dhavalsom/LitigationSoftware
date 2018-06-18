using LS.Models;
using System.Collections.Generic;

namespace LS.DAL.Interface
{
    public interface IITReturnDetailsDataAccess
    {
        ITReturnDetailsResponse InsertorUpdateITReturnDetails(ITReturnDetails itReturnDetails);
    }
}
