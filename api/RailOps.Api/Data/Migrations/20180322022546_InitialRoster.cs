using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RailOps.Api.Data.Migrations
{
    public partial class InitialRoster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EngineModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EngineTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoadTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Comments = table.Column<string>(nullable: true),
                    Length = table.Column<int>(nullable: false),
                    RoadId = table.Column<int>(nullable: false),
                    RoadNumber = table.Column<string>(nullable: true),
                    CarTypeId = table.Column<int>(nullable: false),
                    IsPassenger = table.Column<bool>(nullable: false),
                    IsCaboose = table.Column<bool>(nullable: false),
                    IsFRED = table.Column<bool>(nullable: false),
                    IsUtility = table.Column<bool>(nullable: false),
                    IsHazardous = table.Column<bool>(nullable: false),
                    WeightOunces = table.Column<decimal>(nullable: false),
                    WeightTons = table.Column<int>(nullable: false),
                    Color = table.Column<string>(nullable: true),
                    LoadTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_CarTypes_CarTypeId",
                        column: x => x.CarTypeId,
                        principalTable: "CarTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_LoadTypes_LoadTypeId",
                        column: x => x.LoadTypeId,
                        principalTable: "LoadTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_Roads_RoadId",
                        column: x => x.RoadId,
                        principalTable: "Roads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Engines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Comments = table.Column<string>(nullable: true),
                    Length = table.Column<int>(nullable: false),
                    RoadId = table.Column<int>(nullable: false),
                    RoadNumber = table.Column<string>(nullable: true),
                    ModelId = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    WeightTons = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Engines_EngineModels_ModelId",
                        column: x => x.ModelId,
                        principalTable: "EngineModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Engines_Roads_RoadId",
                        column: x => x.RoadId,
                        principalTable: "Roads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Engines_EngineTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "EngineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarTypeId",
                table: "Cars",
                column: "CarTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_LoadTypeId",
                table: "Cars",
                column: "LoadTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_RoadId",
                table: "Cars",
                column: "RoadId");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_ModelId",
                table: "Engines",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_RoadId",
                table: "Engines",
                column: "RoadId");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_TypeId",
                table: "Engines",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Engines");

            migrationBuilder.DropTable(
                name: "CarTypes");

            migrationBuilder.DropTable(
                name: "LoadTypes");

            migrationBuilder.DropTable(
                name: "EngineModels");

            migrationBuilder.DropTable(
                name: "Roads");

            migrationBuilder.DropTable(
                name: "EngineTypes");
        }
    }
}
