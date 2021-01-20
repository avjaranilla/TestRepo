using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;
using Entities.Interface;
using Entities.Model;

namespace Infra.Repositories
{
    public class ItemPropertyRepository : BaseRepo, IItemPropertyRepository
    {
        public ItemPropertyRepository(string ConnectionString) : base(ConnectionString)
        {
        }

        public IEnumerable<ItemProperty> GetItems(int ListID)
        {
            string query = "SELECT ItemID, ListID, ItemName, ItemDesc, ItemStatus FROM ItemProperty WHERE ListID = @ListID";

            var sqlCommand = new SQLiteCommand(query, sqliteConnection);
            sqlCommand.Parameters.AddWithValue("@ListID", ListID);

            using (var adapater = new SQLiteDataAdapter(sqlCommand))
            {
                DataTable dataTable = new DataTable();
                adapater.Fill(dataTable);

                var list = new List<ItemProperty>();

                foreach (DataRow dr in dataTable.Rows)
                {
                    list.Add(new ItemProperty
                    {
                        ListID = Convert.ToInt32(dr["ListID"]),
                        ItemID = Convert.ToInt32(dr["ItemID"]),
                        ItemName = dr["ItemName"].ToString(),
                        ItemDesc = dr["ItemDesc"].ToString(),
                        ItemStatus = Convert.ToInt32(dr["ItemStatus"])
                    });
                }
                return list;
            }
        }

        public void InsertItem(ItemProperty ItemProperty)
        {
            ItemProperty itemProperty = new ItemProperty();
            itemProperty.ListID = ItemProperty.ListID;
            itemProperty.ItemName = ItemProperty.ItemName;
            itemProperty.ItemDesc = ItemProperty.ItemDesc;
            itemProperty.ItemStatus = ItemProperty.ItemStatus;

            string query = "INSERT INTO ItemProperty(ListID, ItemName, ItemDesc, ItemStatus) VALUES (@ListID, @ItemName, @ItemDesc, @ItemStatus)";
            var con = new SQLiteConnection(sqliteConnection);
            con.Open();
            var sqlCommand = new SQLiteCommand(query, con);
            sqlCommand.Parameters.AddWithValue("@ListID", itemProperty.ListID);
            sqlCommand.Parameters.AddWithValue("@ItemName", itemProperty.ItemName);
            sqlCommand.Parameters.AddWithValue("@ItemDesc", itemProperty.ItemDesc);
            sqlCommand.Parameters.AddWithValue("@ItemStatus", itemProperty.ItemStatus);
            sqlCommand.ExecuteNonQuery();
            con.Close();
        }

        public void UpdateItem(ItemProperty ItemProperty)
        {
            ItemProperty itemProperty = new ItemProperty();
            itemProperty.ItemID = ItemProperty.ItemID;
            itemProperty.ListID = ItemProperty.ListID;
            itemProperty.ItemName = ItemProperty.ItemName;
            itemProperty.ItemDesc = ItemProperty.ItemDesc;
            itemProperty.ItemStatus = ItemProperty.ItemStatus;

            string query = "UPDATE ItemProperty SET ItemName = @ItemName, ItemDesc = @ItemDesc, ItemStatus = @ItemStatus WHERE ItemID = @ItemID AND ListID = @ListID";
            var con = new SQLiteConnection(sqliteConnection);
            con.Open();
            var sqlCommand = new SQLiteCommand(query, con);
            sqlCommand.Parameters.AddWithValue("@ItemID", itemProperty.ItemID);
            sqlCommand.Parameters.AddWithValue("@ListID", itemProperty.ListID);
            sqlCommand.Parameters.AddWithValue("@ItemName", itemProperty.ItemName);
            sqlCommand.Parameters.AddWithValue("@ItemDesc", itemProperty.ItemDesc);
            sqlCommand.Parameters.AddWithValue("@ItemStatus", itemProperty.ItemStatus);
            sqlCommand.ExecuteNonQuery();
            con.Close();
        }

        public void DeleteItemByID(int ItemID, int ListID)
        {
            int itemID = ItemID;
            int listID = ListID;

            string query = "DELETE FROM ItemProperty WHERE ItemID = @ItemID AND ListID = @ListID";
            var con = new SQLiteConnection(sqliteConnection);
            con.Open();
            var sqlCommand = new SQLiteCommand(query, con);
            sqlCommand.Parameters.AddWithValue("@ItemID", itemID);
            sqlCommand.Parameters.AddWithValue("@ListID", listID);
            sqlCommand.ExecuteNonQuery();
            con.Close();
        }

        public void DeleteItemByListID(int ListID)
        {
            int listID = ListID;

            string query = "DELETE FROM ItemProperty WHERE ListID = @ListID";
            var con = new SQLiteConnection(sqliteConnection);
            con.Open();
            var sqlCommand = new SQLiteCommand(query, con);
            sqlCommand.Parameters.AddWithValue("@ListID", listID);
            sqlCommand.ExecuteNonQuery();
            con.Close();
        }

        public ItemProperty GetItemByID(int ItemID, int ListID)
        {
            throw new NotImplementedException();
        }


    }

}
