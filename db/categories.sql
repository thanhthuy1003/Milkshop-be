USE [yumilk];
GO

-- 1. Xóa bảng cũ nếu đã tồn tại (Xóa ràng buộc ở bảng products trước nếu có)
IF OBJECT_ID('dbo.products', 'U') IS NOT NULL 
    ALTER TABLE dbo.products DROP CONSTRAINT IF EXISTS FK_products_categories_category_id;

IF OBJECT_ID('dbo.categories', 'U') IS NOT NULL DROP TABLE dbo.categories;
GO

-- 2. Tạo bảng với cấu trúc chuẩn
CREATE TABLE categories
(
    Id          int identity
        constraint PK_categories primary key,
    -- COLLATE phải đứng trước NOT NULL
    Name        nvarchar(255) collate Latin1_General_CI_AI not null, 
    Description nvarchar(2000),
    is_active   bit       default 0 not null,
    created_at  datetime2 default getdate() not null,
    deleted_at  datetime2,
    modified_at datetime2,
    ParentId    int 
        constraint FK_categories_categories_ParentId
            references categories
)
GO

-- 3. Tạo Index
CREATE INDEX IX_categories_Name ON categories (Name)
GO
CREATE INDEX IX_categories_ParentId ON categories (ParentId)
GO

-- 4. Chèn dữ liệu với IDENTITY_INSERT để bảo toàn khóa ngoại cho các bảng sau
SET IDENTITY_INSERT categories ON;
GO

INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (1, N'Sữa cho bé', N'23', 1, N'2024-06-19 10:57:32', null, N'2024-07-10 01:41:48', null);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (2, N'Sữa bột', null, 1, N'2024-06-19 10:58:15', null, N'2024-07-13 12:23:23', 1);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (3, N'0-12 Tháng', null, 1, N'2024-06-19 11:00:38', null, null, 2);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (4, N'0-6 Tháng', null, 1, N'2024-06-19 11:01:19', null, null, 2);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (5, N'6-12 Tháng', null, 1, N'2024-06-19 11:02:23', null, null, 2);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (6, N'1-2 Tuổi', null, 1, N'2024-06-19 11:02:55', null, null, 2);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (7, N'1-3 Tuổi', null, 1, N'2024-06-19 11:03:43', null, null, 2);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (8, N'2+ Tuổi', null, 1, N'2024-06-19 11:04:09', null, null, 2);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (9, N'1-10 Tuổi', null, 1, N'2024-06-19 11:04:23', null, null, 2);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (10, N'Sữa nước', null, 1, N'2024-06-19 11:05:45', null, null, 1);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (11, N'1+ Tuổi', null, 1, N'2024-06-19 11:06:38', null, null, 10);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (12, N'6+ Tháng', null, 1, N'2024-06-19 11:07:00', null, null, 10);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (13, N'10+ Tuổi', null, 1, N'2024-06-19 11:07:41', null, null, 10);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (14, N'Sữa cho mẹ bầu và sau sinh', null, 1, N'2024-06-19 11:08:28', null, null, null);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (15, N'Sữa cho mẹ bầu', null, 1, N'2024-06-19 11:08:49', null, null, 14);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (16, N'3+ Tuổi', null, 1, N'2024-06-19 11:26:44', null, null, 2);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (17, N'1-10 Tuổi', null, 1, N'2024-06-19 12:24:46', null, null, 10);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (18, N'1-3 Tuổi', N'', 1, N'2024-06-19 05:26:53', null, null, 10);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (19, N'2-6 Tuổi', null, 1, N'2024-06-22 18:05:37', null, null, 2);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (20, N'1+ Tuổi', null, 1, N'2024-06-22 18:08:37', null, null, 2);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (21, N'6-24 Tháng', null, 1, N'2024-06-24 10:56:24', null, null, 2);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (22, N'6+ Tuổi', null, 1, N'2024-06-29 09:53:24', null, null, 10);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (23, N'6-36 Tháng', null, 1, N'2024-06-29 10:17:27', null, null, 2);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (24, N'Sữa tươi', null, 1, N'2024-06-25 17:23:29', null, null, 1);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (25, N'1-10 Tuổi', null, 1, N'2024-06-27 10:35:00', N'2024-07-24 09:17:34', N'2024-07-24 09:17:34', 24);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (26, N'1+ Tuổi', null, 1, N'2024-06-27 10:35:09', null, null, 24);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (27, N'Test', N'1213', 0, N'2024-07-09 01:50:58', N'2024-07-22 20:04:30', N'2024-07-22 13:04:30', null);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (28, N'213123', N'asd', 0, N'2024-07-09 01:51:59', N'2024-07-22 20:04:16', N'2024-07-22 13:04:16', null);
INSERT INTO categories (Id, Name, Description, is_active, created_at, deleted_at, modified_at, ParentId) VALUES (29, N'3-6 tuổi', N'Sữa bột dành cho trẻ em từ 3 đến 6 tuổi', 1, N'2024-07-23 04:35:46', null, null, 2);

GO
SET IDENTITY_INSERT categories OFF;
GO