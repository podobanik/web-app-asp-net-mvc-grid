namespace WebAppAspNetMvcIdentity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AvailableDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        Birthday = c.DateTime(),
                        Gender = c.Int(nullable: false),
                        Reviews = c.String(),
                        IsArchive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Citizenships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClientTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Guid = c.Guid(nullable: false),
                        Data = c.Binary(nullable: false),
                        ContentType = c.String(maxLength: 100),
                        DateChanged = c.DateTime(),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Procedure = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LogHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartImport = c.DateTime(nullable: false),
                        EndImport = c.DateTime(nullable: false),
                        SuccessCount = c.Int(nullable: false),
                        FailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Value = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Type, unique: true);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ClientAvailableDocuments",
                c => new
                    {
                        Client_Id = c.Int(nullable: false),
                        AvailableDocument_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Client_Id, t.AvailableDocument_Id })
                .ForeignKey("dbo.Clients", t => t.Client_Id, cascadeDelete: true)
                .ForeignKey("dbo.AvailableDocuments", t => t.AvailableDocument_Id, cascadeDelete: true)
                .Index(t => t.Client_Id)
                .Index(t => t.AvailableDocument_Id);
            
            CreateTable(
                "dbo.CitizenshipClients",
                c => new
                    {
                        Citizenship_Id = c.Int(nullable: false),
                        Client_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Citizenship_Id, t.Client_Id })
                .ForeignKey("dbo.Citizenships", t => t.Citizenship_Id, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.Client_Id, cascadeDelete: true)
                .Index(t => t.Citizenship_Id)
                .Index(t => t.Client_Id);
            
            CreateTable(
                "dbo.ClientTypeClients",
                c => new
                    {
                        ClientType_Id = c.Int(nullable: false),
                        Client_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClientType_Id, t.Client_Id })
                .ForeignKey("dbo.ClientTypes", t => t.ClientType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.Client_Id, cascadeDelete: true)
                .Index(t => t.ClientType_Id)
                .Index(t => t.Client_Id);
            
            CreateTable(
                "dbo.OrderClients",
                c => new
                    {
                        Order_Id = c.Int(nullable: false),
                        Client_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_Id, t.Client_Id })
                .ForeignKey("dbo.Orders", t => t.Order_Id, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.Client_Id, cascadeDelete: true)
                .Index(t => t.Order_Id)
                .Index(t => t.Client_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OrderClients", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.OrderClients", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Documents", "Id", "dbo.Clients");
            DropForeignKey("dbo.ClientTypeClients", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.ClientTypeClients", "ClientType_Id", "dbo.ClientTypes");
            DropForeignKey("dbo.CitizenshipClients", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.CitizenshipClients", "Citizenship_Id", "dbo.Citizenships");
            DropForeignKey("dbo.ClientAvailableDocuments", "AvailableDocument_Id", "dbo.AvailableDocuments");
            DropForeignKey("dbo.ClientAvailableDocuments", "Client_Id", "dbo.Clients");
            DropIndex("dbo.OrderClients", new[] { "Client_Id" });
            DropIndex("dbo.OrderClients", new[] { "Order_Id" });
            DropIndex("dbo.ClientTypeClients", new[] { "Client_Id" });
            DropIndex("dbo.ClientTypeClients", new[] { "ClientType_Id" });
            DropIndex("dbo.CitizenshipClients", new[] { "Client_Id" });
            DropIndex("dbo.CitizenshipClients", new[] { "Citizenship_Id" });
            DropIndex("dbo.ClientAvailableDocuments", new[] { "AvailableDocument_Id" });
            DropIndex("dbo.ClientAvailableDocuments", new[] { "Client_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Settings", new[] { "Type" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Documents", new[] { "Id" });
            DropTable("dbo.OrderClients");
            DropTable("dbo.ClientTypeClients");
            DropTable("dbo.CitizenshipClients");
            DropTable("dbo.ClientAvailableDocuments");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Settings");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.LogHistories");
            DropTable("dbo.Orders");
            DropTable("dbo.Documents");
            DropTable("dbo.ClientTypes");
            DropTable("dbo.Citizenships");
            DropTable("dbo.Clients");
            DropTable("dbo.AvailableDocuments");
        }
    }
}
