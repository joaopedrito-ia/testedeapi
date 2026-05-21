using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiProjeto.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Produtos_Nome",
                table: "Produtos",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_Nome",
                table: "Categorias",
                column: "Nome",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Produtos_Nome",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_Nome",
                table: "Categorias");
        }
    }
}