using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Factory.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddOnboardingModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OnboardingProcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnboardingProcess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ITSetupModule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnboardingProcessId = table.Column<int>(type: "int", nullable: false),
                    EmailSetup = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    HardwareProvisioned = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SoftwareInstalled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AccessGranted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITSetupModule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ITSetupModule_OnboardingProcess_OnboardingProcessId",
                        column: x => x.OnboardingProcessId,
                        principalTable: "OnboardingProcess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrientationModule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnboardingProcessId = table.Column<int>(type: "int", nullable: false),
                    CompanyOrientationCompleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DepartmentOrientationCompleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    MentorAssigned = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FirstWeekCheckInCompleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrientationModule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrientationModule_OnboardingProcess_OnboardingProcessId",
                        column: x => x.OnboardingProcessId,
                        principalTable: "OnboardingProcess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreboardingModule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnboardingProcessId = table.Column<int>(type: "int", nullable: false),
                    DocumentsReceived = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ContractSigned = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    BackgroundCheckCompleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    WelcomeEmailSent = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreboardingModule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreboardingModule_OnboardingProcess_OnboardingProcessId",
                        column: x => x.OnboardingProcessId,
                        principalTable: "OnboardingProcess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingModule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnboardingProcessId = table.Column<int>(type: "int", nullable: false),
                    ComplianceTraining = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SkillsTraining = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SystemsTraining = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SecurityTraining = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingModule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingModule_OnboardingProcess_OnboardingProcessId",
                        column: x => x.OnboardingProcessId,
                        principalTable: "OnboardingProcess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ITSetupModule_OnboardingProcessId",
                table: "ITSetupModule",
                column: "OnboardingProcessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrientationModule_OnboardingProcessId",
                table: "OrientationModule",
                column: "OnboardingProcessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreboardingModule_OnboardingProcessId",
                table: "PreboardingModule",
                column: "OnboardingProcessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingModule_OnboardingProcessId",
                table: "TrainingModule",
                column: "OnboardingProcessId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ITSetupModule");

            migrationBuilder.DropTable(
                name: "OrientationModule");

            migrationBuilder.DropTable(
                name: "PreboardingModule");

            migrationBuilder.DropTable(
                name: "TrainingModule");

            migrationBuilder.DropTable(
                name: "OnboardingProcess");
        }
    }
}
