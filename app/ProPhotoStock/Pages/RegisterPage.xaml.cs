namespace ProPhotoStock.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

	private async void LoginButton_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("///login");
    }
}