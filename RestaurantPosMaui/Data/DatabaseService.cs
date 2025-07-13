using RestaurantPosMaui.Models;
using SQLite;
using System.ComponentModel;
using System.Diagnostics;

namespace RestaurantPosMaui.Data;

public class DatabaseService
    : IAsyncDisposable
{
    private readonly SQLiteAsyncConnection _connection;
    public DatabaseService()
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "restpos.db3");
        Debug.WriteLine(dbPath);
        _connection = new SQLiteAsyncConnection(dbPath,
           SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);

    }

    public async Task InitilizeDatabaseAsync()
    {
        await _connection.CreateTableAsync<MenuCategory>();
        await _connection.CreateTableAsync<MenuItem>();
        await _connection.CreateTableAsync<MenuItemCategoryMapping>();
        await _connection.CreateTableAsync<Order>();
        await _connection.CreateTableAsync<OrderItem>();

        await SeedDataAsync();

        var menuItems = await GetMenuItemsByCategoryAsync(1);
    }

    private async Task SeedDataAsync()
    {
        var fisrtCategory = await _connection.Table<MenuItem>().FirstOrDefaultAsync();
        if (fisrtCategory != null)
            return; // database already seeded

        var categories = SeedData.GetMenuCategories();
        var menuItems = SeedData.GetMenuItems();
        var mappings = SeedData.GetMenuItemCategoryMapping();


        await _connection.InsertAllAsync(categories);
        await _connection.InsertAllAsync(menuItems);
        await _connection.InsertAllAsync(mappings);
    }

    public async Task<MenuCategory[]> GetMenuCategoriesAsync() =>
        await _connection.Table<MenuCategory>().ToArrayAsync();

    public async Task<MenuItem[]> GetMenuItemsByCategoryAsync(int categoryId)
    {
        var query = @"
                    SELECT menu.* FROM MenuItem AS menu
                    INNER JOIN MenuItemCategoryMapping AS mapping 
                        ON menu.Id = mapping.menuItemId
                    WHERE mapping.MenuCategoryId = ?  
                    ";
        var menuItems = await _connection.QueryAsync<MenuItem>(query, categoryId);
        return [..menuItems];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Returns error massege or null (if the operation is successfull)</returns>
    public async Task<string?> PlaceOrderAsync(OrderModel model)
    {
        var order = new Order
        {
            OrderDate = model.OrderDate,
            PaymentMode = model.PaymentMode,
            TotalAmountPaid = model.TotalAmountPaid,
            TotalItemsCount = model.TotalItemsCount,
        };

        if (await _connection.InsertAsync(order) > 0)
        {
            // Order Inserted successfully
            // Now we have the newly inserted order id in order.id
            // Now we can add the orderId to the OrderItems and Insert OrderItems in the database
            foreach (var item in model.Items)
            {
                item.OrderId = order.Id;
            }
            if (await _connection.InsertAllAsync(model.Items) == 0)
            {
                // Order Items insert operation failed
                // Remove the newly inserted order also
                await _connection.DeleteAsync(order);
                return "Error in inserting order item";
            }

        }
        else 
        {
            return "Error in inserting the order";
        }
        model.Id = order.Id;
        return null;
    }

    public async Task<Order[]> GetOrdersAsync() => 
        await _connection.Table<Order>().ToArrayAsync();

    public async Task<OrderItem[]> GetOrderItemsAsync(long orderId) =>
        await _connection.Table<OrderItem>().Where(oi => oi.OrderId == orderId).ToArrayAsync();

    public async Task<MenuCategory[]> GetCategoriesOfMenuItem(int menuItemId)
    {
        var query = @" 
                    SELECT cat.* 
                    FROM Menucategory cat 
                    INNER JOIN MenuItemCategoryMapping map
                        ON cat.Id = map.MenuCategoryId
                    WHERE map.MenuCategoryId = ?
            ";

        var categories = await _connection.QueryAsync<MenuCategory>(query, menuItemId);

        return [.. categories];
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection != null)
            await _connection.CloseAsync();
    }
}