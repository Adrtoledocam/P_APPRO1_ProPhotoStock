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
        //var photo = (sender as Button).CommandParameter as PhotoItem;
        await DisplayAlert("Click", "Botón presionado", "OK");
        var button = sender as Button;
        var dataContext = button?.BindingContext;
        //var photo = button?.CommandParameter as PhotoItem;
        if (dataContext != null)
        {
            //dynamic photo = dataContext;
            ConfirmContratPage.SelectedPhoto = dataContext as PhotoItem;
            //await DisplayAlert("Données detectés", $"ID Foto: {ConfirmContratPage.SelectedPhoto.photoId}", "OK");

            /*
            await DisplayAlert("Données detectés", $"ID Foto: {photo.photoId}", "OK");
            var navigationParameter = new Dictionary<string, object>
            {
                { "SelectedPhoto", dataContext }
            };*/
            try
            {
                await Shell.Current.GoToAsync("ConfirmContratPage");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error de navegación: {ex.Message}", "OK");
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