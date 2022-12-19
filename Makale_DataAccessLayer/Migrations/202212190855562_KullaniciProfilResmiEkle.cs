namespace Makale_DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KullaniciProfilResmiEkle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kullanici", "ProfilResim", c => c.String(maxLength: 20));
            Sql("Update Kullanici set ProfilResim='user_1.jpg' ");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kullanici", "ProfilResim");
        }
    }
}
