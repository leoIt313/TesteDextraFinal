using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TesteDextra.Infra.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Ingrediente",
                schema: "dbo",
                columns: table => new
                {
                    IdIngrediente = table.Column<long>(type: "bigint", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", unicode: false, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingrediente", x => x.IdIngrediente);
                });

            migrationBuilder.CreateTable(
                name: "Lanche",
                schema: "dbo",
                columns: table => new
                {
                    IdLanche = table.Column<long>(type: "bigint", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lanche", x => x.IdLanche);
                });

            migrationBuilder.CreateTable(
                name: "Parametro",
                schema: "dbo",
                columns: table => new
                {
                    IdParametro = table.Column<long>(type: "bigint", nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, nullable: false),
                    Valor = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametro", x => x.IdParametro);
                });

            migrationBuilder.CreateTable(
                name: "StatusPedido",
                schema: "dbo",
                columns: table => new
                {
                    IdStatusPedido = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(15)", unicode: false, nullable: false),
                    Sigla = table.Column<string>(type: "varchar(3)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusPedido", x => x.IdStatusPedido);
                });

            migrationBuilder.CreateTable(
                name: "TipoPromocao",
                schema: "dbo",
                columns: table => new
                {
                    IdTipoPromocao = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "varchar(255)", unicode: false, nullable: true),
                    Nome = table.Column<string>(type: "varchar(30)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPromocao", x => x.IdTipoPromocao);
                });

            migrationBuilder.CreateTable(
                name: "LancheIngrediente",
                schema: "dbo",
                columns: table => new
                {
                    IdLancheIngrediente = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdIngrediente = table.Column<long>(type: "bigint", nullable: false),
                    IdLanche = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LancheIngrediente", x => x.IdLancheIngrediente);
                    table.ForeignKey(
                        name: "FK_LancheIngrediente_Ingrediente_IdIngrediente",
                        column: x => x.IdIngrediente,
                        principalSchema: "dbo",
                        principalTable: "Ingrediente",
                        principalColumn: "IdIngrediente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LancheIngrediente_Lanche_IdLanche",
                        column: x => x.IdLanche,
                        principalSchema: "dbo",
                        principalTable: "Lanche",
                        principalColumn: "IdLanche",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                schema: "dbo",
                columns: table => new
                {
                    IdPedido = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataPedido = table.Column<DateTime>(type: "datetime", nullable: false),
                    IdStatusPedido = table.Column<int>(type: "int", nullable: false),
                    NomeLanche = table.Column<string>(type: "varchar(50)", nullable: false),
                    NumeroPedido = table.Column<int>(type: "int", nullable: false),
                    ValorFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.IdPedido);
                    table.ForeignKey(
                        name: "FK_Pedido_StatusPedido_IdStatusPedido",
                        column: x => x.IdStatusPedido,
                        principalSchema: "dbo",
                        principalTable: "StatusPedido",
                        principalColumn: "IdStatusPedido",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PedidoIngrediente",
                schema: "dbo",
                columns: table => new
                {
                    IdPedidoIngrediente = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdIngrediente = table.Column<long>(type: "bigint", nullable: false),
                    IdPedido = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoIngrediente", x => x.IdPedidoIngrediente);
                    table.ForeignKey(
                        name: "FK_PedidoIngrediente_Ingrediente_IdIngrediente",
                        column: x => x.IdIngrediente,
                        principalSchema: "dbo",
                        principalTable: "Ingrediente",
                        principalColumn: "IdIngrediente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PedidoIngrediente_Pedido_IdPedido",
                        column: x => x.IdPedido,
                        principalSchema: "dbo",
                        principalTable: "Pedido",
                        principalColumn: "IdPedido",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LancheIngrediente_IdIngrediente",
                schema: "dbo",
                table: "LancheIngrediente",
                column: "IdIngrediente");

            migrationBuilder.CreateIndex(
                name: "IX_LancheIngrediente_IdLanche",
                schema: "dbo",
                table: "LancheIngrediente",
                column: "IdLanche");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_IdStatusPedido",
                schema: "dbo",
                table: "Pedido",
                column: "IdStatusPedido");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoIngrediente_IdIngrediente",
                schema: "dbo",
                table: "PedidoIngrediente",
                column: "IdIngrediente");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoIngrediente_IdPedido",
                schema: "dbo",
                table: "PedidoIngrediente",
                column: "IdPedido");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LancheIngrediente",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Parametro",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PedidoIngrediente",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TipoPromocao",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Lanche",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Ingrediente",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Pedido",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "StatusPedido",
                schema: "dbo");
        }
    }
}
