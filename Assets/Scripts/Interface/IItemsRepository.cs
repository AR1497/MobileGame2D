using System.Collections.Generic;

public interface IItemsRepository
{
    IReadOnlyDictionary<int, IItem> Items { get; }
}
