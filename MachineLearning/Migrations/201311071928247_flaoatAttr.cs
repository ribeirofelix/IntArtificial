namespace MachineLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flaoatAttr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pokemons", "Height", c => c.Single(nullable: false));
            AlterColumn("dbo.Pokemons", "Widht", c => c.Single(nullable: false));
            AlterColumn("dbo.Pokemons", "Weight", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pokemons", "Weight", c => c.Int(nullable: false));
            AlterColumn("dbo.Pokemons", "Widht", c => c.Int(nullable: false));
            AlterColumn("dbo.Pokemons", "Height", c => c.Int(nullable: false));
        }
    }
}
