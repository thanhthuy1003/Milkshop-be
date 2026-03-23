create table cart_details
(
    cart_id     int                                             not null
        constraint FK_cart_details_carts_cart_id
            references carts
            on delete cascade,
    product_id  uniqueidentifier                                not null
        constraint FK_cart_details_products_product_id
            references products
            on delete cascade,
    Quantity    int       default 0                             not null,
    created_at  datetime2 default '0001-01-01T00:00:00.0000000' not null,
    deleted_at  datetime2,
    modified_at datetime2,
    constraint PK_cart_details
        primary key (cart_id, product_id)
)
go

create index IX_cart_details_product_id
    on cart_details (product_id)
go

INSERT INTO yumilk.dbo.cart_details (cart_id, product_id, quantity, created_at, deleted_at, modified_at) VALUES (1, N'573B4F1E-D75C-4D01-96A5-385885BE53AD', 1, N'2024-07-23 13:44:42.7702800', null, null);
INSERT INTO yumilk.dbo.cart_details (cart_id, product_id, quantity, created_at, deleted_at, modified_at) VALUES (2, N'573B4F1E-D75C-4D01-96A5-385885BE53AD', 1, N'2024-07-24 03:10:36.2780470', null, null);
INSERT INTO yumilk.dbo.cart_details (cart_id, product_id, quantity, created_at, deleted_at, modified_at) VALUES (4, N'374EA12C-B2DD-4CD2-A663-3EF0BDE03FAB', 10, N'2024-07-13 04:05:01.6914477', null, null);
INSERT INTO yumilk.dbo.cart_details (cart_id, product_id, quantity, created_at, deleted_at, modified_at) VALUES (8, N'9D5E885B-D489-4950-97C1-4FB1571D06D4', 1, N'2024-07-02 23:31:51.6662120', null, null);
INSERT INTO yumilk.dbo.cart_details (cart_id, product_id, quantity, created_at, deleted_at, modified_at) VALUES (13, N'31349141-F76B-4D6A-BAF0-C7C3CBC79239', 1, N'2024-07-03 12:22:31.5020451', null, null);
INSERT INTO yumilk.dbo.cart_details (cart_id, product_id, quantity, created_at, deleted_at, modified_at) VALUES (14, N'9D5E885B-D489-4950-97C1-4FB1571D06D4', 1, N'2024-07-03 14:18:33.9187542', null, null);
INSERT INTO yumilk.dbo.cart_details (cart_id, product_id, quantity, created_at, deleted_at, modified_at) VALUES (16, N'9D5E885B-D489-4950-97C1-4FB1571D06D4', 1, N'2024-07-21 17:01:03.5653519', null, null);
INSERT INTO yumilk.dbo.cart_details (cart_id, product_id, quantity, created_at, deleted_at, modified_at) VALUES (18, N'573B4F1E-D75C-4D01-96A5-385885BE53AD', 1, N'2024-07-04 05:19:41.7850828', null, null);
INSERT INTO yumilk.dbo.cart_details (cart_id, product_id, quantity, created_at, deleted_at, modified_at) VALUES (18, N'374EA12C-B2DD-4CD2-A663-3EF0BDE03FAB', 26, N'2024-07-04 05:15:13.7232124', null, null);
INSERT INTO yumilk.dbo.cart_details (cart_id, product_id, quantity, created_at, deleted_at, modified_at) VALUES (21, N'A83DDEEB-E49C-4A5A-A7A1-29E5B1F87FFC', 1, N'2024-07-11 01:44:59.2257285', null, null);
INSERT INTO yumilk.dbo.cart_details (cart_id, product_id, quantity, created_at, deleted_at, modified_at) VALUES (21, N'EC1FBDA8-8DE0-4BD2-A73E-5E5C06956165', 1, N'2024-07-13 10:33:41.6422783', null, null);
INSERT INTO yumilk.dbo.cart_details (cart_id, product_id, quantity, created_at, deleted_at, modified_at) VALUES (23, N'573B4F1E-D75C-4D01-96A5-385885BE53AD', 8, N'2024-07-08 13:21:24.8689440', null, N'2024-07-08 13:21:37.8865619');
INSERT INTO yumilk.dbo.cart_details (cart_id, product_id, quantity, created_at, deleted_at, modified_at) VALUES (28, N'9D5E885B-D489-4950-97C1-4FB1571D06D4', 1, N'2024-07-21 08:38:15.3281037', null, null);
INSERT INTO yumilk.dbo.cart_details (cart_id, product_id, quantity, created_at, deleted_at, modified_at) VALUES (29, N'EC1FBDA8-8DE0-4BD2-A73E-5E5C06956165', 2, N'2024-07-21 16:11:34.4282789', null, N'2024-07-21 16:11:43.3538099');
