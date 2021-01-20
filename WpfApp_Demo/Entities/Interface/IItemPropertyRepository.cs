using System;
using System.Collections.Generic;
using System.Text;
using Entities.Model;

namespace Entities.Interface
{
    public interface IItemPropertyRepository : IDisposable
    {
        IEnumerable<ItemProperty> GetItems(int ListID);
        ItemProperty GetItemByID(int ItemID, int ListID);
        void InsertItem(ItemProperty ItemProperty);
        void UpdateItem(ItemProperty ItemProperty);
        void DeleteItemByID(int ItemID, int ListID);
        void DeleteItemByListID(int ListID);
    }
}
