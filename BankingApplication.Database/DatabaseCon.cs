using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data.SqlClient;


namespace BankingApplication.Database
{
    public class DatabaseCon
    {
        public void ConnectionToDatabase()
        {
            string ConnectionString = "Server=localhost\\SQLEXPRESS;Database=BankData;Trusted_Connection=True;";
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from bank", con);
            SqlDataReader rdr  = cmd.ExecuteReader();    
            while (rdr.Read())
            {
                Console.WriteLine(rdr["id"]);

            }
            con.Close();

            
            
        }
        
    }
}
