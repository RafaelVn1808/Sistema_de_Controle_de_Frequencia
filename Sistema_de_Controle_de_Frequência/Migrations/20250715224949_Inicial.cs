using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaDeControleDeFrequencia.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nucleo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nucleo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status_Frequencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status_Frequencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NucleoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Setor_Nucleo_NucleoId",
                        column: x => x.NucleoId,
                        principalTable: "Nucleo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Frequencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MesReferencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusFrequenciaId = table.Column<int>(type: "int", nullable: false),
                    SetorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Frequencia_Setor_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Frequencia_Status_Frequencia_StatusFrequenciaId",
                        column: x => x.StatusFrequenciaId,
                        principalTable: "Status_Frequencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servidor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Matricula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_setor = table.Column<int>(type: "int", nullable: false),
                    SetorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servidor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servidor_Setor_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Frequencia_Servidor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FrequenciaId = table.Column<int>(type: "int", nullable: false),
                    ServidorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequencia_Servidor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Frequencia_Servidor_Frequencia_FrequenciaId",
                        column: x => x.FrequenciaId,
                        principalTable: "Frequencia",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Frequencia_Servidor_Servidor_ServidorId",
                        column: x => x.ServidorId,
                        principalTable: "Servidor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Frequencia_SetorId",
                table: "Frequencia",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Frequencia_StatusFrequenciaId",
                table: "Frequencia",
                column: "StatusFrequenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Frequencia_Servidor_FrequenciaId",
                table: "Frequencia_Servidor",
                column: "FrequenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Frequencia_Servidor_ServidorId",
                table: "Frequencia_Servidor",
                column: "ServidorId");

            migrationBuilder.CreateIndex(
                name: "IX_Servidor_SetorId",
                table: "Servidor",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Setor_NucleoId",
                table: "Setor",
                column: "NucleoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Frequencia_Servidor");

            migrationBuilder.DropTable(
                name: "Frequencia");

            migrationBuilder.DropTable(
                name: "Servidor");

            migrationBuilder.DropTable(
                name: "Status_Frequencia");

            migrationBuilder.DropTable(
                name: "Setor");

            migrationBuilder.DropTable(
                name: "Nucleo");
        }
    }
}
