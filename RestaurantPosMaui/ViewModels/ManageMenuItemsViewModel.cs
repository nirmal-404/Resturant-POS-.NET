using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantPosMaui.Data;
using RestaurantPosMaui.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuItem = RestaurantPosMaui.Data.MenuItem;

namespace RestaurantPosMaui.ViewModels;
public partial class ManageMenuItemsViewModel : ObservableObject
{
    private readonly DatabaseService _databaseService;

    public ManageMenuItemsViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    [ObservableProperty]
    private MenuCategoryModel[] _categories = [];

    [ObservableProperty]
    private MenuItem[] menuItems = [];

    [ObservableProperty]
    private MenuCategoryModel? _selectedCategory = null;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private MenuItemModel _menuItem = new();

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

        SetEmptyCategoriesToItem();

        IsLoading = false;
    }


    private void SetEmptyCategoriesToItem()
    {
        MenuItem.Categories.Clear();
        foreach (var category in Categories)
        {
            var categoryOfItem = new MenuCategoryModel
            {
                Id = category.Id,
                Icon = category.Icon,
                Name = category.Name,
                IsSelected = false
            };
            MenuItem.Categories.Add(categoryOfItem);
        }
    }

    [RelayCommand]
    private async Task SelectCategoryAsync(int categoryId)
    {
        if (SelectedCategory.Id == categoryId)
            return; // The current category is already selected

        IsLoading = true;

        SelectedCategory.IsSelected = false;

        var newlySelectedCategory = Categories.First(c => c.Id == categoryId);
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
    private async Task EditMenuItemAsync(MenuItem menuItem)
    {
        //await Shell.Current.DisplayAlert("Edit", "Edit menue item", "Ok");
        var menuItemModel = new MenuItemModel
        {
            Description = menuItem.Description,
            Icon = menuItem.Icon,
            Id = menuItem.Id,
            Name = menuItem.Name,
            Price = menuItem.Price,
        };

        var itemCategoris = await _databaseService.GetCategoriesOfMenuItem(menuItem.Id);

        foreach (var category in Categories)
        {
            var categoryOfItem = new MenuCategoryModel
            {
                Icon = category.Icon,
                Id = category.Id,
                Name = category.Name
            };

            if (itemCategoris.Any(c => c.Id == category.Id))
                categoryOfItem.IsSelected = true;
            else
                categoryOfItem.IsSelected = false;

            menuItemModel.Categories.Add(category);
        }
        MenuItem = menuItemModel;
    }

    [RelayCommand]
    private void Cancel()
    {
        MenuItem = new();
        SetEmptyCategoriesToItem();
    }
}
