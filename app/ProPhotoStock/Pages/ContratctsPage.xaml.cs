using ProPhotoStock.Services;
namespace ProPhotoStock.Pages;


public partial class ContratctsPage : ContentPage
{
    private readonly ApiService _apiService;
    public ContratctsPage()
	{
		InitializeComponent();
        _apiService = new ApiService();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadContracts();
    }
    private async Task LoadContracts()
    {
        try
        {
            string role = Preferences.Get("user_role", "client").ToLower();
            var contracts = await _apiService.GetContractsByRoleAsync();

            ContractsList.ItemsSource = contracts;


            if (contracts != null)
            {
                ContractsList.ItemsSource = contracts;
            }

            if (role == "admin")
            {
                Title = "Tableau de Bord Admin";
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", "Impossible de charger les contrats", "OK");
        }
    }
}

public class ContractItem 
{
    public int contractId { get; set; }
    public string photoTitle { get; set; }
    public string photoUrl { get; set; }
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
    public string price { get; set; }
    public string status { get; set; }
    public int fkUsage { get; set; }
    public int fkType { get; set; }
    public string DisplayDate => endDate.ToString("dd/MM/yyyy");
}