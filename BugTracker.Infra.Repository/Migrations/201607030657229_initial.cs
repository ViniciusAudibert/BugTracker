namespace BugTracker.Infra.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserActivation",
                c => new
                    {
                        IDUserActivation = c.Int(nullable: false, identity: true),
                        HashCode = c.String(maxLength: 100, unicode: false),
                        RequestDate = c.DateTime(nullable: false),
                        IDUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IDUserActivation)
                .ForeignKey("dbo.User", t => t.IDUser)
                .Index(t => t.IDUser);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        IDUser = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        Password = c.String(maxLength: 100, unicode: false),
                        Image = c.String(maxLength: 100, unicode: false),
                        HashCode = c.String(maxLength: 100, unicode: false),
                        Active = c.Boolean(nullable: false),
                        AccountConfirmed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IDUser);
            
            CreateTable(
                "dbo.Application",
                c => new
                    {
                        IDApplication = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 100, unicode: false),
                        Description = c.String(maxLength: 100, unicode: false),
                        Url = c.String(maxLength: 100, unicode: false),
                        Active = c.Boolean(nullable: false),
                        Image = c.String(maxLength: 100, unicode: false),
                        SpecialTag = c.String(maxLength: 100, unicode: false),
                        IDUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IDApplication)
                .ForeignKey("dbo.User", t => t.IDUser)
                .Index(t => t.IDUser);
            
            CreateTable(
                "dbo.BugTracker",
                c => new
                    {
                        IDBugTracker = c.Int(nullable: false, identity: true),
                        IDApplication = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Description = c.String(maxLength: 100, unicode: false),
                        OccurredDate = c.DateTime(nullable: false),
                        BrowserVersion = c.String(maxLength: 100, unicode: false),
                        BrowserName = c.String(maxLength: 100, unicode: false),
                        PlatformName = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.IDBugTracker)
                .ForeignKey("dbo.Application", t => t.IDApplication)
                .Index(t => t.IDApplication);
            
            CreateTable(
                "dbo.BugTrackerTag",
                c => new
                    {
                        IDBugTrackerTag = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100, unicode: false),
                        IDBugTracker = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IDBugTrackerTag)
                .ForeignKey("dbo.BugTracker", t => t.IDBugTracker)
                .Index(t => t.IDBugTracker);
            
            CreateTable(
                "dbo.UserForgotPassword",
                c => new
                    {
                        IDUserForgotPassword = c.Int(nullable: false, identity: true),
                        IDUser = c.Int(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                        HashCode = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.IDUserForgotPassword)
                .ForeignKey("dbo.User", t => t.IDUser)
                .Index(t => t.IDUser);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserActivation", "IDUser", "dbo.User");
            DropForeignKey("dbo.UserForgotPassword", "IDUser", "dbo.User");
            DropForeignKey("dbo.Application", "IDUser", "dbo.User");
            DropForeignKey("dbo.BugTrackerTag", "IDBugTracker", "dbo.BugTracker");
            DropForeignKey("dbo.BugTracker", "IDApplication", "dbo.Application");
            DropIndex("dbo.UserForgotPassword", new[] { "IDUser" });
            DropIndex("dbo.BugTrackerTag", new[] { "IDBugTracker" });
            DropIndex("dbo.BugTracker", new[] { "IDApplication" });
            DropIndex("dbo.Application", new[] { "IDUser" });
            DropIndex("dbo.UserActivation", new[] { "IDUser" });
            DropTable("dbo.UserForgotPassword");
            DropTable("dbo.BugTrackerTag");
            DropTable("dbo.BugTracker");
            DropTable("dbo.Application");
            DropTable("dbo.User");
            DropTable("dbo.UserActivation");
        }
    }
}
