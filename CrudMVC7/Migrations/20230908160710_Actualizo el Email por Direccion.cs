using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudMVC7.Migrations
{
    /// <inheritdoc />
    public partial class ActualizoelEmailporDireccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactImmunization");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Contacts",
                newName: "Direccion");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApplicationDate",
                table: "ImmunizationPatients",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Direccion",
                table: "Contacts",
                newName: "Email");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApplicationDate",
                table: "ImmunizationPatients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ContactImmunization",
                columns: table => new
                {
                    ContactsContactId = table.Column<int>(type: "int", nullable: false),
                    ImmunizationsImmunizationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactImmunization", x => new { x.ContactsContactId, x.ImmunizationsImmunizationId });
                    table.ForeignKey(
                        name: "FK_ContactImmunization_Contacts_ContactsContactId",
                        column: x => x.ContactsContactId,
                        principalTable: "Contacts",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactImmunization_Immunizations_ImmunizationsImmunizationId",
                        column: x => x.ImmunizationsImmunizationId,
                        principalTable: "Immunizations",
                        principalColumn: "ImmunizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactImmunization_ImmunizationsImmunizationId",
                table: "ContactImmunization",
                column: "ImmunizationsImmunizationId");
        }
    }
}
