create table product_reviews
(
    Id          int identity
        constraint PK_product_reviews
            primary key,
    customer_id uniqueidentifier              not null
        constraint FK_product_reviews_customers_customer_id
            references customers
            on delete cascade,
    product_id  uniqueidentifier              not null
        constraint FK_product_reviews_products_product_id
            references products
            on delete cascade,
    order_id    uniqueidentifier              not null
        constraint FK_product_reviews_orders_order_id
            references orders
            on delete cascade,
    Review      nvarchar(255)                 not null,
    Rating      int                           not null,
    is_active   bit default CONVERT([bit], 1) not null,
    created_at  datetime2                     not null,
    modified_at datetime2,
    deleted_at  datetime2
)
go

create index IX_product_reviews_customer_id
    on product_reviews (customer_id)
go

create index IX_product_reviews_order_id
    on product_reviews (order_id)
go

create index IX_product_reviews_product_id
    on product_reviews (product_id)
go

INSERT INTO yumilk.dbo.product_reviews (customer_id, product_id, order_id, review, rating, is_active, created_at, modified_at, deleted_at) VALUES (N'7108DCCA-90D2-4BDE-B2FB-0DA3B832F725', N'573B4F1E-D75C-4D01-96A5-385885BE53AD', N'817869A0-ED0A-41D9-9D4C-8447F6F7D07F', N'Sữa ngon mà giá tiền thì không hợp lý', 3, 1, N'2024-07-04 23:36:18.8548810', null, null);
INSERT INTO yumilk.dbo.product_reviews (customer_id, product_id, order_id, review, rating, is_active, created_at, modified_at, deleted_at) VALUES (N'98B7A21E-3F8C-4D8F-B05F-D2731AF7E433', N'24A6E2E2-C996-4302-A8F6-3F9054CCE91C', N'67254DF6-D938-464A-BB26-038014251163', N'Sữa Ok nhé', 5, 1, N'2024-07-06 06:53:49.6669835', null, null);
INSERT INTO yumilk.dbo.product_reviews (customer_id, product_id, order_id, review, rating, is_active, created_at, modified_at, deleted_at) VALUES (N'98B7A21E-3F8C-4D8F-B05F-D2731AF7E433', N'3007E8A8-9069-4367-A23B-7EC9A3AF217C', N'ECC15C41-2513-4454-847E-9A3CD5C0CB40', N'Tuyệt vời!!!', 4, 1, N'2024-07-11 03:40:07.1493120', null, null);
INSERT INTO yumilk.dbo.product_reviews (customer_id, product_id, order_id, review, rating, is_active, created_at, modified_at, deleted_at) VALUES (N'1FA04C00-F369-4CC5-B456-C4F1EF1B2CDD', N'374EA12C-B2DD-4CD2-A663-3EF0BDE03FAB', N'FE68B3DE-C42F-4392-B18D-485286C37296', N'sữa siu ngon ', 4, 1, N'2024-07-14 04:10:53.7783219', null, null);
INSERT INTO yumilk.dbo.product_reviews (customer_id, product_id, order_id, review, rating, is_active, created_at, modified_at, deleted_at) VALUES (N'1FA04C00-F369-4CC5-B456-C4F1EF1B2CDD', N'9D5E885B-D489-4950-97C1-4FB1571D06D4', N'FE68B3DE-C42F-4392-B18D-485286C37296', N'Sữa ngon số 1 nên cho 1 sao thôi', 1, 1, N'2024-07-14 04:11:16.5701520', null, null);
INSERT INTO yumilk.dbo.product_reviews (customer_id, product_id, order_id, review, rating, is_active, created_at, modified_at, deleted_at) VALUES (N'BC610E99-11D6-4BF6-9C98-353379F66D98', N'BDFE6F37-C81B-449F-BB7A-686228DFC045', N'A4C30E70-BD7D-449E-B07D-B2B09F05E9DE', N'Sản phẩm tốt', 5, 1, N'2024-07-14 08:31:07.1475977', null, null);
INSERT INTO yumilk.dbo.product_reviews (customer_id, product_id, order_id, review, rating, is_active, created_at, modified_at, deleted_at) VALUES (N'748F37AE-4E34-48C7-922B-E8977E9FA4F5', N'9D5E885B-D489-4950-97C1-4FB1571D06D4', N'88EA6F40-8B80-4BA6-8967-BE60BEF6B7DE', N'Sản phẩm rất phù hợp', 5, 1, N'2024-07-14 08:34:38.8578122', null, null);
INSERT INTO yumilk.dbo.product_reviews (customer_id, product_id, order_id, review, rating, is_active, created_at, modified_at, deleted_at) VALUES (N'1FA04C00-F369-4CC5-B456-C4F1EF1B2CDD', N'374EA12C-B2DD-4CD2-A663-3EF0BDE03FAB', N'952AAF57-E373-4A12-9D34-E480ADE7B3A9', N'Rất hợp cho trẻ và bé', 5, 1, N'2024-07-15 02:28:36.7798916', null, null);
INSERT INTO yumilk.dbo.product_reviews (customer_id, product_id, order_id, review, rating, is_active, created_at, modified_at, deleted_at) VALUES (N'7108DCCA-90D2-4BDE-B2FB-0DA3B832F725', N'9D5E885B-D489-4950-97C1-4FB1571D06D4', N'8BDE6604-4C27-4263-A28A-C782742902F2', N'Sữa tuyệt vời', 5, 1, N'2024-07-16 09:15:54.2104491', null, null);
INSERT INTO yumilk.dbo.product_reviews (customer_id, product_id, order_id, review, rating, is_active, created_at, modified_at, deleted_at) VALUES (N'BC610E99-11D6-4BF6-9C98-353379F66D98', N'EC1FBDA8-8DE0-4BD2-A73E-5E5C06956165', N'85B9E3B1-8C00-40FE-89B8-28866EDBF256', N'Sản phẩm tốt', 5, 1, N'2024-07-18 09:26:39.1677085', null, null);
INSERT INTO yumilk.dbo.product_reviews (customer_id, product_id, order_id, review, rating, is_active, created_at, modified_at, deleted_at) VALUES (N'BC610E99-11D6-4BF6-9C98-353379F66D98', N'BDFE6F37-C81B-449F-BB7A-686228DFC045', N'85B9E3B1-8C00-40FE-89B8-28866EDBF256', N'Sản phẩm hợp ví tiền, phù hợp với trẻ nhỏ', 5, 1, N'2024-07-18 09:27:21.3193309', null, null);
INSERT INTO yumilk.dbo.product_reviews (customer_id, product_id, order_id, review, rating, is_active, created_at, modified_at, deleted_at) VALUES (N'7108DCCA-90D2-4BDE-B2FB-0DA3B832F725', N'374EA12C-B2DD-4CD2-A663-3EF0BDE03FAB', N'24FD8041-354B-4884-84CC-77E6D55270DD', N'Sữa ngon', 5, 1, N'2024-07-19 07:40:25.3942495', null, null);
INSERT INTO yumilk.dbo.product_reviews (customer_id, product_id, order_id, review, rating, is_active, created_at, modified_at, deleted_at) VALUES (N'1FA04C00-F369-4CC5-B456-C4F1EF1B2CDD', N'573B4F1E-D75C-4D01-96A5-385885BE53AD', N'EAFCCEE1-7278-4C16-8A31-D01C2F5F8C65', N'Sữa rất ngon, bé nhà mình rất thích!', 5, 1, N'2024-07-20 01:22:52.5033018', null, null);
INSERT INTO yumilk.dbo.product_reviews (customer_id, product_id, order_id, review, rating, is_active, created_at, modified_at, deleted_at) VALUES (N'1FA04C00-F369-4CC5-B456-C4F1EF1B2CDD', N'374EA12C-B2DD-4CD2-A663-3EF0BDE03FAB', N'9DAE3CB9-9AB7-4A40-82C8-48B64D704129', N'Sữa ngon hôm sau đặt thêm cái mới', 5, 1, N'2024-07-20 02:33:12.5089250', null, null);
INSERT INTO yumilk.dbo.product_reviews (customer_id, product_id, order_id, review, rating, is_active, created_at, modified_at, deleted_at) VALUES (N'98B7A21E-3F8C-4D8F-B05F-D2731AF7E433', N'374EA12C-B2DD-4CD2-A663-3EF0BDE03FAB', N'58D4BD1B-E87E-4B6F-874C-5E600DF6A9A2', N'sữa ngon số 2 Việt Nam', 2, 1, N'2024-07-20 15:28:08.9902495', null, null);
INSERT INTO yumilk.dbo.product_reviews (customer_id, product_id, order_id, review, rating, is_active, created_at, modified_at, deleted_at) VALUES (N'98B7A21E-3F8C-4D8F-B05F-D2731AF7E433', N'31349141-F76B-4D6A-BAF0-C7C3CBC79239', N'58D4BD1B-E87E-4B6F-874C-5E600DF6A9A2', N'Quá hợp lí', 5, 1, N'2024-07-20 15:28:36.6876145', null, null);
INSERT INTO yumilk.dbo.product_reviews (customer_id, product_id, order_id, review, rating, is_active, created_at, modified_at, deleted_at) VALUES (N'7108DCCA-90D2-4BDE-B2FB-0DA3B832F725', N'5C255DB1-6813-4C2D-BC74-1EAA7A500F45', N'24B3C04F-BBD1-46EC-BDA9-90193DE86762', N'Sữa ngon mà mắc quá', 4, 1, N'2024-07-23 01:39:40.2716449', null, null);
INSERT INTO yumilk.dbo.product_reviews (customer_id, product_id, order_id, review, rating, is_active, created_at, modified_at, deleted_at) VALUES (N'BC610E99-11D6-4BF6-9C98-353379F66D98', N'374EA12C-B2DD-4CD2-A663-3EF0BDE03FAB', N'632B608F-DF53-4E9E-B407-EB2BCD375C3F', N'Sản phẩm tốt, đúng với mô tả', 5, 1, N'2024-07-23 13:55:04.8846550', null, null);
INSERT INTO yumilk.dbo.product_reviews (customer_id, product_id, order_id, review, rating, is_active, created_at, modified_at, deleted_at) VALUES (N'4B7C7B24-E62D-4EC6-BFAF-F183CDDF10ED', N'374EA12C-B2DD-4CD2-A663-3EF0BDE03FAB', N'37346D43-F16E-40DA-A67A-F5184E331B27', N'ok', 5, 1, N'2024-07-24 04:41:12.8969374', null, null);
