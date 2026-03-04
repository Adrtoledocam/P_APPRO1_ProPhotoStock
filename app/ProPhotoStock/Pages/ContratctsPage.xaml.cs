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
        ContractsList.ItemsSource = null;
        Title = "Contrats";
        await LoadContracts();
    }
    private async Task LoadContracts()
    {
        try
        {
            var contracts = await _apiService.GetContractsByRoleAsync();

            if (contracts != null)
            {
                ContractsList.ItemsSource = contracts;
                string role = Preferences.Get("user_role", "").ToLower();

                if (role == "admin")
                {
                    Title = "Tableau de Bord Admin";
                }
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
        public string photographerName { get; set; }
        public string photographerCommission { get; set; }

        public int fkUsage { get; set; }
        public int fkType { get; set; }
    public string DisplayDate => endDate.ToString("dd/MM/yyyy");
        public string TypeName => fkType switch
        {
            1 => "Exclusif",
            2 => "Diffusion",
            _ => "Non spécifié"
        };
        public string UsageName => fkUsage switch
        {
            1 => "Publicité",
            2 => "Graphisme",
            3 => "Média",
            _ => "Indéfini"
        };
}