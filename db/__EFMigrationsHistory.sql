create table __EFMigrationsHistory
(
    MigrationId    nvarchar(150) not null
        constraint PK___EFMigrationsHistory
            primary key,
    ProductVersion nvarchar(32)  not null
)
go

INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240512142918_updateAddress', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240512143750_updateAddress2', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240517032045_MiniUpdate', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240517121527_FixNullable', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240518055637_FixUniqueCustomerV2', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240518063718_FixLengthAddress', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240518073252_AddResetCode', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240519050344_AddAuditable+RemoveRefreshToken', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240522072942_FixPassword+FixNullableName', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240525124816_AddIsBanned', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240527051719_AddCollation', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240527132755_AddThumbnailLogo', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240530133129_AddIndexFixCollation', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240603072217_RemoveIsActiveProductImage', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240604103032_AddOrderCode', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240605085420_RemoveRedundancy+UpdatePriceType', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240607021447_AddProductReview', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240610063102_AddThumbnail', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240611135234_AddDistrictIdAndWardCode_Order', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240614092059_PreOrderProduct', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240614151036_ChangeWardCodeType', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240615041114_AddAuditPreorderProduct', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240615100041_AddPost', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240615181248_AddShippingCode_Order', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240618062542_AddParentIdCategory', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240619044645_AddEmail', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240626012738_RemoveVoucher+Point', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240629073740_AddIsActiveImage', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240710031406_Add IsPreorder For Order', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240711084533_Add Voucher + Point + OrderLog + Report', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240711090641_Add Thumbnail for Post', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240711152845_Add MinPriceCondition for Voucher', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240712045421_AddVoucherNPointAmountInOrder', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240712160736_Add ProductId for Report', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240717165806_FixNaming', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240720101851_Add Row Version Product', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240720115516_Add Row Version Order', N'8.0.4');
INSERT INTO yumilk.dbo.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20240720120613_Change OrderCode to TransactionCode', N'8.0.4');
