using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CdService.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MusicGenres",
                columns: table => new
                {
                    IdMusicGenre = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameMusicGenre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MusicalG__C2A4358176EF3AF4", x => x.IdMusicGenre);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    IdGroup = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameGroup = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MusicGenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Groups__32DFFDB3F74504DB", x => x.IdGroup);
                    table.ForeignKey(
                        name: "FK_Groups_MusicalGenres",
                        column: x => x.MusicGenreId,
                        principalTable: "MusicGenres",
                        principalColumn: "IdMusicGenre");
                });

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    IdRecord = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleRecord = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    YearOfPublication = table.Column<int>(type: "int", nullable: false),
                    ImageRecord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Discontinued = table.Column<bool>(type: "bit", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Records__356CCF9A247285E1", x => x.IdRecord);
                    table.ForeignKey(
                        name: "FK_Records_Groups",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "IdGroup");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_MusicGenreId",
                table: "Groups",
                column: "MusicGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_GroupId",
                table: "Records",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Records");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "MusicGenres");
        }
    }
}
