using Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Interface
{
    public interface IListPropertyRepository 
    {
        IEnumerable<ListProperty> GetLists();

        void InsertList(ListProperty ListProperty);

        void Save();
        void DeleteList(int ListID);
        void UpdateList(ListProperty ListProperty);
    }
}
