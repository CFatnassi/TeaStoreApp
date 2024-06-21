using System.Collections.ObjectModel;
using TeaStoreApp.Models;
using TeaStoreApp.Services;

namespace TeaStoreApp.Pages;

public partial class CartPage : ContentPage
{
	public ObservableCollection<ShoppingCartItem> ShoppingCartItems = new ObservableCollection<ShoppingCartItem>();

	public CartPage()
	{
		InitializeComponent();
	}
	protected override void OnAppearing()
	{
		base.OnAppearing();
		GetShoppingCartItems();
		bool address = Preferences.ContainsKey("address");
		if (address)
		{

			LblAddress.Text = Preferences.Get("address", string.Empty);
		}
        else
        {
			LblAddress.Text = "Please Provide your address";
        }
    }
	private void UpdateTotalPrice()
	{
		var totalprice = ShoppingCartItems.Sum(x => x.Price*x.Qty);
		LblTotalPrice.Text = totalprice.ToString();
	}
	private async void GetShoppingCartItems()
	{
		ShoppingCartItems.Clear();
		var shoppingcartitems = await ApiService.GetShoppingCartItems(int.Parse(Preferences.Get("userId", "0")));
		foreach (var item in shoppingcartitems)
		{
			ShoppingCartItems.Add(item);
		}
		CvCart.ItemsSource = ShoppingCartItems;
		UpdateTotalPrice();
	}
	private async void UpdateCartQuantity(int productId, string action)
	{
		var response = await ApiService.UpdateCartQuantity(productId, action);
		if (response)
		{
			return;
		}
		else
		{
			await DisplayAlert("", "Ooops..Something went wrong", "Cancel");
		}

	}

	private void BtnIncrease_Clicked(object sender, EventArgs e)
	{
		if(sender is Button button && button.BindingContext is ShoppingCartItem cartItem)
		{
			
			cartItem.Qty++;
			UpdateCartQuantity(cartItem.ProductId, "increase");
		}
		UpdateTotalPrice();
	}

	private void BtnDecrease_Clicked(object sender, EventArgs e)
	{
		if (sender is Button button && button.BindingContext is ShoppingCartItem cartItem)
		{
			if (cartItem.Qty == 1) return;
			else if (cartItem.Qty > 1)
			{
				cartItem.Qty--;
				UpdateCartQuantity(cartItem.ProductId, "decrease");
			}
		}
		UpdateTotalPrice();
	}

	private void BtnDelete_Clicked(object sender, EventArgs e)
	{
		if (sender is ImageButton button && button.BindingContext is ShoppingCartItem cartItem)
		{

			ShoppingCartItems.Remove(cartItem);
			UpdateCartQuantity(cartItem.ProductId, "delete");
		}
		UpdateTotalPrice();
	}

	private void BtnEditAddress_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new AddressPage());
	}

	private async void TapPlaceOrder_Tapped(object sender, TappedEventArgs e)
	{
		var order = new Order()
		{
			Address = LblAddress.Text,
			UserId = int.Parse(Preferences.Get("userId", "0")),
			OrderTotal = Convert.ToInt32(LblTotalPrice.Text)
		};
		var response = await ApiService.PlaceOrder(order);
		if (response)
		{
			await DisplayAlert("", "Your order has been placed", "Alright");

			ShoppingCartItems.Clear();
		}
		else
		{
			await DisplayAlert("Ooops", "Something went worng", "Cancel");
		}
	}
}