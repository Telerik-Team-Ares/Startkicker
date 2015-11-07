namespace Startkicker.Api
{
    using System.Data.Entity;
    using Startkicker.Data;
    using Startkicker.Data.Migrations;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StartkickerDbContext, Configuration>());
            StartkickerDbContext.Create().Database.Initialize(true);
        }
    }
}