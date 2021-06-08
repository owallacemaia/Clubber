using Microsoft.EntityFrameworkCore.Migrations;

namespace Club.Data.Migrations
{
    public partial class createIntegrantes1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Integrante_Usuarios_UsuarioId",
                table: "Integrante");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Integrante",
                table: "Integrante");

            migrationBuilder.RenameTable(
                name: "Integrante",
                newName: "Integrantes");

            migrationBuilder.RenameIndex(
                name: "IX_Integrante_UsuarioId",
                table: "Integrantes",
                newName: "IX_Integrantes_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Integrantes",
                table: "Integrantes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Integrantes_Usuarios_UsuarioId",
                table: "Integrantes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Integrantes_Usuarios_UsuarioId",
                table: "Integrantes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Integrantes",
                table: "Integrantes");

            migrationBuilder.RenameTable(
                name: "Integrantes",
                newName: "Integrante");

            migrationBuilder.RenameIndex(
                name: "IX_Integrantes_UsuarioId",
                table: "Integrante",
                newName: "IX_Integrante_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Integrante",
                table: "Integrante",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Integrante_Usuarios_UsuarioId",
                table: "Integrante",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
