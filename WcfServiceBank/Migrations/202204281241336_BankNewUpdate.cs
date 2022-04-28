namespace WcfServiceBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BankNewUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TransactionHistories", "SenderAccountNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.TransactionHistories", "ReceiverAccountNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TransactionHistories", "ReceiverAccountNumber", c => c.String());
            AlterColumn("dbo.TransactionHistories", "SenderAccountNumber", c => c.String());
        }
    }
}
