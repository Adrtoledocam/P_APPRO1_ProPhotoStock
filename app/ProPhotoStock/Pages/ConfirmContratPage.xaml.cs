using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using ProPhotoStock.Models;
using ProPhotoStock.Services;

namespace ProPhotoStock.Pages;

//[QueryProperty(nameof(Photo), "SelectedPhoto")]
public partial class ConfirmContratPage : ContentPage
{
    private readonly ApiService _apiService;
    private string _contractType = "";
	private string _usageType = "";

    public static PhotoItem SelectedPhoto { get; set; }

    public ConfirmContratPage()
	{
		InitializeComponent();
        _apiService = new ApiService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        int id = Preferences.Get("selected_photo_id", 0);

        if (id != 0)
        {
            try
            {
                var photo = await _apiService.GetPhotoByIdAsync(id);
                if (photo != null)
                {
                    PhotoPreview.Source = photo.photoUrl;
                    AuthorLabel.Text = photo.useName;
                    TitleLabel.Text = photo.photoTitle;
                    this.BindingContext = photo;
                }
            }
            catch
            {
                await DisplayAlert("Erreur", "Impossible de charger les détails de la photo", "OK");
            }
        }        
    }
    private async void OnBuyClicked(object sender, EventArgs e)
    {
        var photo = this.BindingContext as PhotoItem;
        int photoId = photo?.photoId ?? Preferences.Get("selected_photo_id", 0);
        if (photoId == 0)
        {
            await DisplayAlert("Erreur", "Impossible d'identifier la photo pour l'achat.", "OK");
            return;
        }
        if (string.IsNullOrEmpty(_contractType) || string.IsNullOrEmpty(_usageType))
        {
            await DisplayAlert("Erreur", "Veuillez sélectionner un contrat et un usage.", "OK");
            return;
        }

        int typeId = _contractType == "exclusif" ? 1 : 2;

        int usageId = _usageType switch
        {
            "pub" => 1,
            "graph" => 2,
            "media" => 3,
            _ => 0
        };
        var contractRequest = new ContractRequest
        {
            fkPhoto = photoId,
            fkType = typeId,
            fkUsage = usageId
        };
        try
        {
            bool success = await _apiService.CreateContractAsync(contractRequest);

            if (success)
            {
                await DisplayAlert("Succès", "Contrat confirmé et achat réussi", "OK");
                await Shell.Current.GoToAsync("///main/catalog");
            }
            else
            {
                await DisplayAlert("Erreur", "Problème lors de la création del contrato.", "OK");
            }
        }
        catch
        {
            await DisplayAlert("Erreur Système", $"Une erreur est survenue", "OK");
        }
    }


    private void OnContractChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value) return;
		var rb = sender as RadioButton;
		_contractType = rb.Value.ToString();

		UpdateTotal();

    }
    private void OnUsageChanged(object sender, CheckedChangedEventArgs e)
	{
		if (!e.Value) return;
		var rb = sender as RadioButton;
        _usageType = rb.Value.ToString();

		UpdateTotal();

    }

	private void UpdateTotal()
	{
        if (_contractType == null || _usageType == null) 
		{ 
			TotalLabel.Text = "Total : CHF 0.-"; return; 
		}
        int total = 0; 
		if (_contractType == "exclusif") 
		{ 
			switch (_usageType) 
			{ 
				case "pub": total = 740; 
					break; 
				case "graph": total = 1280; 
					break; 
				case "media": total = 1400; 
					break; 
			} 
		} else if (_contractType == "diffusion") 
		{ 
			switch (_usageType) 
			{ 
				case "pub": total = 370;
					break; 
				case "graph": total = 640; 
					break; 
				case "media": total = 700; 
					break; 
			} 
		}

        TotalLabel.Text = $"Total : CHF {total}.-";
    }
}