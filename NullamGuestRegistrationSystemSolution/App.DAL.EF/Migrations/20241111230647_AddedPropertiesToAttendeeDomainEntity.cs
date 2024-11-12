using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddedPropertiesToAttendeeDomainEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttendeeType",
                table: "Attendees",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyAdditionalInfo",
                table: "Attendees",
                type: "TEXT",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Attendees",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GivenName",
                table: "Attendees",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPeopleFromCompany",
                table: "Attendees",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                table: "Attendees",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PersonAdditionalInfo",
                table: "Attendees",
                type: "TEXT",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalIdentifier",
                table: "Attendees",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RegistryCode",
                table: "Attendees",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SurName",
                table: "Attendees",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_PaymentMethodId",
                table: "Attendees",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendees_PaymentMethods_PaymentMethodId",
                table: "Attendees",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendees_PaymentMethods_PaymentMethodId",
                table: "Attendees");

            migrationBuilder.DropIndex(
                name: "IX_Attendees_PaymentMethodId",
                table: "Attendees");

            migrationBuilder.DropColumn(
                name: "AttendeeType",
                table: "Attendees");

            migrationBuilder.DropColumn(
                name: "CompanyAdditionalInfo",
                table: "Attendees");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Attendees");

            migrationBuilder.DropColumn(
                name: "GivenName",
                table: "Attendees");

            migrationBuilder.DropColumn(
                name: "NumberOfPeopleFromCompany",
                table: "Attendees");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "Attendees");

            migrationBuilder.DropColumn(
                name: "PersonAdditionalInfo",
                table: "Attendees");

            migrationBuilder.DropColumn(
                name: "PersonalIdentifier",
                table: "Attendees");

            migrationBuilder.DropColumn(
                name: "RegistryCode",
                table: "Attendees");

            migrationBuilder.DropColumn(
                name: "SurName",
                table: "Attendees");
        }
    }
}
