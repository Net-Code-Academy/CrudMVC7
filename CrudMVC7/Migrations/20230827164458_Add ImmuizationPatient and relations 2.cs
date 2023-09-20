using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudMVC7.Migrations
{
    /// <inheritdoc />
    public partial class AddImmuizationPatientandrelations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImmunizationPatients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    ImmunizationId = table.Column<int>(type: "int", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImmunizationPatients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImmunizationPatients_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImmunizationPatients_Immunizations_ImmunizationId",
                        column: x => x.ImmunizationId,
                        principalTable: "Immunizations",
                        principalColumn: "ImmunizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImmunizationPatients_ContactId",
                table: "ImmunizationPatients",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ImmunizationPatients_ImmunizationId",
                table: "ImmunizationPatients",
                column: "ImmunizationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImmunizationPatients");
        }
    }
}
