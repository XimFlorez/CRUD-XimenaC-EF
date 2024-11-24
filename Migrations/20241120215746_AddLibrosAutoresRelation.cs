using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDXimenaCastanedaEF.Migrations
{
    /// <inheritdoc />
    public partial class AddLibrosAutoresRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    ID_Autor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Apellido = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Telefono = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Direccion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Autores__9626AD2681B59C6F", x => x.ID_Autor);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Codigo_categoria = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__738F04AD303E9C37", x => x.Codigo_categoria);
                });

            migrationBuilder.CreateTable(
                name: "Editoriales",
                columns: table => new
                {
                    Nit = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Direccion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Sitioweb = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Editoria__C7D1D6DBD2583A8B", x => x.Nit);
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    Isbn = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Titulo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Nombre_autor = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Publicacion = table.Column<DateOnly>(type: "date", nullable: true),
                    Fecha_registro = table.Column<DateOnly>(type: "date", nullable: true),
                    Codigo_categoria = table.Column<int>(type: "int", nullable: true),
                    Nit_editorial = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Libros__9271CED1FC7FF783", x => x.Isbn);
                    table.ForeignKey(
                        name: "FK__Libros__Codigo_c__4D94879B",
                        column: x => x.Codigo_categoria,
                        principalTable: "Categorias",
                        principalColumn: "Codigo_categoria");
                    table.ForeignKey(
                        name: "FK__Libros__Nit_edit__4E88ABD4",
                        column: x => x.Nit_editorial,
                        principalTable: "Editoriales",
                        principalColumn: "Nit");
                });

            migrationBuilder.CreateTable(
                name: "Libros_Autores",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Autor = table.Column<int>(type: "int", nullable: false),
                    Isbn = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Libros_A__3214EC2716C9D033", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LibrosAutores_Autores",
                        column: x => x.ID_Autor,
                        principalTable: "Autores",
                        principalColumn: "ID_Autor");
                    table.ForeignKey(
                        name: "FK_LibrosAutores_Libros",
                        column: x => x.Isbn,
                        principalTable: "Libros",
                        principalColumn: "Isbn");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Libros_Codigo_categoria",
                table: "Libros",
                column: "Codigo_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_Nit_editorial",
                table: "Libros",
                column: "Nit_editorial");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_Autores_ID_Autor",
                table: "Libros_Autores",
                column: "ID_Autor");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_Autores_Isbn",
                table: "Libros_Autores",
                column: "Isbn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Libros_Autores");

            migrationBuilder.DropTable(
                name: "Autores");

            migrationBuilder.DropTable(
                name: "Libros");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Editoriales");
        }
    }
}
