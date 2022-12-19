namespace Makale_DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Begeni",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kullanici_Id = c.Int(),
                        Not_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kullanici", t => t.Kullanici_Id)
                .ForeignKey("dbo.Notlar", t => t.Not_Id)
                .Index(t => t.Kullanici_Id)
                .Index(t => t.Not_Id);
            
            CreateTable(
                "dbo.Kullanici",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(maxLength: 20),
                        Soyad = c.String(maxLength: 20),
                        KullaniciAdi = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 50),
                        Sifre = c.String(nullable: false, maxLength: 20),
                        Aktif = c.Boolean(nullable: false),
                        Admin = c.Boolean(nullable: false),
                        AktifGuid = c.Guid(nullable: false),
                        KayitTarih = c.DateTime(nullable: false),
                        DegistirmeTarihi = c.DateTime(nullable: false),
                        DegistirenKullanici = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notlar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 50),
                        Text = c.String(nullable: false, maxLength: 250),
                        Taslak = c.Boolean(nullable: false),
                        BegeniSayisi = c.Int(nullable: false),
                        KategoriId = c.Int(nullable: false),
                        KayitTarih = c.DateTime(nullable: false),
                        DegistirmeTarihi = c.DateTime(nullable: false),
                        DegistirenKullanici = c.String(nullable: false, maxLength: 20),
                        Kullanici_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kategori", t => t.KategoriId, cascadeDelete: true)
                .ForeignKey("dbo.Kullanici", t => t.Kullanici_Id)
                .Index(t => t.KategoriId)
                .Index(t => t.Kullanici_Id);
            
            CreateTable(
                "dbo.Kategori",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 50),
                        Aciklama = c.String(maxLength: 150),
                        KayitTarih = c.DateTime(nullable: false),
                        DegistirmeTarihi = c.DateTime(nullable: false),
                        DegistirenKullanici = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Yorum",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 250),
                        KayitTarih = c.DateTime(nullable: false),
                        DegistirmeTarihi = c.DateTime(nullable: false),
                        DegistirenKullanici = c.String(nullable: false, maxLength: 20),
                        Kullanici_Id = c.Int(),
                        Not_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kullanici", t => t.Kullanici_Id)
                .ForeignKey("dbo.Notlar", t => t.Not_Id)
                .Index(t => t.Kullanici_Id)
                .Index(t => t.Not_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Yorum", "Not_Id", "dbo.Notlar");
            DropForeignKey("dbo.Yorum", "Kullanici_Id", "dbo.Kullanici");
            DropForeignKey("dbo.Notlar", "Kullanici_Id", "dbo.Kullanici");
            DropForeignKey("dbo.Notlar", "KategoriId", "dbo.Kategori");
            DropForeignKey("dbo.Begeni", "Not_Id", "dbo.Notlar");
            DropForeignKey("dbo.Begeni", "Kullanici_Id", "dbo.Kullanici");
            DropIndex("dbo.Yorum", new[] { "Not_Id" });
            DropIndex("dbo.Yorum", new[] { "Kullanici_Id" });
            DropIndex("dbo.Notlar", new[] { "Kullanici_Id" });
            DropIndex("dbo.Notlar", new[] { "KategoriId" });
            DropIndex("dbo.Begeni", new[] { "Not_Id" });
            DropIndex("dbo.Begeni", new[] { "Kullanici_Id" });
            DropTable("dbo.Yorum");
            DropTable("dbo.Kategori");
            DropTable("dbo.Notlar");
            DropTable("dbo.Kullanici");
            DropTable("dbo.Begeni");
        }
    }
}
