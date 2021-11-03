using System;
using BankingApplication.Database;
using BankingApplication.Models;
namespace BankingApplication.Consl
{
    class Program
    {
        internal static void Main()
        {
            Common common = new Common();
            while (true)
            {
                Console.WriteLine("---------Welcome To ABC Banking Service--------");
                Credentials userCredentials = common.GetCredentials();
                switch(userCredentials.role)
                {
                    case Role.Staff:
                        if (common.Validate(userCredentials)) 
                        {
                            var staffActions = new StaffActions(userCredentials);
                            staffActions.StaffActivities();
                        }
                        break;
                    case Role.User:
                        if(common.Validate(userCredentials))
                        {
                            var userActions = new UserActions(userCredentials);
                            userActions.UserActivities();
                        }
                        break;
                    case Role.Exit:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please Choose from above options only");
                        break;


                }
            }
        }
    }
}
