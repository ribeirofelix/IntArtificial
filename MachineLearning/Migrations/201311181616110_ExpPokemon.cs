namespace MachineLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpPokemon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pokemons", "Exp", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pokemons", "Exp");
        }
    }
}
