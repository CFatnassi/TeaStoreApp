using TeaStoreApp.Services;

namespace TeaStoreApp.Pages;

public partial class SignupPage : ContentPage
{
	public SignupPage()
	{
		InitializeComponent();
	}

	private async void BtnSignup_Clicked(object sender, EventArgs e)
	{
		var response = await ApiService.Register(EntName.Text, EntEmail.Text, EntPhone.Text, EntPassword.Text);
		if (response)
		{
			await DisplayAlert("", "Your account has been created succesfuly", "Alright");
			await Navigation.PushAsync(new LoginPage());
		}
		else
		{
			await DisplayAlert("", "Oops.. Something went wrong", "Cancel");
		}

    }

	private async void TapLogin_Tapped(object sender, TappedEventArgs e)
	{
		await Navigation.PushAsync(new LoginPage());
	}
}