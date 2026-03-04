namespace NET1814_MilkShop.Repositories.CoreHelpers.Enum;

public enum OrderStatusId
{
    Pending = 1,
    Processing = 2,
    Shipped = 3,
    Delivered = 4,
    Cancelled = 5,
    Preordered = 6,
}

public enum RoleId
{
    Admin = 1,
    Staff = 2,
    Buyer = 3,
    Seller = 4,
}

public enum ProductStatusId
{
    Selling = 1,
    Preordered = 2,
    OutOfStock = 3
}