
using BankingApplication.Models;

namespace BankingApplication.Consl

{
    internal class Common
    {

        BankDataBaseContext dbContext = new BankDataBaseContext();
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


            if (userCredentials.role.Equals(Role.Staff))
            {
                var employees = (from emp in dbContext.employees
                                 join user in dbContext.bankusers on emp.userId equals user.id
                                 where user.username.Equals(userCredentials.UserName) && user.password.Equals(userCredentials.Password)
                                 select new
                                 {

                                 }).ToList();

                if (employees.Count == 1)
                    return true;
            }
            else if (userCredentials.role.Equals(Role.User))
            {

                var users = dbContext.bankusers.ToList();
                if (users.Any(e => e.username.Equals(userCredentials.UserName) && e.password.Equals(userCredentials.Password)))
                    return true;
            }
            else if (userCredentials.role.Equals(Role.Exit))
                Environment.Exit(0);
            return false;




        }
    }
}

