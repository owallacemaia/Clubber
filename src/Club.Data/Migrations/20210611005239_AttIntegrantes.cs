using Microsoft.EntityFrameworkCore.Migrations;

namespace Club.Data.Migrations
{
    public partial class AttIntegrantes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Integrantes_Usuarios_UsuarioId",
                table: "Integrantes");

            migrationBuilder.DropIndex(
                name: "IX_Integrantes_UsuarioId",
                table: "Integrantes");

            migrationBuilder.CreateIndex(
                name: "IX_Integrantes_GrupoId",
                table: "Integrantes",
                column: "GrupoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Integrantes_Grupos_GrupoId",
                table: "Integrantes",
                column: "GrupoId",
                principalTable: "Grupos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Integrantes_Grupos_GrupoId",
                table: "Integrantes");

            migrationBuilder.DropIndex(
                name: "IX_Integrantes_GrupoId",
                table: "Integrantes");

            migrationBuilder.CreateIndex(
                name: "IX_Integrantes_UsuarioId",
                table: "Integrantes",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Integrantes_Usuarios_UsuarioId",
                table: "Integrantes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
