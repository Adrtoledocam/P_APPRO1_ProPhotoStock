using ProPhotoStock.Services;

namespace ProPhotoStock.Pages;

public partial class LoginPage : ContentPage
{
    private readonly ApiService _apiService;
    public LoginPage()
	{
		InitializeComponent();
        _apiService = new ApiService();
    }

	private async void RegisterButton_Clicked(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync("///register");
    }
	private async void LoginCommand(object sender, EventArgs e)
	{
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
            return;
        }

        var result = await _apiService.LoginAsync(email, password);

        if (result != null && !string.IsNullOrEmpty(result.Token))
        {
            Preferences.Set("jwt_token", result.Token);

            if (result.User != null)
            {
                Preferences.Set("user_role", result.User.role);
                Preferences.Set("user_name", result.User.username);
            }
            
            await Shell.Current.GoToAsync("///main/catalog");
        }
        else
        {
            await DisplayAlert("Erreur", "Email ou mot de passe incorrect", "OK");
        }
        //await Shell.Current.GoToAsync("///main/catalog");
    }
}