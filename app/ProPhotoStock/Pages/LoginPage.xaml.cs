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

}