using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantPosMaui.Data;
using RestaurantPosMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantPosMaui.ViewModels;
public partial class HomeViewModel : ObservableObject
{
    private readonly DatabaseService _databaseService;

    [ObservableProperty]
    private MenuCategoryModel[] _categories = [];

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
        IsLoading = false;
    }

    [RelayCommand]
    private void SelectCategory(int categoryId)
    {
        if (SelectedCategory.Id == categoryId)
            return; // The current category is already selected

        SelectedCategory.IsSelected = false;

        var newlySelectedCategory = Categories.First(c=> c.Id == categoryId);
        newlySelectedCategory.IsSelected = true;

        SelectedCategory = newlySelectedCategory;
    }
}
