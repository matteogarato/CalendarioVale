using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarioVale.Migrations
{
    /// <inheritdoc />
    public partial class Biometrics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Biometrics",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    DateReading = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DailyStatusDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biometrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Biometrics_DailyStatuses_DailyStatusDate",
                        column: x => x.DailyStatusDate,
                        principalTable: "DailyStatuses",
                        principalColumn: "Date");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Biometrics_DailyStatusDate",
                table: "Biometrics",
                column: "DailyStatusDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Biometrics");
        }
    }
}