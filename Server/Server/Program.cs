using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ClearDatabase();
            Server server = new Server("192.168.1.6", 8888);
        }

        static public void ClearDatabase()
        {
            string connectionStr = @"Data Source=ALINA-PC\SQLEXPRESS;Initial Catalog=master;" +
          "Integrated Security=SSPI;Pooling=False";
            SqlConnection dbConnection = new SqlConnection(connectionStr);
            dbConnection.Open();

            string sqlQuery = "DELETE FROM Invitation WHERE Invitation_ID > 0";
            SqlCommand command = new SqlCommand(sqlQuery, dbConnection);
            command.ExecuteNonQuery();

            sqlQuery = "DELETE FROM Player WHERE Player_ID > 0";
            command = new SqlCommand(sqlQuery, dbConnection);
            command.ExecuteNonQuery();

            dbConnection.Close();
        }
    }
}
