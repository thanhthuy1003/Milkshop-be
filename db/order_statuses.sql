create table order_statuses
(
    Id          int identity
        constraint PK_order_statuses
            primary key,
    Name        nvarchar(255)                                    collate Latin1_General_CI_AS not null,
    Description nvarchar(2000),
    created_at  datetime2 default '0001-01-01T00:00:00.0000000' not null,
    deleted_at  datetime2,
    modified_at datetime2
)
go

INSERT INTO yumilk.dbo.order_statuses (name, description, created_at, deleted_at, modified_at) VALUES (N'Pending', N'Order has been received but not yet processed', N'2024-01-01 10:00:00.0000000', null, N'2024-01-01 10:00:00.0000000');
INSERT INTO yumilk.dbo.order_statuses (name, description, created_at, deleted_at, modified_at) VALUES (N'Processing', N'Order is currently being processed', N'2024-01-02 11:00:00.0000000', null, N'2024-01-02 11:00:00.0000000');
INSERT INTO yumilk.dbo.order_statuses (name, description, created_at, deleted_at, modified_at) VALUES (N'Shipped', N'Order has been shipped to the customer', N'2024-01-03 12:00:00.0000000', null, N'2024-01-03 12:00:00.0000000');
INSERT INTO yumilk.dbo.order_statuses (name, description, created_at, deleted_at, modified_at) VALUES (N'Delivered', N'Order has been delivered to the customer', N'2024-01-04 13:00:00.0000000', null, N'2024-01-04 13:00:00.0000000');
INSERT INTO yumilk.dbo.order_statuses (name, description, created_at, deleted_at, modified_at) VALUES (N'Cancelled', N'Order has been cancelled', N'2024-01-05 14:00:00.0000000', N'2024-01-05 14:30:00.0000000', N'2024-01-05 14:00:00.0000000');
INSERT INTO yumilk.dbo.order_statuses (name, description, created_at, deleted_at, modified_at) VALUES (N'Preordered', N'Order has been returned by the customer', N'2024-01-06 15:00:00.0000000', null, N'2024-01-06 15:00:00.0000000');
