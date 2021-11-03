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
                Console.WriteLine("Enter Role:\n1.Staff\n2.User\n3.Exit");
                Console.WriteLine("-----------------------------------------------");
                switch ((Role)Convert.ToInt32(Console.ReadLine()))
                {
                    case Role.Staff:
                        var bankOfEmployee = common.GetBank(userCredentials);
                        if(bankOfEmployee!=null)
                            new StaffActions(bankOfEmployee);
                        else
                            Console.WriteLine("-------Invalid Credentials----------");
                        break;
                    case Role.User:
                        Account account = common.GetActiveUserAccount(userCredentials);
                        if (account != null)
                            new UserActions(account);
                        else
                          Console.WriteLine("-------------Invalid Crdentials---------");
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
