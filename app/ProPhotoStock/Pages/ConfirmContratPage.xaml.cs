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

    //private PhotoItem _photo;
	/*public PhotoItem Photo
    {
        get => _photo;
        set
        {
            _photo = value;
            OnPropertyChanged();
            UpdateUI(); 
        }
    }*/
    public ConfirmContratPage()
	{
		InitializeComponent();
        _apiService = new ApiService();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (SelectedPhoto != null)
        {
            PhotoPreview.Source = SelectedPhoto.photoUrl;
            AuthorLabel.Text = SelectedPhoto.useName;
            TitleLabel.Text = SelectedPhoto.photoTitle;
        }
    }/*
    private void UpdateUI()
    {
        dynamic p = Photo;
        if (p != null)
        {
            try
            {
                PhotoPreview.Source = p.photoUrl;
            }
            catch
            {
                // Evitamos que un error de carga de imagen rompa la página
            }
        }
    }
    */
    private async void OnBuyClicked(object sender, EventArgs e)
    {
        if (SelectedPhoto == null) return;

        if (string.IsNullOrEmpty(_contractType) || string.IsNullOrEmpty(_usageType))
        {
            await DisplayAlert("Erreur", "Veuillez sélectionner un contrat et un usage.", "OK");
            return;
        }

        var contractRequest = new ContractRequest
        {
            fkPhoto = SelectedPhoto.photoId,
            contractType = _contractType,
            usageType = _usageType
        };

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