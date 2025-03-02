using TeaStoreApp.Services;

namespace TeaStoreApp.Pages;

public partial class ProfilePage : ContentPage
{
	private byte[] imageArray;
	public ProfilePage()
	{
		InitializeComponent();
		LblUserName.Text = Preferences.Get("userName", string.Empty);
	}

	private async void ImgUserProfileBtn_Clicked(object sender, EventArgs e)
	{
		var file = await MediaPicker.PickPhotoAsync();
		if (file != null)
		{
			using(var stream = await file.OpenReadAsync())
			{
				using (var memory = new MemoryStream())
				{
					await stream.CopyToAsync(memory);
					imageArray = memory.ToArray();
					ImgUserProfileBtn.Source = ImageSource.FromStream(()=> new MemoryStream(imageArray));
				}
			}
		}
		var response = await ApiService.UplaodUserImage(imageArray);
		if (response)
		{
			await DisplayAlert("", "Image Uploaded successfully", "Alright");
		}
		else
		{
			await DisplayAlert("Ooops", "Somthing went wrong", "Cancel");
		}
	}
	protected override async void OnAppearing()
	{
		base.OnAppearing();
		var response = await ApiService.GetUserProfileImage();
		if (response.ImageUrl != null)
		{
			ImgUserProfileBtn.Source = response.FullImageUrl;
		}
		
	}

	private void TapOrders_Tapped(object sender, TappedEventArgs e)
	{
		Navigation.PushAsync(new OrdersPage());
    }

	private void BtnLogout_Clicked(object sender, EventArgs e)
	{
		Preferences.Set("accesstoken",string.Empty);
		Application.Current.MainPage = new NavigationPage(new LoginPage());
    }
}