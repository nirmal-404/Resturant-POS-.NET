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
using System.Text.Json;
using System.Threading.Tasks;
using MenuItem = RestaurantPosMaui.Data.MenuItem;

namespace RestaurantPosMaui.ViewModels;
public partial class HomeViewModel : ObservableObject
{
    private readonly DatabaseService _databaseService;
    private readonly OrdersViewModel _ordersViewModel;

    [ObservableProperty]
    private MenuCategoryModel[] _categories = [];

    [ObservableProperty]
    private MenuItem[] menuItems = [];

    [ObservableProperty]
    private MenuCategoryModel? _selectedCategory =null;

    public ObservableCollection<CartModel> CartItems { get; set; } = new();

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty, NotifyPropertyChangedFor(nameof(Total)), NotifyPropertyChangedFor(nameof(TaxAmout))]
    private decimal _subtotal;

    [ObservableProperty, NotifyPropertyChangedFor(nameof(Total)), NotifyPropertyChangedFor(nameof(TaxAmout))]
    private int _taxPercentage;

    public decimal TaxAmout => (Subtotal * TaxPercentage) / 100;

    public decimal Total => Subtotal + TaxAmout;

    public HomeViewModel(DatabaseService databaseService, OrdersViewModel ordersViewModel) 
    {
        _databaseService = databaseService;
        _ordersViewModel = ordersViewModel;
        CartItems.CollectionChanged += CartItems_CollectionChanged;
    }

    private void CartItems_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        // This will be executed whenever we are
        //      Adding item to the cart
        //      Rmoveing item form the cart
        //      Or clearing the cart
        RecalculateAmount();
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
            RecalculateAmount();
        }
    }

    [RelayCommand]
    private void IncreaseQuantity(CartModel cartItem)
    {
        cartItem.Quantity++;
        RecalculateAmount();
    }

    [RelayCommand]
    private void DecreaseQuantity(CartModel cartItem)
    {
        cartItem.Quantity--;
        if(cartItem.Quantity == 0)
            CartItems.Remove(cartItem);
        else
            RecalculateAmount();
    }

    [RelayCommand]
    private void RemoveItemFromCart(CartModel cartItem) => CartItems.Remove(cartItem);

    [RelayCommand]
    private async Task ClearCartAsync()
    {
        if (await Shell.Current.DisplayAlert("Clear Cart?", "Do you really want to clear the cart", "Yes", "No"))
        {
            CartItems.Clear();
        }

    }

    private void RecalculateAmount()
    { 
        Subtotal = CartItems.Sum(c => c.Amount);
    }

    [RelayCommand]
    private async Task TaxPercentageClickAsync()
    {
        var result = await Shell.Current.DisplayPromptAsync("Tax Percentage", "Enter the applicable tax percentage", placeholder: "10", initialValue: TaxPercentage.ToString());

        if (!string.IsNullOrWhiteSpace(result))
        {
            if (!int.TryParse(result, out int enteredTaxPercentage)) 
            {
                await Shell.Current.DisplayAlert("Invalid value", "Entered tax percentage is invalid", "Ok");
                return;
            }

            // if it is a negative value
            if (enteredTaxPercentage < 0) {
                await Shell.Current.DisplayAlert("Invalid value", "Tax perventage cannot be a negative value", "Ok");
                return;
            }

            TaxPercentage = enteredTaxPercentage;
        }
    
    }

    [RelayCommand]
    private async Task PlaceOrderAsync(bool isPaidOnline)
    {
        IsLoading = true;
        Debug.WriteLine(JsonSerializer.Serialize(CartItems, new JsonSerializerOptions { WriteIndented = true }));
        if (await _ordersViewModel.PlaceOrderAsync([.. CartItems], isPaidOnline))
        { 
            // Prderr creation successfull
            // Clear the cart items
            CartItems.Clear();
        }
        IsLoading = false;
    }
}
