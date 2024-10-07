using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HMDDistanceReader.Databases.Interfaces
{
    public interface ILiteDbReader
    {
        string ErrorMessage { get; }
        bool IsConnected { get; }
        IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate, int skip = 0, int limit = int.MaxValue);
    }
}
