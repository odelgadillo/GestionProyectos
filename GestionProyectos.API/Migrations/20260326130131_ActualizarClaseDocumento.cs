using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionProyectos.API.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarClaseDocumento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSubida",
                table: "Documentos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Documentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_UsuarioId",
                table: "Documentos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_Usuarios_UsuarioId",
                table: "Documentos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_Usuarios_UsuarioId",
                table: "Documentos");

            migrationBuilder.DropIndex(
                name: "IX_Documentos_UsuarioId",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "FechaSubida",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Documentos");
        }
    }
}
