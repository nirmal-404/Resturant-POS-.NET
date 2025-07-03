using SQLite;

namespace RestaurantPosMaui.Data;

public class OrderItem
{
    [PrimaryKey, AutoIncrement]
    public long Id { get; set; }

    public long OrderId { get; set; }

    public int ItemId { get; set; }

    public string Name { get; set; }

    public string Icon { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    [Ignore]
    public decimal Amount => Quantity * Price;
}