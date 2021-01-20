using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repositories
{
    public class BaseRepo : IDisposable
    {
        internal readonly SQLiteConnection sqliteConnection;

        public BaseRepo(string ConnectionString)
        {
            sqliteConnection = new SQLiteConnection(ConnectionString);
            sqliteConnection.Open();
            sqliteConnection.Close();
        }

        public void Dispose()
        {
            if (sqliteConnection != null)
            {
                sqliteConnection.Dispose();
            }
        }
    }
}
