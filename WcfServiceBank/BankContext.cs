using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WcfServiceBank.Model;

namespace WcfServiceBank
{
    public class BankContext : DbContext
    {
        public BankContext() : base("name=BankMVCString")
        {
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
    }
}