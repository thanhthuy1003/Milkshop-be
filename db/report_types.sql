create table report_types
(
    Id          int identity
        constraint PK_report_types
            primary key,
    Name        nvarchar(255) not null,
    Description nvarchar(2000),
    created_at  datetime2     not null,
    modified_at datetime2,
    deleted_at  datetime2
)
go

INSERT INTO yumilk.dbo.report_types (name, description, created_at, modified_at, deleted_at) VALUES (N'Hàng giả, hàng nhái', N'Các sản phẩm được sản xuất và bán dưới tên của một thương hiệu hoặc nhãn hiệu đã có danh tiếng mà không có sự cho phép của chủ sở hữu', N'2024-07-14 21:57:35.0000000', null, null);
INSERT INTO yumilk.dbo.report_types (name, description, created_at, modified_at, deleted_at) VALUES (N'Hình ảnh sản phẩm không rõ ràng', N'sản phẩm không được hiển thị rõ ràng hoặc bị mờ', N'2024-07-14 21:58:16.0000000', null, null);
INSERT INTO yumilk.dbo.report_types (name, description, created_at, modified_at, deleted_at) VALUES (N'Sản phẩm không rõ nguồn gốc, xuất xứ', N'sản phẩm không có thông tin chi tiết về nơi sản xuất', N'2024-07-14 22:00:41.0000000', null, null);
INSERT INTO yumilk.dbo.report_types (name, description, created_at, modified_at, deleted_at) VALUES (N'Tên sản phẩm không phù hợp với hình ảnh sản phẩm', N'Tên sản phẩm không phù hợp với hình ảnh', N'2024-07-14 22:01:30.0000000', null, null);
INSERT INTO yumilk.dbo.report_types (name, description, created_at, modified_at, deleted_at) VALUES (N'Sản phẩm sai về độ tuổi sử dụng', N'Sản phẩm được quảng cáo hoặc cung cấp cho nhóm tuổi không phù hợp', N'2024-07-14 22:02:38.0000000', null, null);
