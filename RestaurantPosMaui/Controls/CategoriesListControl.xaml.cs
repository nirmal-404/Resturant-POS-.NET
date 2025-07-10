using CommunityToolkit.Mvvm.Input;
using RestaurantPosMaui.Models;

namespace RestaurantPosMaui.Controls;

public partial class CategoriesListControl : ContentView
{
	public CategoriesListControl()
	{
		InitializeComponent();
	}

	public static readonly BindableProperty CategoriesProperty =
		BindableProperty.Create(nameof(Categories), typeof(MenuCategoryModel[]), typeof(CategoriesListControl), Array.Empty<MenuCategoryModel>());

	public MenuCategoryModel[] Categories
	{
		get => (MenuCategoryModel[])GetValue(CategoriesProperty);
		set => SetValue(CategoriesProperty, value);
	}

	public event Action<MenuCategoryModel> OnCategorySelected;

	[RelayCommand]
	public void SelectCategory(MenuCategoryModel category) =>
        OnCategorySelected?.Invoke(category);


}