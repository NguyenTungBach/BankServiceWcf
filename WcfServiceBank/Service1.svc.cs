using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfServiceBank.Model;

namespace WcfServiceBank
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        BankContext db;

        public Service1()
        {
            db = new BankContext();
        }

        public Account CreateAccount(Account account)
        {
            db.Accounts.AddOrUpdate(account);
            db.SaveChanges();
            return account;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public List<Account> ListAccount()
        {
            return db.Accounts.ToList();
        }

        public Account Login(string UserName, string Password)
        {
            var account = db.Accounts.Where(s => s.UserName.Equals(UserName) && s.Password.Equals(Password)).FirstOrDefault() ;
            return account;
        }

        public TransactionHistory Transfer(int SenderAccountNumber, int ReceiverAccountNumber, double Amount, string Token)
        {
            var accountSender = CheckValid(SenderAccountNumber);
            var accountReceiver = CheckValid(ReceiverAccountNumber);
            if (accountSender != null && accountReceiver != null && CheckBalance(accountSender, Amount))
            {
                if (!CheckToken(accountSender, Token))
                {
                    return null;
                }

                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // Tất cả bắt đầu từ đây
                        // Account
                        accountSender.Balance = accountSender.Balance - Amount;
                        db.Accounts.AddOrUpdate(accountSender);
                        accountReceiver.Balance = accountReceiver.Balance + Amount;
                        db.Accounts.AddOrUpdate(accountReceiver);

                        TransactionHistory transactionHistory = new TransactionHistory {
                            SenderAccountNumber = SenderAccountNumber,
                            ReceiverAccountNumber = ReceiverAccountNumber,
                            Type = 3,
                            Amount = Amount
                        };
                        db.TransactionHistories.AddOrUpdate(transactionHistory);
                        db.SaveChanges();
                        transaction.Commit();
                        return transactionHistory;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Error occurred.");
                    }
                }

            }
            return null;
        }

        private bool CheckBalance(Account account, double amount)
        {
            double checkMoney = account.Balance - amount;
            if (checkMoney < 0)
            {
                return false;
            }
            return true;
        }

        private Account CheckValid(int id)
        {
            var account = db.Accounts.Find(id);
            if (account == null)
            {
                return null;
            }
            return account;
        }

        // Rút tiền
        public TransactionHistory Withdraw(int AccountNumber, double Amount, string Token)
        {
            var account = CheckValid(AccountNumber);
            
            if (account != null && CheckBalance(account, Amount))
            {
                if (!CheckToken(account, Token))
                {
                    return null;
                }

                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // Tất cả bắt đầu từ đây
                        // Account
                        account.Balance = account.Balance - Amount;
                        db.Accounts.AddOrUpdate(account);
                        
                        TransactionHistory transactionHistory = new TransactionHistory {
                            SenderAccountNumber = AccountNumber,
                            ReceiverAccountNumber = AccountNumber,
                            Type = 2,
                            Amount = Amount
                        };
                        db.TransactionHistories.AddOrUpdate(transactionHistory);
                        db.SaveChanges();
                        transaction.Commit();
                        return transactionHistory;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Error occurred.");
                    }
                }

            }
            return null;
        }

        private bool CheckToken(Account account, string token)
        {
            if (account.Token.Equals(token))
            {
                return true;
            }
            return false;
        }

        // Gửi tiền
        public TransactionHistory Deposit(int AccountNumber, double Amount, string Token)
        {
            var account = CheckValid(AccountNumber);

            if (account != null && CheckBalance(account, Amount))
            {
                if (!CheckToken(account, Token))
                {
                    return null;
                }

                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // Tất cả bắt đầu từ đây
                        // Account
                        account.Balance = account.Balance + Amount;
                        db.Accounts.AddOrUpdate(account);

                        TransactionHistory transactionHistory = new TransactionHistory
                        {
                            SenderAccountNumber = AccountNumber,
                            ReceiverAccountNumber = AccountNumber,
                            Type = 1,
                            Amount = Amount
                        };
                        db.TransactionHistories.AddOrUpdate(transactionHistory);
                        db.SaveChanges();
                        transaction.Commit();
                        return transactionHistory;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Error occurred.");
                    }
                }

            }
            return null;
        }

        public Account FindAccount(int id)
        {
            return db.Accounts.Find(id);
        }
    }
}
