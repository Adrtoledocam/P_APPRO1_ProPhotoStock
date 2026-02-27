using ProPhotoStock.Models;
using ProPhotoStock.Services;
namespace ProPhotoStock.Pages;


public partial class RegisterPage : ContentPage
{
    private readonly ApiService _apiService;
    public RegisterPage()
	{
		InitializeComponent();
        _apiService = new ApiService();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(RegUserEntry.Text) ||
            string.IsNullOrWhiteSpace(RegEmailEntry.Text) ||
            string.IsNullOrWhiteSpace(RegPasswordEntry.Text) ||
            RolePicker.SelectedIndex == -1)
        {
            await DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
            return;
        }

        var newUser = new RegisterRequest
        {
            username = RegUserEntry.Text,
            email = RegEmailEntry.Text,
            password = RegPasswordEntry.Text,

            role = RolePicker.SelectedItem.ToString() == "Photographe" ? "photographer" : "client"
        };

        bool success = await _apiService.RegisterAsync(newUser);

        if (success)
        {
            await DisplayAlert("Succès", "Compte créé ! Connectez-vous.", "OK");
            await Shell.Current.GoToAsync("///login");
        }
        else
        {
            await DisplayAlert("Erreur", "L'email est peut-être déjà utilisé", "OK");
        }
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("///login");
    }
}