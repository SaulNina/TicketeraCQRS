using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketera.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InicialPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    ticket_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    closed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.ticket_id);
                    table.ForeignKey(
                        name: "FK_tickets_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    assigned_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "FK_user_roles_roles",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_roles_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "responses",
                columns: table => new
                {
                    response_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ticket_id = table.Column<Guid>(type: "uuid", nullable: false),
                    responder_id = table.Column<Guid>(type: "uuid", nullable: false),
                    message = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_responses", x => x.response_id);
                    table.ForeignKey(
                        name: "FK_responses_responder",
                        column: x => x.responder_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_responses_tickets",
                        column: x => x.ticket_id,
                        principalTable: "tickets",
                        principalColumn: "ticket_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_responses_responder_id",
                table: "responses",
                column: "responder_id");

            migrationBuilder.CreateIndex(
                name: "IX_responses_ticket_id",
                table: "responses",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "UQ_roles_role_name",
                table: "roles",
                column: "role_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tickets_user_id",
                table: "tickets",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_role_id",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "UQ_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_users_username",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "responses");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
