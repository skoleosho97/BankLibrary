using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Middleware.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applicants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    SocialSecurity = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    DriversLicense = table.Column<string>(type: "text", nullable: false),
                    Income = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    Zipcode = table.Column<string>(type: "text", nullable: false),
                    MAddress = table.Column<string>(type: "text", nullable: false),
                    MCity = table.Column<string>(type: "text", nullable: false),
                    MState = table.Column<string>(type: "text", nullable: false),
                    MZipcode = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicants", x => x.Id);
                });

            migrationBuilder.Sql(
            @"
                CREATE FUNCTION ""Fn_Applicants_Updated""() RETURNS TRIGGER LANGUAGE PLPGSQL AS $$
                BEGIN
                    NEW.""LastModified"" = CURRENT_TIMESTAMP;
                    RETURN NEW;
                END;
                $$;

                CREATE TRIGGER ""UpdateTimestamp""
                    BEFORE INSERT OR UPDATE
                    ON ""Applicants""
                    FOR EACH ROW
                    EXECUTE FUNCTION ""Fn_Applicants_Updated""();
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applicants");
        }
    }
}
