using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using RestaurantPosMaui.Data;
using RestaurantPosMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantPosMaui.ViewModels;
public partial class OrdersViewModel : ObservableObject
{
    private readonly DatabaseService _databaseService;
    public OrdersViewModel(DatabaseService databaseService)
    { 
        _databaseService = databaseService;
    }

    // Returns true if the order creation was successfull, false otherwise
    public async Task<bool> PlaceOrderAsync(CartModel[] cartItems, bool isPaidOnline)
    {
        var orderItems = cartItems
            .Select(c => new OrderItem
            {
                Icon = c.Icon,
                ItemId = c.ItemId,
                Name = c.Name,
                Price = c.Price,
                Quantity = c.Quantity
            }).ToArray();

        var orderModel = new OrderModel
        {
            OrderDate = DateTime.Now,
            PaymentMode = isPaidOnline ? "Online" : "Cash",
            TotalAmountPaid = cartItems.Sum(c => c.Amount),
            TotalItemsCount = cartItems.Length,
            Items = orderItems
        };

        var errorMessage = await _databaseService.PlaceOrderAsync(orderModel);
        if (!string.IsNullOrEmpty(errorMessage))
        {
            // Order creation failed
            await Shell.Current.DisplayAlert("Error", errorMessage, "Ok");
            return false;
        }
        // Order creation was successfull
        await Toast.Make("Order placed successfully").Show();
        return true;
    }

}

