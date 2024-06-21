
using TeaStoreApp.Models;
using TeaStoreApp.Services;

namespace TeaStoreApp.Pages;

public partial class FavoritesPage : ContentPage
{
	private BookMarkItemService bookMarkItemService;
	public FavoritesPage()
	{
		InitializeComponent();
		bookMarkItemService = new BookMarkItemService();
	}

	private void CvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var currentSelection = e.CurrentSelection.FirstOrDefault() as BookMarkProduct;
		if (currentSelection == null) return;
		Navigation.PushAsync(new ProductDetailPage(currentSelection.productId));
		((CollectionView)sender).SelectedItem = null;

	}
	protected override void OnAppearing()
	{
		base.OnAppearing();
		GetFavoriteProducts();
	}

	private void GetFavoriteProducts()
	{
		var favouriteProducts = bookMarkItemService.ReadAll();
		CvProducts.ItemsSource = favouriteProducts;
	}
}