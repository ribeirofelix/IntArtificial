namespace MachineLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirtMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pokemons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PokeId = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        Widht = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pokemons");
        }
    }
}
