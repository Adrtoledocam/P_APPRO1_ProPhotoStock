namespace ProPhotoStock.Pages;

public partial class ContratctsPage : ContentPage
{
	public ContratctsPage()
	{
		InitializeComponent();

        ContractsList.ItemsSource = new List<ContractItem> 
		{ 
			new ContractItem 
			{ 
				ContractNumber = 101, 
				Photographer = "Utilisateur 2", 
				PhotoName = "Photo 1", 
				MaxDate = "31/12/2026",
                ContractType = "Diffusion",
                ContractUsage = "Publicité", 
				Cost = "CHF 370.-", 
				ImageUrl = "https://images.unsplash.com/photo-1506744038136-46273834b3fb"
            }, 
			new ContractItem 
			{ 
				ContractNumber = 301, 
				Photographer = "Utilisateur 2",
                PhotoName = "Photo 4", 
				MaxDate = "15/08/2026",
                ContractType = "Exclusif",
                ContractUsage = "Media", 
				Cost = "CHF 1'400.-", 
				ImageUrl = "https://images.unsplash.com/photo-1506744038136-46273834b3fb"
            } 
		};
    }
}

public class ContractItem 
{ 
	public required int ContractNumber { get; set; } 
	public required string Photographer { get; set; } 
	public required string PhotoName { get; set; } 
	public required string MaxDate { get; set; } 
	public required string ContractType { get; set; } 
	public required string ContractUsage { get; set; } 
	public required string Cost { get; set; } 
	public required string ImageUrl { get; set; } 
}