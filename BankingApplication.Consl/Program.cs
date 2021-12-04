using System;
using System.Linq;


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
                if (common.Validate(userCredentials))
                {
                    switch (userCredentials.role)
                    {
                        case Role.Staff:
                            var staffActions = new StaffActions(userCredentials);
                            staffActions.StaffActivities();
                            break;
                        case Role.User:
                            //var userActions = new UserActions(userCredentials);
                            //userActions.UserActivities();
                            break;
                        case Role.Exit:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Please Choose from above options only");
                            break;
                    }
                }
                else
                    Console.WriteLine("------------Invalid Crdentials---------------");


            }
        }
    }
}
