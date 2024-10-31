using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class Alg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OsavõtumaksuMaksmiseViisid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OsavõtumaksuMaksmiseViisiNimetus = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsavõtumaksuMaksmiseViisid", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Üritused",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ÜrituseNimi = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Toimumisaeg = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Koht = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Lisainfo = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OsavõtjateArv = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Üritused", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Osavõtjad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OsavõtjaTüüp = table.Column<int>(type: "int", nullable: false),
                    Eesnimi = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Perekonnanimi = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Isikukood = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    EraisikuLisainfo = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    EttevõtteJuriidilineNimi = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    EttevõtteRegistrikood = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    EttevõttestTulevateOsavõtjateArv = table.Column<int>(type: "int", nullable: false),
                    OsavotumaksuMaksmiseViisId = table.Column<int>(type: "int", nullable: false),
                    ÜritusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osavõtjad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Osavõtjad_OsavõtumaksuMaksmiseViisid_OsavotumaksuMaksmiseViisId",
                        column: x => x.OsavotumaksuMaksmiseViisId,
                        principalTable: "OsavõtumaksuMaksmiseViisid",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Osavõtjad_Üritused_ÜritusId",
                        column: x => x.ÜritusId,
                        principalTable: "Üritused",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Osavõtjad_OsavotumaksuMaksmiseViisId",
                table: "Osavõtjad",
                column: "OsavotumaksuMaksmiseViisId");

            migrationBuilder.CreateIndex(
                name: "IX_Osavõtjad_ÜritusId",
                table: "Osavõtjad",
                column: "ÜritusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Osavõtjad");

            migrationBuilder.DropTable(
                name: "OsavõtumaksuMaksmiseViisid");

            migrationBuilder.DropTable(
                name: "Üritused");
        }
    }
}
