namespace ProPhotoStock.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();

    }

	private async void RegisterButton_Clicked(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync("///register");
    }
	private async void LoginCommand(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("///main/catalog");
    }
}