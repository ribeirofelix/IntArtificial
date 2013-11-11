namespace MachineLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pokeIdString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pokemons", "PokeId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pokemons", "PokeId", c => c.Int(nullable: false));
        }
    }
}
