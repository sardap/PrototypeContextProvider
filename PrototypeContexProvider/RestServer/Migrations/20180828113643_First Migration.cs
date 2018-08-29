using Microsoft.EntityFrameworkCore.Migrations;

namespace RestServer.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompositeContex",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompositeContex", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompositeContexJson",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompositeContexJson", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contex",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Interval = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contex", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataConsumer",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataConsumer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivacyOblgations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Purpose = table.Column<string>(nullable: true),
                    Granularity = table.Column<string>(nullable: true),
                    Anonymisation = table.Column<string>(nullable: true),
                    Notifaction = table.Column<string>(nullable: true),
                    Accounting = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivacyOblgations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResharingObligations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CanShare = table.Column<bool>(nullable: false),
                    Cardinality = table.Column<int>(nullable: false),
                    Recurring = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResharingObligations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entry",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContexId = table.Column<long>(nullable: true),
                    Glue = table.Column<int>(nullable: false),
                    Not = table.Column<bool>(nullable: false),
                    CompositeContexJsonId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entry_CompositeContexJson_CompositeContexJsonId",
                        column: x => x.CompositeContexJsonId,
                        principalTable: "CompositeContexJson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entry_Contex_ContexId",
                        column: x => x.ContexId,
                        principalTable: "Contex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Polcies",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Author = table.Column<string>(nullable: true),
                    Proity = table.Column<int>(nullable: false),
                    Decision = table.Column<string>(nullable: true),
                    DataConsumerId = table.Column<long>(nullable: true),
                    CompositeContexId = table.Column<long>(nullable: true),
                    JsonCompositeContexId = table.Column<long>(nullable: true),
                    PrivacyOblgationsId = table.Column<long>(nullable: true),
                    ResharingObligationsId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polcies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Polcies_CompositeContex_CompositeContexId",
                        column: x => x.CompositeContexId,
                        principalTable: "CompositeContex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Polcies_DataConsumer_DataConsumerId",
                        column: x => x.DataConsumerId,
                        principalTable: "DataConsumer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Polcies_CompositeContexJson_JsonCompositeContexId",
                        column: x => x.JsonCompositeContexId,
                        principalTable: "CompositeContexJson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Polcies_PrivacyOblgations_PrivacyOblgationsId",
                        column: x => x.PrivacyOblgationsId,
                        principalTable: "PrivacyOblgations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Polcies_ResharingObligations_ResharingObligationsId",
                        column: x => x.ResharingObligationsId,
                        principalTable: "ResharingObligations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entry_CompositeContexJsonId",
                table: "Entry",
                column: "CompositeContexJsonId");

            migrationBuilder.CreateIndex(
                name: "IX_Entry_ContexId",
                table: "Entry",
                column: "ContexId");

            migrationBuilder.CreateIndex(
                name: "IX_Polcies_CompositeContexId",
                table: "Polcies",
                column: "CompositeContexId");

            migrationBuilder.CreateIndex(
                name: "IX_Polcies_DataConsumerId",
                table: "Polcies",
                column: "DataConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_Polcies_JsonCompositeContexId",
                table: "Polcies",
                column: "JsonCompositeContexId");

            migrationBuilder.CreateIndex(
                name: "IX_Polcies_PrivacyOblgationsId",
                table: "Polcies",
                column: "PrivacyOblgationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Polcies_ResharingObligationsId",
                table: "Polcies",
                column: "ResharingObligationsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entry");

            migrationBuilder.DropTable(
                name: "Polcies");

            migrationBuilder.DropTable(
                name: "Contex");

            migrationBuilder.DropTable(
                name: "CompositeContex");

            migrationBuilder.DropTable(
                name: "DataConsumer");

            migrationBuilder.DropTable(
                name: "CompositeContexJson");

            migrationBuilder.DropTable(
                name: "PrivacyOblgations");

            migrationBuilder.DropTable(
                name: "ResharingObligations");
        }
    }
}
