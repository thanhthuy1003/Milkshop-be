create table product_statuses
(
    Id          int identity
        constraint PK_product_statuses
            primary key,
    Name        nvarchar(255) default N''                           not null,
    Description nvarchar(2000),
    created_at  datetime2     default '0001-01-01T00:00:00.0000000' not null,
    deleted_at  datetime2,
    modified_at datetime2
)
go

INSERT INTO yumilk.dbo.product_statuses (name, description, created_at, deleted_at, modified_at) VALUES (N'SELLING', N'Sản phẩm đang được bán.', N'2024-05-22 14:08:40.6233333', null, N'2024-05-28 11:10:49.4138910');
INSERT INTO yumilk.dbo.product_statuses (name, description, created_at, deleted_at, modified_at) VALUES (N'PREORDER', N'Sản phẩm có thể được đặt trước.', N'2024-05-22 14:08:40.6233333', null, null);
INSERT INTO yumilk.dbo.product_statuses (name, description, created_at, deleted_at, modified_at) VALUES (N'OUT OF STOCK', N'Sản phẩm hiện tại không còn trong kho.', N'2024-05-22 14:08:40.6233333', null, null);
