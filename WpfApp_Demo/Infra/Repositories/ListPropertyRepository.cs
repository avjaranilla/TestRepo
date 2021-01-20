using System;
using System.Collections.Generic;
using Entities.Interface;
using Entities.Model;
using System.Data.SQLite;
using System.Data;

namespace Infra.Repositories
{
    public class ListPropertyRepository : BaseRepo, IListPropertyRepository
    {
        public ListPropertyRepository(string ConnectionString) : base(ConnectionString)
        {

        }

        public void DeleteList(int ListID)
        {

            int lid = ListID;
            string query = "DELETE FROM ListProperty WHERE ListID = @ListID";
            var con = new SQLiteConnection(sqliteConnection);
            con.Open();
            var sqlCommand = new SQLiteCommand(query, con);
            sqlCommand.Parameters.AddWithValue("@ListID", lid);
            sqlCommand.ExecuteNonQuery();
            con.Close();
        }

        public IEnumerable<ListProperty> GetLists()
        {
            string query = "SELECT  * from ListProperty";

            var sqlCommand = new SQLiteCommand(query, sqliteConnection);

            using (var adapater = new SQLiteDataAdapter(sqlCommand))
            {
                DataTable dataTable = new DataTable();
                adapater.Fill(dataTable);

                var list = new List<ListProperty>();

                foreach (DataRow dr in dataTable.Rows)
                {
                    list.Add(new ListProperty
                    {
                        ListID = Convert.ToInt32(dr["ListID"]),
                        ListDesc = dr["ListDesc"].ToString(),
                        ListName = dr["ListName"].ToString()
                    });
                }
                return list;
            }
        }

        public void InsertList(ListProperty ListProperty)
        {
            ListProperty listProperty = new ListProperty();
            listProperty.ListID = ListProperty.ListID;
            listProperty.ListName = ListProperty.ListName;
            listProperty.ListDesc = ListProperty.ListDesc;

            string query = "INSERT INTO ListProperty(ListName, ListDesc) VALUES (@listName, @listDesc)";
            var con = new SQLiteConnection(sqliteConnection);
            con.Open();
            var sqlCommand = new SQLiteCommand(query, con);
            sqlCommand.Parameters.AddWithValue("@listName", listProperty.ListName);
            sqlCommand.Parameters.AddWithValue("@listDesc", listProperty.ListDesc);
            sqlCommand.ExecuteNonQuery();
            con.Close();
        }

        public void UpdateList(ListProperty ListProperty)
        {
            ListProperty listProperty = new ListProperty();
            listProperty.ListID = ListProperty.ListID;
            listProperty.ListName = ListProperty.ListName;
            listProperty.ListDesc = ListProperty.ListDesc;

            string query = "UPDATE ListProperty SET ListName = @listName, ListDesc = @listDesc WHERE ListID = @listID";
            var con = new SQLiteConnection(sqliteConnection);
            con.Open();
            var sqlCommand = new SQLiteCommand(query, con);
            sqlCommand.Parameters.AddWithValue("@listName", listProperty.ListName);
            sqlCommand.Parameters.AddWithValue("@listDesc", listProperty.ListDesc);
            sqlCommand.Parameters.AddWithValue("@listID", listProperty.ListID);
            sqlCommand.ExecuteNonQuery();
            con.Close();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }


    }
}
