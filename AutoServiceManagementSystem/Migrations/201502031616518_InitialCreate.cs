namespace AutoServiceManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        Manufacturer = c.Int(),
                        Model = c.String(),
                        PlateCode = c.String(),
                        VIN = c.String(),
                        Year = c.Int(),
                        Mileage = c.Int(),
                        Displacement = c.Single(),
                        FuelType = c.Int(),
                        Owner_CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.CarId)
                .ForeignKey("dbo.Customers", t => t.Owner_CustomerId)
                .Index(t => t.Owner_CustomerId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        CurrentMileage = c.Int(nullable: false),
                        DateStarted = c.DateTime(),
                        DateFinished = c.DateTime(),
                        Finished = c.Boolean(nullable: false),
                        Paid = c.Boolean(nullable: false),
                        Diagnosis_DiagId = c.Int(),
                        Labour_LabourId = c.Int(),
                        OilChange_OilChangeId = c.Int(),
                        Car_CarId = c.Int(),
                    })
                .PrimaryKey(t => t.JobId)
                .ForeignKey("dbo.Diags", t => t.Diagnosis_DiagId)
                .ForeignKey("dbo.Labours", t => t.Labour_LabourId)
                .ForeignKey("dbo.OilChanges", t => t.OilChange_OilChangeId)
                .ForeignKey("dbo.Cars", t => t.Car_CarId)
                .Index(t => t.Diagnosis_DiagId)
                .Index(t => t.Labour_LabourId)
                .Index(t => t.OilChange_OilChangeId)
                .Index(t => t.Car_CarId);
            
            CreateTable(
                "dbo.Diags",
                c => new
                    {
                        DiagId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.DiagId);
            
            CreateTable(
                "dbo.TroubleCodes",
                c => new
                    {
                        TroubleCodeId = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                        Permanent = c.Boolean(nullable: false),
                        Diag_DiagId = c.Int(),
                    })
                .PrimaryKey(t => t.TroubleCodeId)
                .ForeignKey("dbo.Diags", t => t.Diag_DiagId)
                .Index(t => t.Diag_DiagId);
            
            CreateTable(
                "dbo.Labours",
                c => new
                    {
                        LabourId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.LabourId);
            
            CreateTable(
                "dbo.OilChanges",
                c => new
                    {
                        OilChangeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Liters = c.Single(nullable: false),
                        PricePerLiter = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OilChangeId);
            
            CreateTable(
                "dbo.SpareParts",
                c => new
                    {
                        PartId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DealerCode = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Supplier_SupplierId = c.Int(),
                        Job_JobId = c.Int(),
                    })
                .PrimaryKey(t => t.PartId)
                .ForeignKey("dbo.Suppliers", t => t.Supplier_SupplierId)
                .ForeignKey("dbo.Jobs", t => t.Job_JobId)
                .Index(t => t.Supplier_SupplierId)
                .Index(t => t.Job_JobId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        City = c.String(),
                        DiscountPercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WebsiteUrl = c.String(),
                        LogoUrl = c.String(),
                    })
                .PrimaryKey(t => t.SupplierId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        MoneyOwed = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalRevenue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalProfit = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "Owner_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Jobs", "Car_CarId", "dbo.Cars");
            DropForeignKey("dbo.SpareParts", "Job_JobId", "dbo.Jobs");
            DropForeignKey("dbo.SpareParts", "Supplier_SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Jobs", "OilChange_OilChangeId", "dbo.OilChanges");
            DropForeignKey("dbo.Jobs", "Labour_LabourId", "dbo.Labours");
            DropForeignKey("dbo.Jobs", "Diagnosis_DiagId", "dbo.Diags");
            DropForeignKey("dbo.TroubleCodes", "Diag_DiagId", "dbo.Diags");
            DropIndex("dbo.SpareParts", new[] { "Job_JobId" });
            DropIndex("dbo.SpareParts", new[] { "Supplier_SupplierId" });
            DropIndex("dbo.TroubleCodes", new[] { "Diag_DiagId" });
            DropIndex("dbo.Jobs", new[] { "Car_CarId" });
            DropIndex("dbo.Jobs", new[] { "OilChange_OilChangeId" });
            DropIndex("dbo.Jobs", new[] { "Labour_LabourId" });
            DropIndex("dbo.Jobs", new[] { "Diagnosis_DiagId" });
            DropIndex("dbo.Cars", new[] { "Owner_CustomerId" });
            DropTable("dbo.Customers");
            DropTable("dbo.Suppliers");
            DropTable("dbo.SpareParts");
            DropTable("dbo.OilChanges");
            DropTable("dbo.Labours");
            DropTable("dbo.TroubleCodes");
            DropTable("dbo.Diags");
            DropTable("dbo.Jobs");
            DropTable("dbo.Cars");
        }
    }
}
