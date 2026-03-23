USE [yumilk];
GO

-- 1. Xóa bảng cũ nếu đã tồn tại để tránh lỗi Msg 2714
IF OBJECT_ID('dbo.reports', 'U') IS NOT NULL DROP TABLE dbo.reports;
GO

-- 2. Tạo bảng reports với cấu trúc đầy đủ
CREATE TABLE reports
(
    Id              uniqueidentifier NOT NULL
        constraint PK_reports primary key,
    report_type_id  int              NOT NULL
        constraint FK_reports_report_types_report_type_id
            references report_types on delete cascade,
    customer_id     uniqueidentifier NOT NULL
        constraint FK_reports_customers_customer_id
            references customers on delete cascade,
    -- Chuyển title và description thành nullable vì lệnh INSERT không có dữ liệu này
    title           nvarchar(255),
    description     nvarchar(2000),
    resolved_at     datetime2,
    resolved_by     uniqueidentifier NOT NULL,
    created_at      datetime2        NOT NULL,
    modified_at     datetime2,
    deleted_at      datetime2,
    -- Bổ sung cột product_id để khớp với lệnh INSERT
    product_id      uniqueidentifier
        constraint FK_reports_products_product_id
            references products on delete set null
)
GO

-- 3. Tạo các Index
CREATE INDEX IX_reports_customer_id ON reports (customer_id)
GO
CREATE INDEX IX_reports_report_type_id ON reports (report_type_id)
GO
CREATE INDEX IX_reports_product_id ON reports (product_id)
GO

-- 4. Chèn dữ liệu (Đảm bảo đã chạy các file customers, products, report_types trước)
INSERT INTO yumilk.dbo.reports (Id, report_type_id, customer_id, resolved_at, resolved_by, created_at, modified_at, deleted_at, product_id) 
VALUES (N'7CD1FA7E-316C-4240-B73C-057C76AEE8BF', 1, N'1FA04C00-F369-4CC5-B456-C4F1EF1B2CDD', null, N'00000000-0000-0000-0000-000000000000', N'2024-07-15 02:44:35', null, null, N'374EA12C-B2DD-4CD2-A663-3EF0BDE03FAB');

INSERT INTO yumilk.dbo.reports (Id, report_type_id, customer_id, resolved_at, resolved_by, created_at, modified_at, deleted_at, product_id) 
VALUES (N'9A40B30D-BB7C-42A6-9AA5-1D8C1FF053B0', 2, N'967D21A8-44C5-48BE-9013-74AA6BAA5D5B', null, N'00000000-0000-0000-0000-000000000000', N'2024-07-23 07:10:10', null, null, N'05A54838-37A9-4E1F-B32B-9B00A8A6DC51');

INSERT INTO yumilk.dbo.reports (Id, report_type_id, customer_id, resolved_at, resolved_by, created_at, modified_at, deleted_at, product_id) 
VALUES (N'1B405839-5661-4EE6-9CEC-1E2F44F55E43', 3, N'7108DCCA-90D2-4BDE-B2FB-0DA3B832F725', null, N'00000000-0000-0000-0000-000000000000', N'2024-07-14 23:58:07', null, null, N'5C255DB1-6813-4C2D-BC74-1EAA7A500F45');

INSERT INTO yumilk.dbo.reports (Id, report_type_id, customer_id, resolved_at, resolved_by, created_at, modified_at, deleted_at, product_id) 
VALUES (N'266F2F9F-7276-4B0E-8243-266F56BEE5A0', 5, N'98B7A21E-3F8C-4D8F-B05F-D2731AF7E433', null, N'00000000-0000-0000-0000-000000000000', N'2024-07-22 10:07:12', null, null, N'9D5E885B-D489-4950-97C1-4FB1571D06D4');

INSERT INTO yumilk.dbo.reports (Id, report_type_id, customer_id, resolved_at, resolved_by, created_at, modified_at, deleted_at, product_id) 
VALUES (N'EC6FA38E-687C-43D2-8F09-75D687536561', 1, N'4B7C7B24-E62D-4EC6-BFAF-F183CDDF10ED', null, N'00000000-0000-0000-0000-000000000000', N'2024-07-24 04:40:36', null, null, N'A843237A-D2D0-43C1-BD96-1C231E2DFA53');

INSERT INTO yumilk.dbo.reports (Id, report_type_id, customer_id, resolved_at, resolved_by, created_at, modified_at, deleted_at, product_id) 
VALUES (N'95F13B10-80C7-4560-A7C0-B549A00797F1', 3, N'B4C4244A-6EAA-4AC6-A91B-9A1537EB2ECB', null, N'00000000-0000-0000-0000-000000000000', N'2024-07-23 07:48:13', null, null, N'F2900267-0521-44EF-90FF-7C54090AA88F');

INSERT INTO yumilk.dbo.reports (Id, report_type_id, customer_id, resolved_at, resolved_by, created_at, modified_at, deleted_at, product_id) 
VALUES (N'B3D87ED1-E3EC-42AB-99F2-B6A91188FAF2', 4, N'1FA04C00-F369-4CC5-B456-C4F1EF1B2CDD', null, N'00000000-0000-0000-0000-000000000000', N'2024-07-15 02:28:56', null, null, N'374EA12C-B2DD-4CD2-A663-3EF0BDE03FAB');

INSERT INTO yumilk.dbo.reports (Id, report_type_id, customer_id, resolved_at, resolved_by, created_at, modified_at, deleted_at, product_id) 
VALUES (N'D5680ACA-E9B1-44EE-8B99-D3B13A1D3FDA', 3, N'B4C4244A-6EAA-4AC6-A91B-9A1537EB2ECB', null, N'00000000-0000-0000-0000-000000000000', N'2024-07-23 07:48:08', null, null, N'A843237A-D2D0-43C1-BD96-1C231E2DFA53');

INSERT INTO yumilk.dbo.reports (Id, report_type_id, customer_id, resolved_at, resolved_by, created_at, modified_at, deleted_at, product_id) 
VALUES (N'16E7B172-0C2F-4872-BB52-DA62DEA5B835', 3, N'7108DCCA-90D2-4BDE-B2FB-0DA3B832F725', null, N'00000000-0000-0000-0000-000000000000', N'2024-07-14 15:29:12', null, null, N'374EA12C-B2DD-4CD2-A663-3EF0BDE03FAB');

INSERT INTO yumilk.dbo.reports (Id, report_type_id, customer_id, resolved_at, resolved_by, created_at, modified_at, deleted_at, product_id) 
VALUES (N'D1245746-AA3C-4AF8-8203-E70C02EBACC2', 1, N'1FA04C00-F369-4CC5-B456-C4F1EF1B2CDD', null, N'00000000-0000-0000-0000-000000000000', N'2024-07-14 16:17:49', null, null, N'0BB0FCC5-C044-4D87-915C-CFC349F7C007');

GO