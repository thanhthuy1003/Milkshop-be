create table product_attributes
(
    Id          int identity
        constraint PK_product_attributes
            primary key,
    Name        nvarchar(255)                                   collate Latin1_General_CI_AS not null ,
    Description nvarchar(2000),
    is_active   bit       default CONVERT([bit], 0)             not null,
    created_at  datetime2 default '0001-01-01T00:00:00.0000000' not null,
    deleted_at  datetime2,
    modified_at datetime2
)
go

INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'Hướng dẫn sử dụng', null, 1, N'2024-06-17 14:25:10.0000000', null, N'2024-07-13 17:07:59.4890277');
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'Hướng dẫn bảo quản', null, 1, N'2024-06-17 14:25:27.0000000', null, null);
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'Nhà sản xuất', null, 1, N'2024-06-17 14:25:47.0000000', null, null);
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'Sản xuất tại', null, 1, N'2024-06-17 14:26:08.0000000', null, null);
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'Hạn sử dụng', null, 1, N'2024-06-20 17:05:56.0000000', null, null);
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'Kích thước (Bao bì)', null, 1, N'2024-06-20 17:06:48.0000000', null, null);
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'Xuất xứ', null, 1, N'2024-06-20 17:11:09.0000000', null, null);
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'Chất liệu', null, 1, N'2024-06-20 17:21:10.0000000', null, null);
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'Phân loại', null, 1, N'2024-06-20 17:21:21.0000000', null, null);
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'Số lượng', null, 1, N'2024-06-20 17:26:18.0000000', null, null);
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'Dung tích', null, 1, N'2024-06-20 17:29:48.0000000', null, null);
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'Hương vị', null, 1, N'2024-06-20 17:29:57.0000000', null, null);
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'Hãng sản xuất', null, 1, N'2024-06-20 17:40:59.0000000', null, null);
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'Kích thước sản phẩm(DxRxC cm)', null, 1, N'2024-06-22 18:56:32.0000000', null, null);
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'Đối tượng sử dụng', null, 1, N'2024-06-22 19:15:50.0000000', null, null);
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'Nhập khẩu ', null, 1, N'2024-06-24 10:48:30.0000000', null, null);
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'Sản xuất và đóng gói', null, 1, N'2024-06-24 10:48:32.0000000', null, null);
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'Quy cách đóng gói', null, 1, N'2024-06-29 10:20:22.0000000', null, null);
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'Lưu ý', null, 1, N'2024-07-05 13:20:27.0000000', null, null);
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'test', N'123', 0, N'2024-07-09 03:49:37.6245480', null, N'2024-07-09 16:00:31.2680920');
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'asd', N'12312451', 0, N'2024-07-09 03:58:28.5431780', null, N'2024-07-09 16:00:25.4899960');
INSERT INTO yumilk.dbo.product_attributes (name, description, is_active, created_at, deleted_at, modified_at) VALUES (N'Lượng nước đun sôi để nguội 40°C (ml)', N'Lượng nước đun sôi để nguội 40°C (ml)', 1, N'2024-07-23 04:46:24.6031660', null, null);
