using System.Collections.Generic;
namespace Company.Project.Features
{
    public interface IRepository<TKey, TValue>
    {
        IReadOnlyDictionary<TKey, TValue> Collection { get; }
    }
}
