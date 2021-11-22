using BankingApplication.Database;
using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BankingApplication.Consl
{
    internal class Common
    {
        JsonReadWrite dataReadWrite;
        internal Common()
        {
            dataReadWrite = new JsonReadWrite();
        }

        internal Credentials GetCredentials()
        {
            Credentials userCredentials = new Credentials();
            Console.WriteLine("Enter Username:");
            userCredentials.UserName = Console.ReadLine();
            Console.WriteLine("Enter Password");
            userCredentials.Password = Console.ReadLine();
            Console.WriteLine("Enter Role:\n1.Staff\n2.User\n3.Exit");
            Console.WriteLine("-----------------------------------------------");
            userCredentials.role = (Role)Convert.ToInt32(Console.ReadLine());
            return userCredentials;
        }
        internal void DisplayStatus(bool status, String message)
        {
            if (status != false)
                Console.WriteLine(message);
            else
                Console.WriteLine("Action can't be performed..Please contact your Manager");
        }

        internal bool Validate(Credentials userCredentials)
        {

            using (SqlConnection con = new SqlConnection(Constant.ConnectionString))
            {
                con.Open();
                SqlCommand cmd = null;
                if (userCredentials.role.Equals(Role.Staff))
                {
                    cmd = new SqlCommand("select u.username,u.password from  employee e join bankuser u on e.userid=u.id where u.username=@userName and u.password=@password", con);
                }
                else if (userCredentials.role.Equals(Role.User))
                {
                    cmd = new SqlCommand("select * from bankuser where username=@userName and password=@password", con);
                }
                cmd.Parameters.AddWithValue("username", userCredentials.UserName);
                cmd.Parameters.AddWithValue("password", userCredentials.Password);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                    return true;
                else
                    return false;
            }
        }
            
            
    }
}

