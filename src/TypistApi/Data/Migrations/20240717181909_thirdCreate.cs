﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypistApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class thirdCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "Name");
        }
    }
}
