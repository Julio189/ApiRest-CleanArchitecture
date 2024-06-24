using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ModeloApi.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pessoa",
                columns: table => new
                {
                    psa_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    psa_nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    psa_documento = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    psa_telefone = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pessoa", x => x.psa_id);
                });

            migrationBuilder.CreateTable(
                name: "produto",
                columns: table => new
                {
                    pdt_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pdt_nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    pdt_codErp = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    pdt_preco = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produto", x => x.pdt_id);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    usr_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usr_nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    usr_senha = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    usr_grupo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    usr_status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.usr_id);
                });

            migrationBuilder.CreateTable(
                name: "compra",
                columns: table => new
                {
                    cmp_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pdt_id = table.Column<int>(type: "integer", nullable: false),
                    psa_id = table.Column<int>(type: "integer", nullable: false),
                    cmp_data = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_compra", x => x.cmp_id);
                    table.ForeignKey(
                        name: "FK_compra_pessoa_psa_id",
                        column: x => x.psa_id,
                        principalTable: "pessoa",
                        principalColumn: "psa_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_compra_produto_pdt_id",
                        column: x => x.pdt_id,
                        principalTable: "produto",
                        principalColumn: "pdt_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_compra_pdt_id",
                table: "compra",
                column: "pdt_id");

            migrationBuilder.CreateIndex(
                name: "IX_compra_psa_id",
                table: "compra",
                column: "psa_id");

            migrationBuilder.CreateIndex(
                name: "IX_pessoa_psa_documento",
                table: "pessoa",
                column: "psa_documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pessoa_psa_telefone",
                table: "pessoa",
                column: "psa_telefone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_produto_pdt_codErp",
                table: "produto",
                column: "pdt_codErp",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_produto_pdt_nome",
                table: "produto",
                column: "pdt_nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuario_usr_nome",
                table: "usuario",
                column: "usr_nome",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "compra");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "pessoa");

            migrationBuilder.DropTable(
                name: "produto");
        }
    }
}
