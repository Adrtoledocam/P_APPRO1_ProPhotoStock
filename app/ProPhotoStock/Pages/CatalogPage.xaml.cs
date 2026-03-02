namespace ProPhotoStock.Pages;
using ProPhotoStock.Models;
using ProPhotoStock.Services;
public partial class CatalogPage : ContentPage
{
    private readonly ApiService _apiService;
    //private string _savedPath = "https://images.unsplash.com/photo-1506744038136-46273834b3fb";

    public CatalogPage()
	{
		InitializeComponent();
        _apiService = new ApiService();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadPhotos();
    }
    private async Task LoadPhotos()
    {
        try
        {
            var photos = await _apiService.GetPhotosAsync();
            PhotosList.ItemsSource = photos;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", $"Impossible de charger les photos : {ex.Message}", "OK");
            return;
        }
        
    }
    private async void OnFilterChanged(object sender, CheckedChangedEventArgs e)
    {
        if (!e.Value) { await LoadPhotos(); return; }

        string tag = "";
        if (sender == PaysageFilter) tag = "Paysage";
        else if (sender == PortraitFilter) tag = "Portrait";
        else if (sender == SportFilter) tag = "Sport";

        if (!string.IsNullOrEmpty(tag))
        {
            var filtered = await _apiService.GetPhotosByTagAsync(tag);
            PhotosList.ItemsSource = filtered;
        }
    }
    private async void OnConfirmContratClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var dataContext = button?.BindingContext;
        if(dataContext != null)
        {
            dynamic photoD = dataContext;
            Preferences.Set("selected_photo_id", photoD.photoId);           

            try
            {
                await Shell.Current.GoToAsync("ConfirmContratPage");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "Error boton", "OK");
        }
        
    }
}
public class PhotoItem
{
    public int photoId { get; set; }
    public string photoTitle { get; set; }
    public string photoUrl { get; set; }
    public string useName { get; set; }
    public string tagName { get; set; }
}