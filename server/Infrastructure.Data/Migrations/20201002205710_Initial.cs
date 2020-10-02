using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeacherCourses",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    CourseId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Cost = table.Column<string>(maxLength: 7, nullable: false),
                    Actived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherCourses", x => new { x.CourseId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Actived = table.Column<bool>(nullable: false),
                    TeacherCourseCourseId = table.Column<Guid>(nullable: true),
                    TeacherCourseUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_TeacherCourses_TeacherCourseCourseId_TeacherCourseUserId",
                        columns: x => new { x.TeacherCourseCourseId, x.TeacherCourseUserId },
                        principalTable: "TeacherCourses",
                        principalColumns: new[] { "CourseId", "UserId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TeacherCourseId = table.Column<Guid>(nullable: false),
                    WeekDay = table.Column<int>(nullable: false),
                    From = table.Column<int>(nullable: false),
                    To = table.Column<int>(nullable: false),
                    TeacherCourseCourseId = table.Column<Guid>(nullable: true),
                    TeacherCourseUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_TeacherCourses_TeacherCourseCourseId_TeacherCourseUserId",
                        columns: x => new { x.TeacherCourseCourseId, x.TeacherCourseUserId },
                        principalTable: "TeacherCourses",
                        principalColumns: new[] { "CourseId", "UserId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(maxLength: 40, nullable: true),
                    Password = table.Column<string>(maxLength: 40, nullable: true),
                    Name = table.Column<string>(maxLength: 120, nullable: true),
                    Photo = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Actived = table.Column<bool>(nullable: false),
                    Whatsapp = table.Column<string>(nullable: true),
                    Facebook = table.Column<string>(maxLength: 255, nullable: true),
                    Bio = table.Column<string>(nullable: true),
                    TeacherCourseCourseId = table.Column<Guid>(nullable: true),
                    TeacherCourseUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_TeacherCourses_TeacherCourseCourseId_TeacherCourseUserId",
                        columns: x => new { x.TeacherCourseCourseId, x.TeacherCourseUserId },
                        principalTable: "TeacherCourses",
                        principalColumns: new[] { "CourseId", "UserId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Connections_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Connections_UserId",
                table: "Connections",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherCourseCourseId_TeacherCourseUserId",
                table: "Courses",
                columns: new[] { "TeacherCourseCourseId", "TeacherCourseUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TeacherCourseCourseId_TeacherCourseUserId",
                table: "Schedules",
                columns: new[] { "TeacherCourseCourseId", "TeacherCourseUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_TeacherCourseCourseId_TeacherCourseUserId",
                table: "Users",
                columns: new[] { "TeacherCourseCourseId", "TeacherCourseUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherCourses_Courses_CourseId",
                table: "TeacherCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_TeacherCourses_TeacherCourseCourseId_TeacherCourseUserId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "TeacherCourses");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
