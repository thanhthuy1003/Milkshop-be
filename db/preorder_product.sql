CREATE TABLE preorder_products (
    product_id uniqueidentifier NOT NULL,
    max_preorder_quantity int NOT NULL DEFAULT 1000,
    start_date datetime2 NOT NULL,
    end_date datetime2 NOT NULL,
    expected_preorder_days int NOT NULL,
    created_at datetime2 NOT NULL,
    modified_at datetime2 NULL,
    deleted_at datetime2 NULL,
    CONSTRAINT PK_preorder_products PRIMARY KEY (product_id),
    CONSTRAINT FK_preorder_products_products_product_id FOREIGN KEY (product_id) 
        REFERENCES products (Id) ON DELETE CASCADE
);
GO
