using Godwit.Common.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

namespace Godwit.Common.Data
{
    public class KetoDbContext : IdentityDbContext<User>
    {
        static KetoDbContext()
        {
            NpgsqlConnection.GlobalTypeMapper
                .MapEnum<Gender>()
                .MapEnum<MeasurementUnit>()
                .MapEnum<DeficitGoal>()
                .MapEnum<ActivityLevel>();
        }

        public KetoDbContext(DbContextOptions<KetoDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresEnum<Gender>();
            builder.HasPostgresEnum<MeasurementUnit>();
            builder.HasPostgresEnum<DeficitGoal>();
            builder.HasPostgresEnum<ActivityLevel>();
            builder.HasPostgresExtension("citext");

            builder.ApplyConfiguration(new UserEntityConfig());
            builder.ApplyConfiguration(new UserIntakeTargetEntityConfig());
            builder.ApplyConfiguration(new NotificationEntityConfig());
            builder.ApplyConfiguration(new IntakeEntityConfig());
            builder.ApplyConfiguration(new IngredientEntityConfig());
            builder.ApplyConfiguration(new IdentityUserTokenEntityConfig());
            builder.ApplyConfiguration(new IdentityUserTokenEntityConfig());
            builder.ApplyConfiguration(new IdentityUserLoginEntityConfig());
            builder.ApplyConfiguration(new IdentityUserClaimEntityConfig());
            builder.ApplyConfiguration(new IdentityUserRoleEntityConfig());
            builder.ApplyConfiguration(new IdentityRoleEntityConfig());
            builder.ApplyConfiguration(new IdentityRoleClaimEntityConfig());
        }
    }

    public class KetoDbContextFactory : IDesignTimeDbContextFactory<KetoDbContext>
    {
        public KetoDbContext CreateDbContext(string[] args)
        {
            const string dbConn = "Server=apps.db.godwit.io;Database=keto_db;User Id=postgres;Password=1qaz!QAZ;";


            var optionsBuilder = new DbContextOptionsBuilder<KetoDbContext>();
            optionsBuilder.UseNpgsql(dbConn, opt =>
            {
                opt.UseAdminDatabase("postgres");
                opt.UseNodaTime();
            }).UseSnakeCaseNamingConvention();

            return new KetoDbContext(optionsBuilder.Options);
        }
    }
}