using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionProyectos.API.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTableAsignacionProyecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colaboradores");

            migrationBuilder.RenameColumn(
                name: "Rol",
                table: "Usuarios",
                newName: "RolSistema");

            migrationBuilder.AddColumn<bool>(
                name: "EstaActivo",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AsignacionesProyectos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    ProyectoId = table.Column<int>(type: "int", nullable: false),
                    RolEnProyecto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignacionesProyectos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AsignacionesProyectos_Proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AsignacionesProyectos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesProyectos_ProyectoId",
                table: "AsignacionesProyectos",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesProyectos_UsuarioId",
                table: "AsignacionesProyectos",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AsignacionesProyectos");

            migrationBuilder.DropColumn(
                name: "EstaActivo",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "RolSistema",
                table: "Usuarios",
                newName: "Rol");

            migrationBuilder.CreateTable(
                name: "Colaboradores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProyectoId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colaboradores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colaboradores_Proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_ProyectoId",
                table: "Colaboradores",
                column: "ProyectoId");
        }
    }
}
