namespace MachineLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LotOfChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pokemons", "CatchRate", c => c.Int(nullable: false));
            AddColumn("dbo.Pokemons", "PerfSpeed", c => c.Int(nullable: false));
            AddColumn("dbo.Pokemons", "PerfPower", c => c.Int(nullable: false));
            AddColumn("dbo.Pokemons", "PerfSkill", c => c.Int(nullable: false));
            AddColumn("dbo.Pokemons", "PerfStamina", c => c.Int(nullable: false));
            AddColumn("dbo.Pokemons", "PerfJump", c => c.Int(nullable: false));
            AddColumn("dbo.Pokemons", "PerfTotal", c => c.Int(nullable: false));
            AddColumn("dbo.Pokemons", "PerfAvg", c => c.Int(nullable: false));
            AddColumn("dbo.Pokemons", "BaseHp", c => c.Int(nullable: false));
            AddColumn("dbo.Pokemons", "BaseAttack", c => c.Int(nullable: false));
            AddColumn("dbo.Pokemons", "BaseDefense", c => c.Int(nullable: false));
            AddColumn("dbo.Pokemons", "BaseSpAttack", c => c.Int(nullable: false));
            AddColumn("dbo.Pokemons", "BaseSpDefense", c => c.Int(nullable: false));
            AddColumn("dbo.Pokemons", "BaseSpeed", c => c.Int(nullable: false));
            AddColumn("dbo.Pokemons", "BaseTotal", c => c.Int(nullable: false));
            AddColumn("dbo.Pokemons", "BaseAvarage", c => c.Single(nullable: false));
            AddColumn("dbo.Pokemons", "Color", c => c.String());
            AddColumn("dbo.Pokemons", "Habitat", c => c.String());
            AddColumn("dbo.Pokemons", "Ability1", c => c.String());
            AddColumn("dbo.Pokemons", "Ability2", c => c.String());
            AddColumn("dbo.Pokemons", "Hidden", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pokemons", "Hidden");
            DropColumn("dbo.Pokemons", "Ability2");
            DropColumn("dbo.Pokemons", "Ability1");
            DropColumn("dbo.Pokemons", "Habitat");
            DropColumn("dbo.Pokemons", "Color");
            DropColumn("dbo.Pokemons", "BaseAvarage");
            DropColumn("dbo.Pokemons", "BaseTotal");
            DropColumn("dbo.Pokemons", "BaseSpeed");
            DropColumn("dbo.Pokemons", "BaseSpDefense");
            DropColumn("dbo.Pokemons", "BaseSpAttack");
            DropColumn("dbo.Pokemons", "BaseDefense");
            DropColumn("dbo.Pokemons", "BaseAttack");
            DropColumn("dbo.Pokemons", "BaseHp");
            DropColumn("dbo.Pokemons", "PerfAvg");
            DropColumn("dbo.Pokemons", "PerfTotal");
            DropColumn("dbo.Pokemons", "PerfJump");
            DropColumn("dbo.Pokemons", "PerfStamina");
            DropColumn("dbo.Pokemons", "PerfSkill");
            DropColumn("dbo.Pokemons", "PerfPower");
            DropColumn("dbo.Pokemons", "PerfSpeed");
            DropColumn("dbo.Pokemons", "CatchRate");
        }
    }
}
