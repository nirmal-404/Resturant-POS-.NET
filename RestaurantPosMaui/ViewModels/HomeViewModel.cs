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
public partial class HomeViewModel : ObservableObject
{
    private readonly DatabaseService _databaseService;

    [ObservableProperty]
    private MenuCategoryModel[] _categories = [];

    [ObservableProperty]
    private MenuItem[] menuItems = [];

    [ObservableProperty]
    private MenuCategoryModel? _selectedCategory =null;

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
}
