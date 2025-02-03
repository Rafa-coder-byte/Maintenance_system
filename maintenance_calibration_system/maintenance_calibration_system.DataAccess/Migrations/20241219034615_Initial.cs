using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maintenance_calibration_system.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AlphanumericCode = table.Column<string>(type: "TEXT", nullable: false),
                    Magnitude_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Magnitude_UnitofMagnitude = table.Column<string>(type: "TEXT", nullable: false),
                    Manufacturer = table.Column<string>(type: "TEXT", nullable: false),
                    EquipmentType = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    CodeControl = table.Column<string>(type: "TEXT", nullable: true),
                    SignalControl = table.Column<int>(type: "INTEGER", nullable: true),
                    Maintenance = table.Column<bool>(type: "INTEGER", nullable: true),
                    PrincipleOperation = table.Column<string>(type: "TEXT", nullable: true),
                    Calibrated = table.Column<bool>(type: "INTEGER", nullable: true),
                    Protocol = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EquipmentElement = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    ExecutionDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateActivity = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NameTechnician = table.Column<string>(type: "TEXT", nullable: false),
                    ActivityType = table.Column<string>(type: "TEXT", maxLength: 21, nullable: false),
                    NameCertificateAuthority = table.Column<string>(type: "TEXT", nullable: true),
                    Calibration_PlanningId = table.Column<Guid>(type: "TEXT", nullable: true),
                    TypeMaintenance = table.Column<int>(type: "INTEGER", nullable: true),
                    PlanningId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceActivities_Planes_Calibration_PlanningId",
                        column: x => x.Calibration_PlanningId,
                        principalTable: "Planes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaintenanceActivities_Planes_PlanningId",
                        column: x => x.PlanningId,
                        principalTable: "Planes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceActuator",
                columns: table => new
                {
                    MaintenanceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ActuatorId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceActuator", x => new { x.MaintenanceId, x.ActuatorId });
                    table.ForeignKey(
                        name: "FK_MaintenanceActuator_Equipments_ActuatorId",
                        column: x => x.ActuatorId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaintenanceActuator_MaintenanceActivities_MaintenanceId",
                        column: x => x.MaintenanceId,
                        principalTable: "MaintenanceActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SensorCalibration",
                columns: table => new
                {
                    SensorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CalibrationId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorCalibration", x => new { x.SensorId, x.CalibrationId });
                    table.ForeignKey(
                        name: "FK_SensorCalibration_Equipments_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SensorCalibration_MaintenanceActivities_CalibrationId",
                        column: x => x.CalibrationId,
                        principalTable: "MaintenanceActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceActivities_Calibration_PlanningId",
                table: "MaintenanceActivities",
                column: "Calibration_PlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceActivities_PlanningId",
                table: "MaintenanceActivities",
                column: "PlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceActuator_ActuatorId",
                table: "MaintenanceActuator",
                column: "ActuatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SensorCalibration_CalibrationId",
                table: "SensorCalibration",
                column: "CalibrationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceActuator");

            migrationBuilder.DropTable(
                name: "SensorCalibration");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "MaintenanceActivities");

            migrationBuilder.DropTable(
                name: "Planes");
        }
    }
}
