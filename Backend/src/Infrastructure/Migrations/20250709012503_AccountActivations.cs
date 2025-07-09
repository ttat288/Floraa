using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AccountActivations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("41cebf62-99c6-4eee-816d-6c9751475bb9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b532e596-d687-4b93-bb0a-0022778abb65"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("bcf99f83-4329-4c4d-8788-f13e5e39a7e2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("cd35235c-e23a-44a8-9a08-a770a060fa15"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("84130c3c-f1fb-4387-90c8-f572375ab620"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("8ce20d68-e117-44c2-88b4-a7679a14628b"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("c731d057-a704-450e-a884-3ae305f83a98"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("c85ead4d-6995-402c-8e79-13424f409311"));

            migrationBuilder.DeleteData(
                table: "Occasions",
                keyColumn: "Id",
                keyValue: new Guid("066f36fe-2b55-45ac-895f-b971e9169105"));

            migrationBuilder.DeleteData(
                table: "Occasions",
                keyColumn: "Id",
                keyValue: new Guid("42df7c9c-2c3e-40a2-be30-8a2e008084e3"));

            migrationBuilder.DeleteData(
                table: "Occasions",
                keyColumn: "Id",
                keyValue: new Guid("52b9ac0f-3967-429c-89a7-ad3b38623f22"));

            migrationBuilder.DeleteData(
                table: "Occasions",
                keyColumn: "Id",
                keyValue: new Guid("7735a022-ff25-494d-9332-7e3751b34413"));

            migrationBuilder.DeleteData(
                table: "Occasions",
                keyColumn: "Id",
                keyValue: new Guid("a0895d16-6b36-4eb8-84ba-7a11e98fe2a4"));

            migrationBuilder.DeleteData(
                table: "RecipientTypes",
                keyColumn: "Id",
                keyValue: new Guid("3131404d-70a4-48d6-ba66-570e5652ceec"));

            migrationBuilder.DeleteData(
                table: "RecipientTypes",
                keyColumn: "Id",
                keyValue: new Guid("57f7f69d-e52d-458c-a4d3-e0269f18de68"));

            migrationBuilder.DeleteData(
                table: "RecipientTypes",
                keyColumn: "Id",
                keyValue: new Guid("b729c0ec-2456-44be-9d8c-82360d1d27d9"));

            migrationBuilder.DeleteData(
                table: "RecipientTypes",
                keyColumn: "Id",
                keyValue: new Guid("c70e6a40-52bc-4cbb-a379-d09414fd7ff7"));

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: new Guid("29636898-1f29-42dd-8613-445582f20be0"));

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: new Guid("75b732ee-b44c-451b-ad38-774c94c129fd"));

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: new Guid("b6d43375-c4b9-4367-8583-01a13d594ffa"));

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "AccountActivations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivationToken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TempPassword = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountActivations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountActivations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("3b65b03f-1f95-4fa5-8abe-82343c9ddc19"), new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(714), null, "Orchid flowers", false, "Orchids", new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(714) },
                    { new Guid("3f52724b-c840-46e3-94c3-e192ac10a990"), new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(702), null, "Lily flowers", false, "Lilies", new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(703) },
                    { new Guid("89ff9ab6-cd5c-4429-936f-3dbd7f8150f7"), new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(696), null, "Rose flowers", false, "Roses", new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(697) },
                    { new Guid("bbe319bf-70c8-4ecc-b7c5-a085e09c97da"), new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(709), null, "Tulip flowers", false, "Tulips", new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(710) }
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0f5d8238-4d07-41af-9ab3-e1f5c54329f5"), new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(564), null, "White flowers", true, false, "White", new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(564) },
                    { new Guid("328f82a4-e145-4477-a42b-1a5fe08a9703"), new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(558), null, "Red flowers", true, false, "Red", new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(559) },
                    { new Guid("7602aa41-43d2-4840-97ee-d1268656770e"), new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(578), null, "Yellow flowers", true, false, "Yellow", new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(579) },
                    { new Guid("a5669ad2-0ee4-4f66-bb81-31900fc79659"), new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(569), null, "Pink flowers", true, false, "Pink", new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(569) }
                });

            migrationBuilder.InsertData(
                table: "Occasions",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("02929fbb-e00c-4551-9d1e-cd2c89699e2c"), new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(625), null, true, false, "Birthday", new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(625) },
                    { new Guid("5740203c-4336-4da6-8822-7fc1093878c5"), new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(648), null, true, false, "Wedding", new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(648) },
                    { new Guid("74a4d119-8e73-4a53-9bc8-69327cf630a3"), new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(630), null, true, false, "Anniversary", new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(630) },
                    { new Guid("9657e06c-34c8-42e6-81bc-9af42fe5f35c"), new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(643), null, true, false, "Mother's Day", new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(643) },
                    { new Guid("e2a8d006-53c6-43af-af7a-10d9dde6b5d2"), new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(634), null, true, false, "Valentine's Day", new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(635) }
                });

            migrationBuilder.InsertData(
                table: "RecipientTypes",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("135abc59-92bb-4be5-9536-0d5520139f81"), new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(766), null, true, false, "Colleague", new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(766) },
                    { new Guid("3361be9e-0fcc-4def-908e-079f67d1af58"), new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(761), null, true, false, "Friend", new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(762) },
                    { new Guid("99184c5a-b0d4-4ad1-8d84-b486eb4c86fa"), new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(754), null, true, false, "Family", new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(754) },
                    { new Guid("f7bf863f-d474-447a-993b-2d159af68e0e"), new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(749), null, true, false, "Lover", new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(750) }
                });

            migrationBuilder.InsertData(
                table: "Themes",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11906caa-a33e-4d07-bda8-f8df47e9239b"), new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(324), null, "Modern flower arrangements", true, false, "Modern", new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(325) },
                    { new Guid("50797280-eb43-470b-beff-76f3b626c9da"), new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(297), null, "Romantic flower arrangements", true, false, "Romantic", new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(298) },
                    { new Guid("f6d27151-60d8-4416-94b1-780f985a56e9"), new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(303), null, "Classic flower arrangements", true, false, "Classic", new DateTime(2025, 7, 9, 1, 25, 1, 601, DateTimeKind.Utc).AddTicks(304) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountActivations_ActivationToken",
                table: "AccountActivations",
                column: "ActivationToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountActivations_UserId",
                table: "AccountActivations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountActivations");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3b65b03f-1f95-4fa5-8abe-82343c9ddc19"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3f52724b-c840-46e3-94c3-e192ac10a990"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("89ff9ab6-cd5c-4429-936f-3dbd7f8150f7"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("bbe319bf-70c8-4ecc-b7c5-a085e09c97da"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("0f5d8238-4d07-41af-9ab3-e1f5c54329f5"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("328f82a4-e145-4477-a42b-1a5fe08a9703"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("7602aa41-43d2-4840-97ee-d1268656770e"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("a5669ad2-0ee4-4f66-bb81-31900fc79659"));

            migrationBuilder.DeleteData(
                table: "Occasions",
                keyColumn: "Id",
                keyValue: new Guid("02929fbb-e00c-4551-9d1e-cd2c89699e2c"));

            migrationBuilder.DeleteData(
                table: "Occasions",
                keyColumn: "Id",
                keyValue: new Guid("5740203c-4336-4da6-8822-7fc1093878c5"));

            migrationBuilder.DeleteData(
                table: "Occasions",
                keyColumn: "Id",
                keyValue: new Guid("74a4d119-8e73-4a53-9bc8-69327cf630a3"));

            migrationBuilder.DeleteData(
                table: "Occasions",
                keyColumn: "Id",
                keyValue: new Guid("9657e06c-34c8-42e6-81bc-9af42fe5f35c"));

            migrationBuilder.DeleteData(
                table: "Occasions",
                keyColumn: "Id",
                keyValue: new Guid("e2a8d006-53c6-43af-af7a-10d9dde6b5d2"));

            migrationBuilder.DeleteData(
                table: "RecipientTypes",
                keyColumn: "Id",
                keyValue: new Guid("135abc59-92bb-4be5-9536-0d5520139f81"));

            migrationBuilder.DeleteData(
                table: "RecipientTypes",
                keyColumn: "Id",
                keyValue: new Guid("3361be9e-0fcc-4def-908e-079f67d1af58"));

            migrationBuilder.DeleteData(
                table: "RecipientTypes",
                keyColumn: "Id",
                keyValue: new Guid("99184c5a-b0d4-4ad1-8d84-b486eb4c86fa"));

            migrationBuilder.DeleteData(
                table: "RecipientTypes",
                keyColumn: "Id",
                keyValue: new Guid("f7bf863f-d474-447a-993b-2d159af68e0e"));

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: new Guid("11906caa-a33e-4d07-bda8-f8df47e9239b"));

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: new Guid("50797280-eb43-470b-beff-76f3b626c9da"));

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: new Guid("f6d27151-60d8-4416-94b1-780f985a56e9"));

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("41cebf62-99c6-4eee-816d-6c9751475bb9"), new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3787), null, "Tulip flowers", false, "Tulips", new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3788) },
                    { new Guid("b532e596-d687-4b93-bb0a-0022778abb65"), new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3779), null, "Rose flowers", false, "Roses", new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3779) },
                    { new Guid("bcf99f83-4329-4c4d-8788-f13e5e39a7e2"), new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3791), null, "Orchid flowers", false, "Orchids", new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3791) },
                    { new Guid("cd35235c-e23a-44a8-9a08-a770a060fa15"), new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3783), null, "Lily flowers", false, "Lilies", new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3783) }
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("84130c3c-f1fb-4387-90c8-f572375ab620"), new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3684), null, "Red flowers", true, false, "Red", new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3684) },
                    { new Guid("8ce20d68-e117-44c2-88b4-a7679a14628b"), new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3688), null, "White flowers", true, false, "White", new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3688) },
                    { new Guid("c731d057-a704-450e-a884-3ae305f83a98"), new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3697), null, "Yellow flowers", true, false, "Yellow", new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3698) },
                    { new Guid("c85ead4d-6995-402c-8e79-13424f409311"), new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3691), null, "Pink flowers", true, false, "Pink", new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3692) }
                });

            migrationBuilder.InsertData(
                table: "Occasions",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("066f36fe-2b55-45ac-895f-b971e9169105"), new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3750), null, true, false, "Wedding", new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3751) },
                    { new Guid("42df7c9c-2c3e-40a2-be30-8a2e008084e3"), new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3747), null, true, false, "Mother's Day", new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3747) },
                    { new Guid("52b9ac0f-3967-429c-89a7-ad3b38623f22"), new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3742), null, true, false, "Valentine's Day", new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3742) },
                    { new Guid("7735a022-ff25-494d-9332-7e3751b34413"), new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3735), null, true, false, "Birthday", new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3735) },
                    { new Guid("a0895d16-6b36-4eb8-84ba-7a11e98fe2a4"), new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3738), null, true, false, "Anniversary", new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3739) }
                });

            migrationBuilder.InsertData(
                table: "RecipientTypes",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("3131404d-70a4-48d6-ba66-570e5652ceec"), new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3819), null, true, false, "Family", new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3819) },
                    { new Guid("57f7f69d-e52d-458c-a4d3-e0269f18de68"), new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3827), null, true, false, "Colleague", new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3827) },
                    { new Guid("b729c0ec-2456-44be-9d8c-82360d1d27d9"), new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3816), null, true, false, "Lover", new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3816) },
                    { new Guid("c70e6a40-52bc-4cbb-a379-d09414fd7ff7"), new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3824), null, true, false, "Friend", new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3824) }
                });

            migrationBuilder.InsertData(
                table: "Themes",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("29636898-1f29-42dd-8613-445582f20be0"), new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3504), null, "Modern flower arrangements", true, false, "Modern", new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3505) },
                    { new Guid("75b732ee-b44c-451b-ad38-774c94c129fd"), new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3484), null, "Romantic flower arrangements", true, false, "Romantic", new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3484) },
                    { new Guid("b6d43375-c4b9-4367-8583-01a13d594ffa"), new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3488), null, "Classic flower arrangements", true, false, "Classic", new DateTime(2025, 6, 30, 13, 52, 18, 143, DateTimeKind.Utc).AddTicks(3488) }
                });
        }
    }
}
