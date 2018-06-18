using LS.Models;
using System;

namespace LS.BL.Interface
{
    public interface IITReturnDetailsBL : IDisposable
    {
        ITReturnDetailsResponse InsertorUpdateITReturnDetails(ITReturnDetails itReturnDetails);
    }
}
