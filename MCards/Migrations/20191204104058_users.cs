using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MCards.Migrations
{
    public partial class users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardType",
                columns: table => new
                {
                    PK_CardType = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CardTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardType", x => x.PK_CardType);
                });

            migrationBuilder.CreateTable(
                name: "Expansion",
                columns: table => new
                {
                    PK_Expansion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExpansionName = table.Column<string>(nullable: true),
                    ExpansionShortName = table.Column<string>(nullable: true),
                    ExpansionCards = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expansion", x => x.PK_Expansion);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    PK_Card = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CardName = table.Column<string>(nullable: true),
                    FK_CardType = table.Column<int>(nullable: false),
                    FK_Expansion = table.Column<int>(nullable: false),
                    CardNumber = table.Column<int>(nullable: false),
                    IsFoil = table.Column<bool>(nullable: false),
                    FK_Condition = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.PK_Card);
                    table.ForeignKey(
                        name: "FK_Card_CardType_FK_CardType",
                        column: x => x.FK_CardType,
                        principalTable: "CardType",
                        principalColumn: "PK_CardType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Card_Expansion_FK_Expansion",
                        column: x => x.FK_Expansion,
                        principalTable: "Expansion",
                        principalColumn: "PK_Expansion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_FK_CardType",
                table: "Card",
                column: "FK_CardType");

            migrationBuilder.CreateIndex(
                name: "IX_Card_FK_Expansion",
                table: "Card",
                column: "FK_Expansion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CardType");

            migrationBuilder.DropTable(
                name: "Expansion");
        }
    }
}
