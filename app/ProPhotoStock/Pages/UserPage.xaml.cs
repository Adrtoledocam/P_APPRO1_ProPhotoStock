namespace ProPhotoStock.Pages;

public partial class UserPage : ContentPage
{
    public UserPage() 
    { 
        InitializeComponent(); 
        BindingContext = new UserInfo 
        { 
            UserName = "Utilisateur 2", 
            Email = "Utilisateur2@gmail.com", 
            AccountType = "Photographe", 
            ContractsCount = 2 
        }; 
    }
    private async void OnLogoutClicked(object sender, EventArgs e)
    { 
        await Shell.Current.GoToAsync("///login");
    }
}
public class UserInfo 
{ 
    public required string UserName { get; set; } 
    public required string Email { get; set; } 
    public required string AccountType { get; set; } 
    public required int ContractsCount { get; set; } 
}