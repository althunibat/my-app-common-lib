using System;
using Godwit.Common.Data.Model;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Godwit.Common.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:activity_level", "sedentary,lightly_active,moderately_active,very_active")
                .Annotation("Npgsql:Enum:deficit_goal", "lose_weight,maintain,gain_muscle")
                .Annotation("Npgsql:Enum:gender", "male,female")
                .Annotation("Npgsql:Enum:measurement_unit", "g,tbsp,tsp,cup,ml")
                .Annotation("Npgsql:PostgresExtension:citext", ",,");

            migrationBuilder.CreateSequence(
                name: "EntityFrameworkHiLoSequence",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "ingredient",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    name = table.Column<string>(type: "citext", maxLength: 256, nullable: false),
                    measurement_unit = table.Column<MeasurementUnit>(nullable: false),
                    serving_value = table.Column<float>(nullable: false),
                    calories_per_serving = table.Column<float>(nullable: false),
                    carbs_per_serving = table.Column<float>(nullable: false),
                    fat_per_serving = table.Column<float>(nullable: false),
                    protein_per_serving = table.Column<float>(nullable: false),
                    created_on = table.Column<Instant>(nullable: false),
                    updated_on = table.Column<Instant>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ingredient", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_tokens",
                columns: table => new
                {
                    user_id = table.Column<string>(nullable: false),
                    login_provider = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    user_name = table.Column<string>(maxLength: 256, nullable: false),
                    normalized_user_name = table.Column<string>(maxLength: 256, nullable: false),
                    email = table.Column<string>(maxLength: 256, nullable: false),
                    normalized_email = table.Column<string>(maxLength: 256, nullable: false),
                    email_confirmed = table.Column<bool>(nullable: false),
                    password_hash = table.Column<string>(nullable: false),
                    security_stamp = table.Column<string>(nullable: false),
                    concurrency_stamp = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(maxLength: 15, nullable: false),
                    phone_number_confirmed = table.Column<bool>(nullable: false),
                    two_factor_enabled = table.Column<bool>(nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(nullable: true),
                    lockout_enabled = table.Column<bool>(nullable: false),
                    access_failed_count = table.Column<int>(nullable: false),
                    first_name = table.Column<string>(maxLength: 256, nullable: false),
                    last_name = table.Column<string>(maxLength: 256, nullable: false),
                    birth_date = table.Column<LocalDate>(nullable: false),
                    gender = table.Column<Gender>(nullable: false),
                    created_on = table.Column<Instant>(nullable: false),
                    updated_on = table.Column<Instant>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<string>(nullable: false),
                    claim_type = table.Column<string>(nullable: false),
                    claim_value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_claims_roles_identity_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notification",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    user_id = table.Column<string>(nullable: false),
                    message = table.Column<string>(nullable: false),
                    created_on = table.Column<Instant>(nullable: false),
                    read_on = table.Column<Instant>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notification", x => x.id);
                    table.ForeignKey(
                        name: "fk_notification_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    user_id = table.Column<string>(nullable: false),
                    claim_type = table.Column<string>(nullable: false),
                    claim_value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_claims_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_intake_target",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    user_id = table.Column<string>(nullable: false),
                    height = table.Column<float>(nullable: false),
                    fat_percentage = table.Column<float>(nullable: false),
                    activity_level = table.Column<ActivityLevel>(nullable: false),
                    goal = table.Column<DeficitGoal>(nullable: false),
                    deficit_percentage = table.Column<float>(nullable: false),
                    net_carbs_percentage = table.Column<float>(nullable: false),
                    protein_taken = table.Column<float>(nullable: false),
                    total_calories_intake = table.Column<float>(nullable: true),
                    total_fat_intake = table.Column<float>(nullable: true),
                    total_carbs_intake = table.Column<float>(nullable: true),
                    total_protein_intake = table.Column<float>(nullable: true),
                    created_on = table.Column<Instant>(nullable: false),
                    updated_on = table.Column<Instant>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_intake_target", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_intake_target_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_logins",
                columns: table => new
                {
                    login_provider = table.Column<string>(nullable: false),
                    provider_key = table.Column<string>(nullable: false),
                    provider_display_name = table.Column<string>(nullable: true),
                    user_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_user_logins_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_id = table.Column<string>(nullable: false),
                    role_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_user_roles_roles_identity_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "daily_log",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    target_id = table.Column<long>(nullable: false),
                    user_intake_target_id = table.Column<long>(nullable: true),
                    total_calories = table.Column<short>(nullable: false),
                    total_carbs = table.Column<short>(nullable: false),
                    total_fat = table.Column<short>(nullable: false),
                    total_protein = table.Column<short>(nullable: false),
                    created_on = table.Column<Instant>(nullable: false),
                    updated_on = table.Column<Instant>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_daily_log", x => x.id);
                    table.ForeignKey(
                        name: "fk_daily_log_user_intake_target_user_intake_target_id",
                        column: x => x.user_intake_target_id,
                        principalTable: "user_intake_target",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "intake",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    target_id = table.Column<long>(nullable: false),
                    ingredient_id = table.Column<long>(nullable: false),
                    measurement_unit = table.Column<MeasurementUnit>(nullable: false),
                    size_taken = table.Column<float>(nullable: false),
                    calories = table.Column<float>(nullable: false),
                    carbs = table.Column<float>(nullable: false),
                    fat = table.Column<float>(nullable: false),
                    protein = table.Column<float>(nullable: false),
                    time = table.Column<Instant>(nullable: false),
                    created_on = table.Column<Instant>(nullable: false),
                    updated_on = table.Column<Instant>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_intake", x => x.id);
                    table.ForeignKey(
                        name: "fk_intake_ingredient_ingredient_id",
                        column: x => x.ingredient_id,
                        principalTable: "ingredient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_intake_user_intake_target_user_intake_target_id",
                        column: x => x.target_id,
                        principalTable: "user_intake_target",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_daily_log_user_intake_target_id",
                table: "daily_log",
                column: "user_intake_target_id");

            migrationBuilder.CreateIndex(
                name: "ix_ingredient_name",
                table: "ingredient",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_intake_ingredient_id",
                table: "intake",
                column: "ingredient_id");

            migrationBuilder.CreateIndex(
                name: "ix_intake_target_id",
                table: "intake",
                column: "target_id");

            migrationBuilder.CreateIndex(
                name: "ix_notification_user_id",
                table: "notification",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_claims_role_id",
                table: "role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_roles_normalized_name",
                table: "roles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_claims_user_id",
                table: "user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_intake_target_user_id",
                table: "user_intake_target",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_logins_user_id",
                table: "user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_role_id",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_normalized_email",
                table: "users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "ix_users_normalized_user_name",
                table: "users",
                column: "normalized_user_name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "daily_log");

            migrationBuilder.DropTable(
                name: "intake");

            migrationBuilder.DropTable(
                name: "notification");

            migrationBuilder.DropTable(
                name: "role_claims");

            migrationBuilder.DropTable(
                name: "user_claims");

            migrationBuilder.DropTable(
                name: "user_logins");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "user_tokens");

            migrationBuilder.DropTable(
                name: "ingredient");

            migrationBuilder.DropTable(
                name: "user_intake_target");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropSequence(
                name: "EntityFrameworkHiLoSequence");
        }
    }
}
