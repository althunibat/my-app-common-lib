using Godwit.Common.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Godwit.Common.Data {
    public class KetoDbContext : IdentityDbContext<User> {
        static KetoDbContext() {
            NpgsqlConnection.GlobalTypeMapper
                .MapEnum<Gender>()
                .MapEnum<MeasurementUnit>()
                .MapEnum<DeficitGoal>()
                .MapEnum<ActivityLevel>();
        }

        public KetoDbContext(DbContextOptions<KetoDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) {
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
}