using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sdatcc_v2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tccs_Alunos_AlunoId",
                table: "Tccs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tccs_Arquivos_ArquivoId",
                table: "Tccs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tccs_Professores_ProfessorId",
                table: "Tccs");

            migrationBuilder.AlterColumn<int>(
                name: "ProfessorId",
                table: "Tccs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ArquivoId",
                table: "Tccs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AlunoId",
                table: "Tccs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tccs_Alunos_AlunoId",
                table: "Tccs",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tccs_Arquivos_ArquivoId",
                table: "Tccs",
                column: "ArquivoId",
                principalTable: "Arquivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tccs_Professores_ProfessorId",
                table: "Tccs",
                column: "ProfessorId",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tccs_Alunos_AlunoId",
                table: "Tccs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tccs_Arquivos_ArquivoId",
                table: "Tccs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tccs_Professores_ProfessorId",
                table: "Tccs");

            migrationBuilder.AlterColumn<int>(
                name: "ProfessorId",
                table: "Tccs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ArquivoId",
                table: "Tccs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AlunoId",
                table: "Tccs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tccs_Alunos_AlunoId",
                table: "Tccs",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tccs_Arquivos_ArquivoId",
                table: "Tccs",
                column: "ArquivoId",
                principalTable: "Arquivos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tccs_Professores_ProfessorId",
                table: "Tccs",
                column: "ProfessorId",
                principalTable: "Professores",
                principalColumn: "Id");
        }
    }
}
