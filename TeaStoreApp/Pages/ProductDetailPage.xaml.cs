using TeaStoreApp.Models;
using TeaStoreApp.Services;

namespace TeaStoreApp.Pages;

public partial class ProductDetailPage : ContentPage
{
    public int productId { get; set; }
	private BookMarkItemService bookMarkItemService = new BookMarkItemService();
	public string ImageUrl;
    public ProductDetailPage(int prodid)
	{
		this.productId = prodid;
		InitializeComponent();
		GetProductDetail(prodid);
	}
	private async void GetProductDetail(int prodid)
	{	
		var product = await ApiService.GetProductDetail(prodid);
		LblProductName.Text = product.Name;
		LblProductDescription.Text = product.Detail;
		ImgProduct.Source = product.FullImageUrl;
		LblProductPrice.Text = product.Price.ToString();
		LblTotalPrice.Text = product.Price.ToString();
		ImageUrl = product.FullImageUrl;	
	}

	private void BtnAdd_Clicked(object sender, EventArgs e)
	{
		var i = Convert.ToInt32(LblQty.Text);
		i++;
		LblQty.Text = i.ToString();
		var totalPrice = i*Convert.ToInt32(LblProductPrice.Text);
		LblTotalPrice.Text = totalPrice.ToString();                         
	}

	private void BtnRemove_Clicked(object sender, EventArgs e)
	{
		var i = Convert.ToInt32(LblQty.Text);
		i--;
		if (i < 1)
		{
			return;
		}
		LblQty.Text = i.ToString();
		var totalPrice = i * Convert.ToInt32(LblProductPrice.Text);
		LblTotalPrice.Text = totalPrice.ToString();
	}

	private async void BtnAddToCart_Clicked(object sender, EventArgs e)
	{
		var shoppingCart = new ShoppingCart()
		{
			Qty = Convert.ToInt32(LblQty.Text),
			Price = Convert.ToInt32(LblProductPrice.Text),
			TotalAmount = Convert.ToInt32(LblTotalPrice.Text),
			ProductId = productId,
			CustomerId = int.Parse(Preferences.Get("userId", "0"))
		};
		var response = await ApiService.AddItemsInCart(shoppingCart);
		if(response)
		{
			await DisplayAlert("", "Your item has been added to the cart", "Alright");
		}
		else
		{
			await DisplayAlert("Ooops", "Somthing went wrong", "Cancel");
		}
	}

	private void ImgBtnFavorite_Clicked(object sender, EventArgs e)
	{
		var existingBookMark = bookMarkItemService.Read(productId);
		if (existingBookMark != null)
		{
			bookMarkItemService.Delete(existingBookMark);
		}
		else
		{
			var bookMarkedProduct = new BookMarkProduct()
			{
				productId = productId,
				IsBookMarked = true,
				Detail = LblProductDescription.Text,
				Name = LblProductName.Text,
				Price = Convert.ToInt32(LblProductPrice.Text),
				ImageUrl = ImageUrl
			};
			bookMarkItemService.Create(bookMarkedProduct);
		}
		UpdateFavoriteButton();
	}
	private void UpdateFavoriteButton()
	{
		var existingBookMark = bookMarkItemService.Read(productId);
		if (existingBookMark != null)
		{
			ImgBtnFavorite.Source = "heartfill";
		}
		else
		{
			ImgBtnFavorite.Source = "heart";
		}
	}
	protected override void OnAppearing()
	{
		base.OnAppearing();
		UpdateFavoriteButton();
	}
}