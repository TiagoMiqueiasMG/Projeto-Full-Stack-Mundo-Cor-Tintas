using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace repository.Migrations
{
    /// <inheritdoc />
    public partial class migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: false),
                    UId = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Frete",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCliente = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: false),
                    Cep = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: false),
                    Rua = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    DataEntrega = table.Column<DateTime>(type: "datetime", nullable: true),
                    ValorKm = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Distancia = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    ValorFrete = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    ValorCompra = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Usuarioid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frete", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Frete_Usuario",
                        column: x => x.Usuarioid,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Frete_Usuarioid",
                table: "Frete",
                column: "Usuarioid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Frete");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
