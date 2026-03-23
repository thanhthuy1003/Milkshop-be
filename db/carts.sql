create table carts
(
    Id          int identity
        constraint PK_carts
            primary key,
    customer_id uniqueidentifier default '00000000-0000-0000-0000-000000000000' not null
        constraint FK_carts_customers_customer_id
            references customers
            on delete cascade,
    created_at  datetime2        default '0001-01-01T00:00:00.0000000'          not null,
    deleted_at  datetime2,
    modified_at datetime2
)
go

create unique index IX_carts_customer_id
    on carts (customer_id)
go

INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'7108DCCA-90D2-4BDE-B2FB-0DA3B832F725', N'2024-06-25 07:53:44.5734280', null, N'2024-07-03 15:16:06.3162154');
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'98B7A21E-3F8C-4D8F-B05F-D2731AF7E433', N'2024-06-27 02:32:11.4162665', null, N'2024-07-04 07:54:10.8227806');
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'BC610E99-11D6-4BF6-9C98-353379F66D98', N'2024-06-27 06:09:42.8951457', null, N'2024-07-04 02:46:13.1419586');
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'97D784C8-2430-49F4-9D66-122CC159F52A', N'2024-06-30 11:47:40.2830692', null, null);
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'ACC3ADED-3F65-4A85-B7A8-1E73D4B09266', N'2024-06-30 12:29:26.9276383', null, null);
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'3F675EBE-6DCF-4528-A146-CE7FE88945EB', N'2024-07-01 04:26:34.2259043', null, null);
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'5BE3447E-6293-499E-B6C7-49F3699517A5', N'2024-07-02 23:31:51.2009970', null, null);
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'EE96D8D9-75CA-4C93-80BF-42CB1CD7ADB6', N'2024-07-03 11:55:17.1066570', null, N'2024-07-03 12:08:41.0564869');
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'47D6F512-F487-46D2-954F-D583F0E783C9', N'2024-07-03 11:56:21.5451503', null, null);
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'E730D2D2-B0C7-4A6C-A9F3-2D93756B57B8', N'2024-07-03 12:05:33.8166101', null, null);
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'AD122ADC-E1C2-43B3-AE2B-A7114C524558', N'2024-07-03 12:19:03.0529333', null, N'2024-07-03 12:35:23.7734544');
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'A84D9C03-7AC2-499B-A57A-16784431EA59', N'2024-07-03 12:22:31.4904284', null, null);
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'4B344CD6-F21B-4B68-A7CC-1F57974F0265', N'2024-07-03 14:18:33.8161422', null, null);
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'90EB9D4F-D51A-4A35-96D4-721AF4D3BB11', N'2024-07-04 02:46:36.0274631', null, N'2024-07-04 02:51:25.2564745');
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'BD6A2BA0-B05D-4F07-ADA6-279A0D23BC90', N'2024-07-04 03:06:05.9774290', null, N'2024-07-04 03:08:55.7735522');
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'D5CCDFFF-93C6-41CA-A571-5F573DA7DF8F', N'2024-07-04 05:15:13.5341877', null, null);
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'B40075FD-79FA-40FB-B81E-26BCE0B072BB', N'2024-07-04 05:21:30.6608542', null, N'2024-07-05 11:20:20.9392807');
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'B4C4244A-6EAA-4AC6-A91B-9A1537EB2ECB', N'2024-07-05 13:26:45.7475371', null, null);
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'63ADD270-D7F1-4011-B1AE-639FC8D03974', N'2024-07-08 03:50:21.0319564', null, null);
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'748F37AE-4E34-48C7-922B-E8977E9FA4F5', N'2024-07-08 07:43:25.9891118', null, null);
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'1920BE18-B4C0-468B-A10D-DEED91FF6486', N'2024-07-08 13:21:24.7858435', null, null);
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'5F26FF3F-C378-4673-8F96-C0D1CCBFFD70', N'2024-07-11 03:51:00.2700410', null, null);
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'1FA04C00-F369-4CC5-B456-C4F1EF1B2CDD', N'2024-07-13 11:15:49.6458404', null, null);
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'8B3D0234-43CE-499E-A1A8-E97333907002', N'2024-07-17 14:26:29.1921753', null, null);
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'11D9BF9D-D010-4DC9-9F05-E992FBB09560', N'2024-07-20 02:13:18.2136909', null, null);
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'2A51D680-D7AA-40DD-AF5C-BE75F4BF0887', N'2024-07-21 08:38:15.2853139', null, null);
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'F6108C88-3764-4B24-BD40-1A4553B7153C', N'2024-07-21 16:11:34.4136518', null, null);
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'967D21A8-44C5-48BE-9013-74AA6BAA5D5B', N'2024-07-23 06:45:36.9779435', null, null);
INSERT INTO yumilk.dbo.carts (customer_id, created_at, deleted_at, modified_at) VALUES (N'4B7C7B24-E62D-4EC6-BFAF-F183CDDF10ED', N'2024-07-24 04:24:34.2876533', null, null);
