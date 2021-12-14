using BankingApplication.Data;
namespace BankingApplication.Services
{
    public class AccountService
    {
        public BankDataBaseContext dbContext { get; set; }
       

        public AccountService()
        {
            this.dbContext = new BankDataBaseContext();
            
        }
        public bool Deposit(string accNumber, decimal amount, string currencyName)
        {
            var account = FetchAccount(accNumber);
            Data.Currency currencyObj = FetchCurrency(account.bankId, currencyName);
            account.balance += (amount * currencyObj.exchangeRate);
            Data.Transaction transaction = GenerateTransaction(account, account, currencyObj, amount, "1");
            dbContext.transactions.Add(transaction);
            dbContext.SaveChanges();
            return true;

        }

        private Data.Transaction? GenerateTransaction(Models.Account sendAccount, Models.Account recAccount, Data.Currency currency, decimal amount, string type)
        {
            Data.Transaction transaction = new Data.Transaction();
            transaction.receiveraccountId = recAccount.accountId;
            transaction.senderaccountId = sendAccount.accountId;
            transaction.transid = $"TXN {sendAccount.bankId} {DateTime.Now.ToString("yyyyMMddhhmmss")}";
            transaction.transactionAmount = amount;
            transaction.type = type;
            transaction.transactionOn = DateTime.Today;
            transaction.currency = currency.name;
            transaction.currency1 = currency;
            return transaction;
        }

        public bool Withdraw(string accNumber, decimal amount, string currencyName)
        {
            var account = FetchAccount(accNumber);
            Data.Currency currencyObj = FetchCurrency(account.bankId, currencyName);
            if (account.balance >= amount * currencyObj.exchangeRate)
            {
                account.balance -= amount * currencyObj.exchangeRate;
                Data.Transaction  transaction = GenerateTransaction(account, account, currencyObj, amount, "2");
                dbContext.transactions.Add(transaction);
                dbContext.SaveChanges();
                return true;
            }
            throw new InsufficientAmountException();
        }
        private Models.Account FetchAccount(string accNumber)
        {
            Data.Account account = (from acc in dbContext.accounts where acc.accountNumber == accNumber && acc.status == "Active" select acc).FirstOrDefault();
            if (account != null)
            {
                Models.Account account1 = new Models.Account();
                account1.bankId = account.bankId;
                account1.accountNumber = account.accountNumber;
                account1.balance = account.balance;
                account1.accountId = account.accountId;
                return account1;
            }
            else
                throw new AccountDoesntExistException();
        }
        private Data.Currency FetchCurrency(string bankId, string currencyName)
        {
             Data.Currency currencyObj = (from currency in dbContext.currencies where currency.bankid == bankId && currency.name == currencyName.ToUpper() select currency).FirstOrDefault();
            if (currencyObj == null)
                throw new CurrencyNotSupportedException();
            else
            {
                
                return currencyObj;

            }
               
        }
        public List<Data.Transaction> Displaytransactions(string accNumber)
        {
            var  account = FetchAccount(accNumber);
            List<Data.Transaction> transactions = (from transaction in dbContext.transactions where transaction.receiveraccountId == account.accountId || transaction.senderaccountId == account.accountId select transaction).ToList();
            return transactions;
        }
        public bool ChangePassword(string accNumber, string oldpassword, string newPassword)
        {
            var account = FetchAccount(accNumber);
            Data.BankUser bankuser = (from user in dbContext.bankusers where user.id == account.userId && user.password == oldpassword select user).SingleOrDefault();
            if (bankuser == null)
                return false;
            else
            {
                bankuser.password = newPassword;
                dbContext.SaveChanges();
            }
            return true;
        }
        public bool TransferAmount(string sendAccNumber, string recAccNumber, decimal amount, string currencyName, string mode)
        {
            decimal charges = 0;
            Models.Account senderAccount = FetchAccount(sendAccNumber);
            Models.Account receiverAccount = FetchAccount(recAccNumber);
            Data.Bank senderBank = (from bank in dbContext.banks where bank.id == senderAccount.bankId select bank).SingleOrDefault();
            Data.Currency currencyObj = FetchCurrency(senderAccount.bankId, currencyName);
            if (senderAccount.bankId == receiverAccount.bankId)
            {
                if (mode == "1")
                    charges = (from charge in dbContext.serviceCharges where charge.bankId == senderAccount.bankId && charge.name == "SelfRTGS" select charge.value).SingleOrDefault();
                if (mode == "2")
                    charges = (from charge in dbContext.serviceCharges where charge.bankId == senderAccount.bankId && charge.name == "SelfIMPS" select charge.value).SingleOrDefault();

            }
            else
            {
                if (mode == "1")
                    charges = (from charge in dbContext.serviceCharges where charge.bankId == senderAccount.bankId && charge.name == "OtherRTGS" select charge.value).SingleOrDefault();
                if (mode == "2")
                    charges = (from charge in dbContext.serviceCharges where charge.bankId == senderAccount.bankId && charge.name == "OtherIMPS" select charge.value).SingleOrDefault();


            }
            if (senderAccount.balance >= amount)
            {
                senderAccount.balance -= amount;
                receiverAccount.balance += amount;
                senderBank.balance -= charges;
                Data.Transaction  transaction = GenerateTransaction(senderAccount, receiverAccount, currencyObj, amount, "3");
                dbContext.transactions.Add(transaction);
                dbContext.SaveChanges();
                return true;

            }
            else
            {
                throw new InsufficientAmountException();
            }
            
        }


    }
}
