using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SistemaDeControleDeFrequencia.Migrations
{
    /// <inheritdoc />
    public partial class SeedStatusFrequencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Setor",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Status_Frequencia",
                columns: new[] { "Id", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 1, "Frequência ainda não enviada ao RH.", "Pendente" },
                    { 2, "Frequência recebida pelo RH.", "Recebido" },
                    { 3, "Frequência conferida e regularizada.", "Regularizado" },
                    { 4, "Frequência lançada oficialmente pelo RH.", "Lançado" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Setor_Nome",
                table: "Setor",
                column: "Nome",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Setor_Nome",
                table: "Setor");

            migrationBuilder.DeleteData(
                table: "Status_Frequencia",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Status_Frequencia",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Status_Frequencia",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Status_Frequencia",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Setor",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
