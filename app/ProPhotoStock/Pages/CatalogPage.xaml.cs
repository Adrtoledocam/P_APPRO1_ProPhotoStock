namespace ProPhotoStock.Pages;

public partial class CatalogPage : ContentPage
{
	public CatalogPage()
	{
		InitializeComponent();

        PhotosList.ItemsSource = new List<PhotoItem> 
		{ 
			new PhotoItem 
			{ 
				User = "Utilisateur 1", 
				Title = "Photo 1", 
				ImageUrl = "https://images.unsplash.com/photo-1506744038136-46273834b3fb", 
				Tag = "#Paysage" 
			}, 
			new PhotoItem 
			{ 
				User = "Utilisateur 2", 
				Title = "Photo 2", 
				ImageUrl = "https://images.unsplash.com/photo-1506744038136-46273834b3fb",
				Tag = "#Portrait" 
			} 
		};
    }
}

public class PhotoItem { public required string Title { get; set; } public required string User { get; set; }  public required string ImageUrl { get; set; } public required string Tag { get; set; } }