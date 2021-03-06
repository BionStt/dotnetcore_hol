﻿// Copyright Information
// ==================================
// SpyStore.Hol - SpyStore.Hol.Dal - 20191004033450_Final.cs
// All samples copyright Philip Japikse
// http://www.skimedic.com 2020/03/07
// See License.txt for more information
// ==================================

using Microsoft.EntityFrameworkCore.Migrations;

namespace SpyStore.Hol.Dal.EfStructures.Migrations
{
    public partial class Final : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderTotal",
                schema: "Store",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LineItemTotal",
                schema: "Store",
                table: "OrderDetails");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OrderTotal",
                schema: "Store",
                table: "Orders",
                type: "money",
                nullable: false,
                computedColumnSql: "Store.GetOrderTotal([Id])");

            migrationBuilder.AddColumn<decimal>(
                name: "LineItemTotal",
                schema: "Store",
                table: "OrderDetails",
                type: "money",
                nullable: false,
                computedColumnSql: "[Quantity]*[UnitCost]");
        }
    }
}