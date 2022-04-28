namespace WcfServiceBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BankNew : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountNumber = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Balance = c.Double(nullable: false),
                        Status = c.Int(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                        DeleteAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AccountNumber);
            
            CreateTable(
                "dbo.TransactionHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        SenderAccountNumber = c.String(),
                        ReceiverAccountNumber = c.String(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TransactionHistories");
            DropTable("dbo.Accounts");
        }
    }
}
