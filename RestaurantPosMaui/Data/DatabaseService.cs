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

    public async ValueTask DisposeAsync()
    {
        if (_connection != null)
            await _connection.CloseAsync();
    }
}