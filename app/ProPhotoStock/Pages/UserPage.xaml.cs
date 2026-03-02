using ProPhotoStock.Models;
using ProPhotoStock.Services;
namespace ProPhotoStock.Pages;

public partial class UserPage : ContentPage
{
    public UserPage() 
    { 
        InitializeComponent();        
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();


        LblUserName.Text = Preferences.Get("user_name", "Inconnu");
        LblEmail.Text = Preferences.Get("user_email", "Non disponible");
        LblAccountType.Text = Preferences.Get("user_role", "Client");


    }

    /*
    private void LoadUserData()
    {

        BindingContext = new UserInfo
        {
            UserName = "test", 
            Email = "test@test.com",
            AccountType = "Photographe", // 
            ContractsCount = 5
        };
    }*/
    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        Preferences.Clear();
        await Shell.Current.GoToAsync("///login");
    }
}
