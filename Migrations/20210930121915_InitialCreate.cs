using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Valsa.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customerDetails",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "981250, 1"),
                    first_name = table.Column<string>(type: "varchar(50)", nullable: false),
                    last_name = table.Column<string>(type: "varchar(50)", nullable: true),
                    gender = table.Column<string>(type: "varchar(50)", nullable: false),
                    dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    mobile_number = table.Column<string>(type: "varchar(10)", nullable: false),
                    address = table.Column<string>(type: "varchar(max)", nullable: false),
                    pan_no = table.Column<string>(type: "varchar(50)", nullable: false),
                    email = table.Column<string>(type: "varchar(50)", nullable: false),
                    starting_balance = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customerDetails", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLogins",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLogins", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLogins",
                columns: table => new
                {
                    employee_id = table.Column<int>(type: "int", nullable: false),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLogins", x => x.employee_id);
                });

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    account_number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    account_pin = table.Column<int>(type: "int", nullable: false),
                    balance = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.account_number);
                    table.ForeignKey(
                        name: "FK_accounts_customerDetails_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customerDetails",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "loans",
                columns: table => new
                {
                    loan_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1000, 1"),
                    account_number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    mobile_number = table.Column<string>(type: "varchar(10)", nullable: false),
                    amount = table.Column<decimal>(type: "money", nullable: false),
                    date_time = table.Column<DateTime>(type: "date", nullable: false),
                    EmpType = table.Column<string>(type: "varchar(30)", nullable: false),
                    income = table.Column<decimal>(type: "money", nullable: false),
                    orgName = table.Column<string>(type: "varchar(100)", nullable: false),
                    purpose = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loans", x => x.loan_id);
                    table.ForeignKey(
                        name: "FK_loans_accounts_account_number",
                        column: x => x.account_number,
                        principalTable: "accounts",
                        principalColumn: "account_number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    reference_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "4510, 1"),
                    transaction_type = table.Column<string>(type: "varchar(20)", nullable: false),
                    account_number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    account_pin = table.Column<int>(type: "int", nullable: false),
                    counterparty = table.Column<string>(type: "varchar(15)", nullable: true),
                    date_time = table.Column<DateTime>(type: "date", nullable: false),
                    updated_balance = table.Column<decimal>(type: "money", nullable: false),
                    amount = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.reference_id);
                    table.ForeignKey(
                        name: "FK_transactions_accounts_account_number",
                        column: x => x.account_number,
                        principalTable: "accounts",
                        principalColumn: "account_number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_accounts_account_number",
                table: "accounts",
                column: "account_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_accounts_customer_id",
                table: "accounts",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_customerDetails_email",
                table: "customerDetails",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customerDetails_mobile_number",
                table: "customerDetails",
                column: "mobile_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customerDetails_pan_no",
                table: "customerDetails",
                column: "pan_no",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_loans_account_number",
                table: "loans",
                column: "account_number");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_account_number",
                table: "transactions",
                column: "account_number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerLogins");

            migrationBuilder.DropTable(
                name: "EmployeeLogins");

            migrationBuilder.DropTable(
                name: "loans");

            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "customerDetails");
        }
    }
}
