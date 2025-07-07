using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantPosMaui.Data;
using RestaurantPosMaui.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuItem = RestaurantPosMaui.Data.MenuItem;

namespace RestaurantPosMaui.ViewModels;
public partial class HomeViewModel : ObservableObject
{
    private readonly DatabaseService _databaseService;

    [ObservableProperty]
    private MenuCategoryModel[] _categories = [];

    [ObservableProperty]
    private MenuItem[] menuItems = [];

    [ObservableProperty]
    private MenuCategoryModel? _selectedCategory =null;

    public ObservableCollection<CartModel> CartItems { get; set; } = new();

    [ObservableProperty]
    private bool _isLoading;


    public HomeViewModel(DatabaseService databaseService) 
    {
        _databaseService = databaseService;
    }

    private bool _isInitialized;
    public async ValueTask InitializeAsync() 
    {
        if (_isInitialized)
            return; // Already Initialized

        _isInitialized = true;

        IsLoading = true;
        Categories = (await _databaseService.GetMenuCategoriesAsync())
            .Select(MenuCategoryModel.FromEntity)
            .ToArray();

        Categories[0].IsSelected = true;
        SelectedCategory = Categories[0];

        MenuItems = await _databaseService.GetMenuItemsByCategoryAsync(SelectedCategory.Id);

        IsLoading = false;
    }

    [RelayCommand]
    private async Task SelectCategoryAsync(int categoryId)
    {
        if (SelectedCategory.Id == categoryId)
            return; // The current category is already selected

        IsLoading = true;

        SelectedCategory.IsSelected = false;

        var newlySelectedCategory = Categories.First(c=> c.Id == categoryId);
        newlySelectedCategory.IsSelected = true;

        SelectedCategory = newlySelectedCategory;

        MenuItems = await _databaseService.GetMenuItemsByCategoryAsync(SelectedCategory.Id);
        Debug.WriteLine(MenuItems);
        

        IsLoading = false;

        /* foreach (var item in MenuItems)
        {
            Debug.WriteLine($"Id: {item.Id}, Name: {item.Name}, Price: {item.Price}, Icon: {item.Icon}");
        } */
    }

    [RelayCommand]
    private void AddToCart(MenuItem menuItem)
    {
        var cartItem = CartItems.FirstOrDefault(c => c.ItemId == menuItem.Id);

        if (cartItem == null)
        {
            // Item does not exist in the cart
            // Add item to the cart
            cartItem = new CartModel
            {
                ItemId = menuItem.Id,
                Icon = menuItem.Icon,
                Name = menuItem.Name,
                Price = menuItem.Price,
                Quantity = 1
            };
            CartItems.Add(cartItem);
        }
        else
        {
            // This item exist in the cart
            // Increase the quantity for this item in the cart
            cartItem.Quantity++;

        }
    }

    [RelayCommand]
    private void IncreaseQuantity(CartModel cartItem) => cartItem.Quantity++;

    [RelayCommand]
    private void DecreaseQuantity(CartModel cartItem)
    {
        cartItem.Quantity--;
        if(cartItem.Quantity == 0)
            CartItems.Remove(cartItem);
    }

    [RelayCommand]
    private void RemoveItemFromCart(CartModel cartItem) => CartItems.Remove(cartItem);
}
