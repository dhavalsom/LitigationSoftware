using LS.Models;
using System;

namespace LS.BL.Interface
{
    public interface IITReturnDetailsBL : IDisposable
    {
        ITReturnComplexAPIModelResponse InsertorUpdateITReturnDetails(ITReturnComplexAPIModel itReturnDetails);
    }
}
