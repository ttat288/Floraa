using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4a5aabd6-7bb5-4266-a96b-e882e51f8c36"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("625b59ea-0559-45db-8d7f-f8b64aec1128"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a5e7d70b-1376-4a9a-b044-8daabff613eb"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f8ff8052-c347-4139-8b0c-4bba7e7a1d96"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("136e785c-0602-4f3e-9134-913a70c0e5be"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("475736eb-efea-4a24-bf96-540931cdd918"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("50ab314f-a0ba-4237-8c51-5e1348a225d5"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("802f5bd9-d43b-47b2-ac84-309cb01ddc1d"));

            migrationBuilder.DeleteData(
                table: "Occasions",
                keyColumn: "Id",
                keyValue: new Guid("29967f12-d87d-4e6f-beee-0864c02f0298"));

            migrationBuilder.DeleteData(
                table: "Occasions",
                keyColumn: "Id",
                keyValue: new Guid("5680ea95-b9d9-4ed5-8c15-2c8048dc49b9"));

            migrationBuilder.DeleteData(
                table: "Occasions",
                keyColumn: "Id",
                keyValue: new Guid("6f01276b-6357-4af7-9bd9-2a4ab4a4eba4"));

            migrationBuilder.DeleteData(
                table: "Occasions",
                keyColumn: "Id",
                keyValue: new Guid("afefea5d-15c0-47a5-8bf2-5a4d1a5dc73a"));

            migrationBuilder.DeleteData(
                table: "Occasions",
                keyColumn: "Id",
                keyValue: new Guid("c5c090fa-12f8-42c4-8a09-946ca5a28eb3"));

            migrationBuilder.DeleteData(
                table: "RecipientTypes",
                keyColumn: "Id",
                keyValue: new Guid("1c50f921-6590-442c-9931-9f9c17577a31"));

            migrationBuilder.DeleteData(
                table: "RecipientTypes",
                keyColumn: "Id",
                keyValue: new Guid("9ab70068-646f-49a4-a85f-03c32de5feec"));

            migrationBuilder.DeleteData(
                table: "RecipientTypes",
                keyColumn: "Id",
                keyValue: new Guid("d10e5df7-7f14-410b-a206-bf289e044d23"));

            migrationBuilder.DeleteData(
                table: "RecipientTypes",
                keyColumn: "Id",
                keyValue: new Guid("e40485bc-921f-4985-87b2-63cabc3db9af"));

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: new Guid("2893e30a-1b9b-4a65-9070-290506286609"));

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: new Guid("62932c29-1d14-4ce2-b620-3c48293124a2"));

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: new Guid("7f68e77f-30dc-4613-a104-a99256da065a"));

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "UserVouchers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserVouchers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "SubProducts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SubProducts");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "StorageLogs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "StorageLogs");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ShopCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ShopCategories");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ProductTargets");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductTargets");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ProductOccasions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductOccasions");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ActivityLogs");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "UserVouchers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserVouchers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "SubProducts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SubProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Storages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Storages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "StorageLogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "StorageLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ShopCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ShopCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RefreshTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ProductTargets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductTargets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ProductOccasions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductOccasions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ProductCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Payments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "OrderDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OrderDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Notifications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Carts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Carts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ActivityLogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ActivityLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("4a5aabd6-7bb5-4266-a96b-e882e51f8c36"), new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8576), null, "Rose flowers", false, "Roses", new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8576) },
                    { new Guid("625b59ea-0559-45db-8d7f-f8b64aec1128"), new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8578), null, "Lily flowers", false, "Lilies", new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8579) },
                    { new Guid("a5e7d70b-1376-4a9a-b044-8daabff613eb"), new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8586), null, "Orchid flowers", false, "Orchids", new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8586) },
                    { new Guid("f8ff8052-c347-4139-8b0c-4bba7e7a1d96"), new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8581), null, "Tulip flowers", false, "Tulips", new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8582) }
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("136e785c-0602-4f3e-9134-913a70c0e5be"), new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8499), null, "White flowers", true, false, "White", new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8499) },
                    { new Guid("475736eb-efea-4a24-bf96-540931cdd918"), new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8495), null, "Red flowers", true, false, "Red", new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8496) },
                    { new Guid("50ab314f-a0ba-4237-8c51-5e1348a225d5"), new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8504), null, "Yellow flowers", true, false, "Yellow", new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8504) },
                    { new Guid("802f5bd9-d43b-47b2-ac84-309cb01ddc1d"), new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8501), null, "Pink flowers", true, false, "Pink", new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8502) }
                });

            migrationBuilder.InsertData(
                table: "Occasions",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("29967f12-d87d-4e6f-beee-0864c02f0298"), new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8547), null, true, false, "Wedding", new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8548) },
                    { new Guid("5680ea95-b9d9-4ed5-8c15-2c8048dc49b9"), new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8537), null, true, false, "Anniversary", new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8538) },
                    { new Guid("6f01276b-6357-4af7-9bd9-2a4ab4a4eba4"), new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8540), null, true, false, "Valentine's Day", new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8541) },
                    { new Guid("afefea5d-15c0-47a5-8bf2-5a4d1a5dc73a"), new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8534), null, true, false, "Birthday", new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8535) },
                    { new Guid("c5c090fa-12f8-42c4-8a09-946ca5a28eb3"), new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8543), null, true, false, "Mother's Day", new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8544) }
                });

            migrationBuilder.InsertData(
                table: "RecipientTypes",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("1c50f921-6590-442c-9931-9f9c17577a31"), new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8619), null, true, false, "Colleague", new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8619) },
                    { new Guid("9ab70068-646f-49a4-a85f-03c32de5feec"), new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8612), null, true, false, "Family", new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8612) },
                    { new Guid("d10e5df7-7f14-410b-a206-bf289e044d23"), new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8614), null, true, false, "Friend", new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8615) },
                    { new Guid("e40485bc-921f-4985-87b2-63cabc3db9af"), new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8609), null, true, false, "Lover", new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8609) }
                });

            migrationBuilder.InsertData(
                table: "Themes",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("2893e30a-1b9b-4a65-9070-290506286609"), new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8311), null, "Romantic flower arrangements", true, false, "Romantic", new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8311) },
                    { new Guid("62932c29-1d14-4ce2-b620-3c48293124a2"), new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8314), null, "Classic flower arrangements", true, false, "Classic", new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8315) },
                    { new Guid("7f68e77f-30dc-4613-a104-a99256da065a"), new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8317), null, "Modern flower arrangements", true, false, "Modern", new DateTime(2025, 6, 30, 13, 3, 31, 619, DateTimeKind.Utc).AddTicks(8317) }
                });
        }
    }
}
