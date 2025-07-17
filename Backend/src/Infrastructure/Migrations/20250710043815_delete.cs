using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class delete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("7d85dbcf-f89d-43fe-aad6-076b43ae4b8a"), new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6936), null, "Rose flowers", false, "Roses", new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6937) },
                    { new Guid("813098e7-a7b3-4f5e-a59f-56c98d0797b9"), new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6940), null, "Lily flowers", false, "Lilies", new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6940) },
                    { new Guid("a030899a-a604-4e3c-8f04-e7c7157e1cf1"), new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6948), null, "Orchid flowers", false, "Orchids", new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6948) },
                    { new Guid("f782967c-299e-4303-9f56-5ac9d6e98b98"), new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6945), null, "Tulip flowers", false, "Tulips", new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6945) }
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("293290d3-121e-4473-9b6e-107ea862f332"), new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6820), null, "White flowers", true, false, "White", new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6821) },
                    { new Guid("3f95720c-37ce-401e-b523-524a37b99ff8"), new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6830), null, "Pink flowers", true, false, "Pink", new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6830) },
                    { new Guid("4a839bb6-4d85-4ff8-8df2-774523bf923e"), new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6835), null, "Yellow flowers", true, false, "Yellow", new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6835) },
                    { new Guid("871f4f73-b579-4a30-a88b-c913793822cc"), new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6817), null, "Red flowers", true, false, "Red", new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6817) }
                });

            migrationBuilder.InsertData(
                table: "Occasions",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("135f9319-951b-4804-8c4b-5ee2c648a688"), new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6875), null, true, false, "Birthday", new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6876) },
                    { new Guid("22ecddea-1b2a-4e8e-ac86-b43aaa4787a9"), new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6878), null, true, false, "Anniversary", new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6879) },
                    { new Guid("94d996bb-0a83-495d-a727-6dee1d9eaaff"), new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6881), null, true, false, "Valentine's Day", new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6882) },
                    { new Guid("cf97d038-ae0d-447e-962c-3282522385e8"), new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6886), null, true, false, "Mother's Day", new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6887) },
                    { new Guid("dc8e4f3d-f624-452f-aadd-2331f1f6eb8d"), new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6889), null, true, false, "Wedding", new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6890) }
                });

            migrationBuilder.InsertData(
                table: "RecipientTypes",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0204d1d5-635f-49cd-b325-dc84bc5eb2c3"), new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6999), null, true, false, "Lover", new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6999) },
                    { new Guid("8af4136a-709c-4f30-91ba-1a76eaaaba21"), new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(7008), null, true, false, "Friend", new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(7008) },
                    { new Guid("9a68752c-7b8f-42bc-9b92-84728213c84d"), new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(7002), null, true, false, "Family", new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(7002) },
                    { new Guid("e0424738-3913-432c-bd0b-ffa66db1f4aa"), new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(7010), null, true, false, "Colleague", new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(7011) }
                });

            migrationBuilder.InsertData(
                table: "Themes",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("83ac3c95-56cc-4b3d-bacd-d020c9a9e145"), new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6600), null, "Classic flower arrangements", true, false, "Classic", new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6600) },
                    { new Guid("b750f6be-4fd8-4e59-9cc5-a9abcbbc5dde"), new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6612), null, "Modern flower arrangements", true, false, "Modern", new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6613) },
                    { new Guid("bb837dda-08b1-4129-bf53-a7b19a5d5c4e"), new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6596), null, "Romantic flower arrangements", true, false, "Romantic", new DateTime(2025, 7, 10, 4, 38, 13, 257, DateTimeKind.Utc).AddTicks(6596) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedAt", "DeletedAt", "Email", "IsActive", "IsDeleted", "Name", "PasswordHash", "PhoneNumber", "RefreshToken", "RefreshTokenExpiryTime", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("5314b725-9c1a-43eb-9f5a-c59dcb4233e3"), null, new DateTime(2025, 7, 10, 4, 38, 13, 392, DateTimeKind.Utc).AddTicks(3355), null, "tranthamanhtoan288@gmail.com", true, false, "System Administrator", "$2a$11$1uFmwhKsgptdOf.Or5mlf.C6rQukKG3ZLyIw9zeTjC7rAotp0Imli", "0123456789", null, null, 5, new DateTime(2025, 7, 10, 4, 38, 13, 392, DateTimeKind.Utc).AddTicks(3361) },
                    { new Guid("9d43d7b4-abaa-4baa-a9a4-c366d7cc4b25"), null, new DateTime(2025, 7, 10, 4, 38, 13, 530, DateTimeKind.Utc).AddTicks(1681), null, "tranthamanhtoan47@gmail.com", true, false, "Super Manager", "$2a$11$r2bMoq98Y4h5hNuiXVM7k.HdS..K0CG2w5VQuu8N6UHXmjf/Kw06m", "0987654321", null, null, 4, new DateTime(2025, 7, 10, 4, 38, 13, 530, DateTimeKind.Utc).AddTicks(1687) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7d85dbcf-f89d-43fe-aad6-076b43ae4b8a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("813098e7-a7b3-4f5e-a59f-56c98d0797b9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a030899a-a604-4e3c-8f04-e7c7157e1cf1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f782967c-299e-4303-9f56-5ac9d6e98b98"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("293290d3-121e-4473-9b6e-107ea862f332"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("3f95720c-37ce-401e-b523-524a37b99ff8"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("4a839bb6-4d85-4ff8-8df2-774523bf923e"));

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: new Guid("871f4f73-b579-4a30-a88b-c913793822cc"));

            migrationBuilder.DeleteData(
                table: "Occasions",
                keyColumn: "Id",
                keyValue: new Guid("135f9319-951b-4804-8c4b-5ee2c648a688"));

            migrationBuilder.DeleteData(
                table: "Occasions",
                keyColumn: "Id",
                keyValue: new Guid("22ecddea-1b2a-4e8e-ac86-b43aaa4787a9"));

            migrationBuilder.DeleteData(
                table: "Occasions",
                keyColumn: "Id",
                keyValue: new Guid("94d996bb-0a83-495d-a727-6dee1d9eaaff"));

            migrationBuilder.DeleteData(
                table: "Occasions",
                keyColumn: "Id",
                keyValue: new Guid("cf97d038-ae0d-447e-962c-3282522385e8"));

            migrationBuilder.DeleteData(
                table: "Occasions",
                keyColumn: "Id",
                keyValue: new Guid("dc8e4f3d-f624-452f-aadd-2331f1f6eb8d"));

            migrationBuilder.DeleteData(
                table: "RecipientTypes",
                keyColumn: "Id",
                keyValue: new Guid("0204d1d5-635f-49cd-b325-dc84bc5eb2c3"));

            migrationBuilder.DeleteData(
                table: "RecipientTypes",
                keyColumn: "Id",
                keyValue: new Guid("8af4136a-709c-4f30-91ba-1a76eaaaba21"));

            migrationBuilder.DeleteData(
                table: "RecipientTypes",
                keyColumn: "Id",
                keyValue: new Guid("9a68752c-7b8f-42bc-9b92-84728213c84d"));

            migrationBuilder.DeleteData(
                table: "RecipientTypes",
                keyColumn: "Id",
                keyValue: new Guid("e0424738-3913-432c-bd0b-ffa66db1f4aa"));

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: new Guid("83ac3c95-56cc-4b3d-bacd-d020c9a9e145"));

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: new Guid("b750f6be-4fd8-4e59-9cc5-a9abcbbc5dde"));

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: new Guid("bb837dda-08b1-4129-bf53-a7b19a5d5c4e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5314b725-9c1a-43eb-9f5a-c59dcb4233e3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9d43d7b4-abaa-4baa-a9a4-c366d7cc4b25"));

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
        }
    }
}
