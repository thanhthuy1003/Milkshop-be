create table roles
(
    Id          int identity
        constraint PK_roles
            primary key,
    Name        nvarchar(255)                                   not null,
    Description nvarchar(2000),
    created_at  datetime2 default '0001-01-01T00:00:00.0000000' not null,
    deleted_at  datetime2,
    modified_at datetime2
)
go

INSERT INTO yumilk.dbo.roles (name, description, created_at, deleted_at, modified_at) VALUES (N'Admin', null, N'0001-01-01 00:00:00.0000000', null, null);
INSERT INTO yumilk.dbo.roles (name, description, created_at, deleted_at, modified_at) VALUES (N'Staff', null, N'0001-01-01 00:00:00.0000000', null, null);
INSERT INTO yumilk.dbo.roles (name, description, created_at, deleted_at, modified_at) VALUES (N'Customer', null, N'0001-01-01 00:00:00.0000000', null, N'2024-07-02 12:18:29.7490242');
