﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RefreshTokensWithPolicy.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.InsertData(
					table: "AspNetRoles",
					columns: ["Id", "Name", "NormalizedName", "ConcurrencyStamp"],
					values:
					[
						Guid.NewGuid().ToString(),
						"User",
						"USER",
						Guid.NewGuid().ToString(),
					]
				);
			migrationBuilder.InsertData(
					table: "AspNetRoles",
					columns: ["Id", "Name", "NormalizedName", "ConcurrencyStamp"],
					values:
					[
						Guid.NewGuid().ToString(),
						"Admin",
						"ADMIN",
						Guid.NewGuid().ToString(),
					]
				);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("DELETE FROM [AspNetRoles]");

		}
	}
}
