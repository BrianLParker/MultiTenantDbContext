// Copyright (c) 2023, Brian Parker. All Rights Reserved.
// 20231202070251_CreateInitialTenantSchema.cs licensed under the MIT License.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiTenantDbContext.Data.Migrations.TenantContext
{
    /// <inheritdoc />
    public partial class CreateInitialTenantSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SomeItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SomeItems", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SomeItems");
        }
    }
}
