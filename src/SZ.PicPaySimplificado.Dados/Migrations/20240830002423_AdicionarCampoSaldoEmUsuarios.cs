using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SZ.PicPaySimplificado.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCampoSaldoEmUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Saldo",
                table: "Usuarios",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "Usuarios");
        }
    }
}
