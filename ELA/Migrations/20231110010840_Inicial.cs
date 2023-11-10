using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ELA.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Assuntos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assuntos", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false),
                    CPF = table.Column<string>(type: "longtext", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Senha = table.Column<string>(type: "longtext", nullable: false),
                    TipoUsuarioEnum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Artigos",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SubTitulo = table.Column<string>(type: "longtext", nullable: false),
                    Texto = table.Column<string>(type: "longtext", nullable: false),
                    Titulo = table.Column<string>(type: "longtext", nullable: false),
                    Resumo = table.Column<string>(type: "longtext", nullable: false),
                    DataPostagem = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artigos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artigos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FiqueAtentos",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Texto = table.Column<string>(type: "longtext", nullable: false),
                    Titulo = table.Column<string>(type: "longtext", nullable: false),
                    Resumo = table.Column<string>(type: "longtext", nullable: false),
                    DataPostagem = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiqueAtentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiqueAtentos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Perguntas",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Resposta = table.Column<string>(type: "longtext", nullable: false),
                    Titulo = table.Column<string>(type: "longtext", nullable: false),
                    Resumo = table.Column<string>(type: "longtext", nullable: false),
                    DataPostagem = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perguntas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Perguntas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ArtigoAssunto",
                columns: table => new
                {
                    ArtigoId = table.Column<int>(type: "int", nullable: false),
                    AssuntosId = table.Column<int>(type: "int", nullable: false),
                    AssuntoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtigoAssunto", x => new { x.ArtigoId, x.AssuntosId });
                    table.ForeignKey(
                        name: "FK_ArtigoAssunto_Artigos_ArtigoId",
                        column: x => x.ArtigoId,
                        principalTable: "Artigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtigoAssunto_Assuntos_AssuntoId",
                        column: x => x.AssuntoId,
                        principalTable: "Assuntos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArtigoAssunto_Assuntos_AssuntosId",
                        column: x => x.AssuntosId,
                        principalTable: "Assuntos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AssuntoFiqueAtento",
                columns: table => new
                {
                    AssuntosId = table.Column<int>(type: "int", nullable: false),
                    FiqueAtentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssuntoFiqueAtento", x => new { x.AssuntosId, x.FiqueAtentoId });
                    table.ForeignKey(
                        name: "FK_AssuntoFiqueAtento_Assuntos_AssuntosId",
                        column: x => x.AssuntosId,
                        principalTable: "Assuntos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssuntoFiqueAtento_FiqueAtentos_FiqueAtentoId",
                        column: x => x.FiqueAtentoId,
                        principalTable: "FiqueAtentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AssuntoPergunta",
                columns: table => new
                {
                    AssuntosId = table.Column<int>(type: "int", nullable: false),
                    PerguntaId = table.Column<int>(type: "int", nullable: false),
                    AssuntoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssuntoPergunta", x => new { x.AssuntosId, x.PerguntaId });
                    table.ForeignKey(
                        name: "FK_AssuntoPergunta_Assuntos_AssuntoId",
                        column: x => x.AssuntoId,
                        principalTable: "Assuntos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssuntoPergunta_Assuntos_AssuntosId",
                        column: x => x.AssuntosId,
                        principalTable: "Assuntos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssuntoPergunta_Perguntas_PerguntaId",
                        column: x => x.PerguntaId,
                        principalTable: "Perguntas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Assuntos",
                columns: new[] { "Id", "Descricao" },
                values: new object[,]
                {
                    { 1, "Infantil" },
                    { 2, "Meninas" },
                    { 3, "Meninos" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "CPF", "DataNascimento", "Email", "Nome", "Senha", "TipoUsuarioEnum" },
                values: new object[,]
                {
                    { 1, "123.123.123-12", new DateTime(1987, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "bella.swan@email.com", "Isabella Swan", "edwardJacob", 3 },
                    { 2, "122.123.123-12", new DateTime(1987, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "katnip@email.com", "Katniss Everdeen", "girlOnFire", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtigoAssunto_AssuntoId",
                table: "ArtigoAssunto",
                column: "AssuntoId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtigoAssunto_AssuntosId",
                table: "ArtigoAssunto",
                column: "AssuntosId");

            migrationBuilder.CreateIndex(
                name: "IX_Artigos_UsuarioId",
                table: "Artigos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AssuntoFiqueAtento_FiqueAtentoId",
                table: "AssuntoFiqueAtento",
                column: "FiqueAtentoId");

            migrationBuilder.CreateIndex(
                name: "IX_AssuntoPergunta_AssuntoId",
                table: "AssuntoPergunta",
                column: "AssuntoId");

            migrationBuilder.CreateIndex(
                name: "IX_AssuntoPergunta_PerguntaId",
                table: "AssuntoPergunta",
                column: "PerguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_FiqueAtentos_UsuarioId",
                table: "FiqueAtentos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Perguntas_UsuarioId",
                table: "Perguntas",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtigoAssunto");

            migrationBuilder.DropTable(
                name: "AssuntoFiqueAtento");

            migrationBuilder.DropTable(
                name: "AssuntoPergunta");

            migrationBuilder.DropTable(
                name: "Artigos");

            migrationBuilder.DropTable(
                name: "FiqueAtentos");

            migrationBuilder.DropTable(
                name: "Assuntos");

            migrationBuilder.DropTable(
                name: "Perguntas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
