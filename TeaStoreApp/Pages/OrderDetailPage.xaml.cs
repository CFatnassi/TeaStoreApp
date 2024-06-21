
using TeaStoreApp.Services;

namespace TeaStoreApp.Pages;

public partial class OrderDetailPage : ContentPage
{
	public OrderDetailPage(int orderId, int orderTotal)
	{
		InitializeComponent();
		LblTotalPrice.Text = orderTotal + "$";
		GetOrderDetail(orderId);
	}

	private async void GetOrderDetail(int orderId)
	{
		var orderDetails = await ApiService.GetOrderDetails(orderId);
		CvOrderDetail.ItemsSource = orderDetails;
	}
}