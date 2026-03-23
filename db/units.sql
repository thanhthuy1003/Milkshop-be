create table units
(
    Id          int identity
        constraint PK_units
            primary key,
    Name        nvarchar(255)                                   collate Latin1_General_CI_AS not null ,
    Description nvarchar(2000),
    is_active   bit       default CONVERT([bit], 0)             not null,
    created_at  datetime2 default '0001-01-01T00:00:00.0000000' not null,
    deleted_at  datetime2,
    modified_at datetime2,
    gram        int       default 0                             not null
)
go

create index IX_units_Name
    on units (Name)
go

INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'800G', N'5', 1, N'2024-06-17 12:57:13.0000000', null, N'2024-07-13 15:06:15.2154760', 800);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'90G', null, 1, N'2024-06-17 12:57:16.0000000', null, null, 90);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'850G', null, 1, N'2024-06-17 12:57:51.0000000', null, null, 850);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'1,6KG', null, 1, N'2024-06-17 12:58:19.0000000', null, null, 1600);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'1,4KG', null, 1, N'2024-06-17 12:59:16.0000000', null, null, 1400);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'900G', null, 1, N'2024-06-17 12:59:56.0000000', null, null, 900);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'400G', null, 1, N'2024-06-17 13:00:03.0000000', null, null, 400);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'200G', null, 1, N'2024-06-17 13:01:00.0000000', null, null, 200);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'50G', null, 1, N'2024-06-17 13:01:20.0000000', null, null, 50);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'448G', null, 1, N'2024-06-19 11:23:22.0000000', null, null, 448);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'672G', null, 1, N'2024-06-19 11:25:06.0000000', null, null, 672);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'3,2KG', null, 1, N'2024-06-19 11:32:27.0000000', null, null, 3200);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'Thùng 12 Lốc (110ml)', N'48 hộp x 110 ml', 1, N'2024-06-19 11:40:24.0000000', null, null, 5438);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'27G', null, 1, N'2024-06-22 17:47:04.0000000', null, N'2024-06-28 00:04:01.2418310', 27);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'560G', null, 1, N'2024-06-22 17:51:22.0000000', null, null, 560);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'540G', null, 1, N'2024-06-22 18:00:08.0000000', null, null, 540);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'Thùng 6 Lốc (180ml)', N'24 hộp x 180ml', 1, N'2024-06-22 18:11:46.0000000', null, null, 4450);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'216G', null, 1, N'2024-06-22 18:59:21.0000000', null, N'2024-07-04 05:43:14.0659114', 216);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'4,8KG', null, 1, N'2024-06-23 13:57:33.0000000', null, null, 4800);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'200ml', null, 1, N'2024-06-24 11:25:34.0000000', null, null, 206);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'Lốc 4 hộp (110ml)', N'4 hộp x 110ml', 1, N'2024-06-27 10:40:27.0000000', null, null, 440);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'180ml', null, 1, N'2024-06-29 10:18:10.0000000', null, null, 185);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'320G', null, 1, N'0001-01-01 00:00:00.0000000', null, null, 320);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'380G', null, 1, N'2024-07-04 12:48:29.0000000', null, null, 380);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'test', N'asd', 1, N'2024-07-22 12:57:10.4680510', N'2024-07-22 19:57:13.2923790', N'2024-07-22 12:57:13.3564290', 22);
INSERT INTO yumilk.dbo.units (name, description, is_active, created_at, deleted_at, modified_at, gram) VALUES (N'1,7KG', N'1700 Gram', 1, N'2024-07-23 04:38:52.4732649', null, null, 1700);
