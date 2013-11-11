namespace MachineLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PokeIdIntAndPokeBody : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pokemons", "Body", c => c.Int(nullable: false));
            AlterColumn("dbo.Pokemons", "PokeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pokemons", "PokeId", c => c.String());
            DropColumn("dbo.Pokemons", "Body");
        }
    }
}
