
using TeaStoreApp.Models;
using TeaStoreApp.Services;

namespace TeaStoreApp.Pages;

public partial class ProductListPage : ContentPage
{
	public ProductListPage(int catid)
	{
		InitializeComponent();
		GetProducts(catid);
	}

	private async void GetProducts(int catid)
	{
		var products = await ApiService.GetProducts("category", catid.ToString());
		CvProducts.ItemsSource= products;
	}

	private void CvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var currentSelection = e.CurrentSelection.FirstOrDefault() as Product;
		if (currentSelection == null) return;
		Navigation.PushAsync(new ProductDetailPage(currentSelection.Id));
		((CollectionView)sender).SelectedItem = null;
	}
}