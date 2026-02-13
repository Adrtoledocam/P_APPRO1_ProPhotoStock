using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ProPhotoStock.Pages;

public partial class ConfirmContratPage : ContentPage
{
	private string _contractType = "";
	private string _usageType = "";

	public ConfirmContratPage()
	{
		InitializeComponent();
		PhotoPreview.Source = "https://images.unsplash.com/photo-1506744038136-46273834b3fb";	
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

    private async void OnBuyClicked(object sender, EventArgs e)
	{
        if (_contractType == null || _usageType == null) 
		{ 
			await DisplayAlert("Erreur", "Veuillez sélectionner un contrat et un usage.", "OK"); 
			return; 		
		}
        await DisplayAlert("Contrat confirmé", $"Vous avez acheté le contrat", "OK");

		await Shell.Current.GoToAsync("///main/catalog");
    }
}