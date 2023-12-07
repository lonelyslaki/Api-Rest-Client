using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApiProject.Migrations
{
    /// <inheritdoc />
    public partial class AddCorreoToClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Clients",
                newName: "Numero");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Clients",
                newName: "Nombre");

            migrationBuilder.AddColumn<string>(
                name: "Correo",
                table: "Clients",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "Clients",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Correo",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Edad",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "Clients",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Clients",
                newName: "Email");
        }
    }
}
