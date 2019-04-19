using System;

namespace LS.DAL.Interface
{
    public interface IDataAccessBase
    {
        void LogError(Exception error);
    }
}
