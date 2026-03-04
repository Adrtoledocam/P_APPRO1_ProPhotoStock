using Microsoft.Maui.Media;
using ProPhotoStock.Services;
using ProPhotoStock.Models;
namespace ProPhotoStock.Pages;


public partial class UploadPhotoPage : ContentPage
{

    FileResult? _photo;

    public string PhotoPath { get; set; }
    private string _savedPath;
    private readonly ApiService _apiService = new ApiService();

    public UploadPhotoPage()
	{
		InitializeComponent();
        //Console.WriteLine("Constructor: PhotoPath = " + PhotoPath);
    }

    private void ResetUI()
    {
        UploadFields.IsVisible = false;
        SelectGrid.IsVisible = true;
        PhotoPreview.IsVisible = false;
        PhotoPreview.Source = null;
        PhotoNameEntry.Text = string.Empty;
        foreach (var layout in UploadFields.Children.OfType<HorizontalStackLayout>())
        {
            foreach (var cb in layout.Children.OfType<CheckBox>())
                cb.IsChecked = false;
        }
        _photo = null;
    }

    private async void OnTakePhotoClicked(object sender, EventArgs e)
	{
		try
		{
			_photo = await MediaPicker.Default.CapturePhotoAsync();
			if (_photo != null)
			{
				var stream = await _photo.OpenReadAsync();
                PhotoPreview.Source = ImageSource.FromStream(() => stream);
                UploadFields.IsVisible = true;
                PhotoPreview.IsVisible = true;
                SelectGrid.IsVisible = false;
            }
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"Unable to take photo: {ex.Message}", "OK");
		}
    }

    private async void OnPickPhotoClicked(object sender, EventArgs e)
    {
        try
        {
            _photo = await MediaPicker.Default.PickPhotoAsync();
            if (_photo != null)
            {
                var stream = await _photo.OpenReadAsync();
                PhotoPreview.Source = ImageSource.FromStream(() => stream);
                UploadFields.IsVisible = true;
                PhotoPreview.IsVisible = true;
                SelectGrid.IsVisible = false;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Unable to pick photo: {ex.Message}", "OK");
        }
    }

    protected override void OnAppearing()
    {
        //Console.WriteLine("OnAppearing: PhotoPath = " + PhotoPath);
        base.OnAppearing();
        ResetUI();
        if (!string.IsNullOrEmpty(PhotoPath)) 
        {
            PhotoPreview.Source = ImageSource.FromFile(PhotoPath); 
        }
    }
    private async void OnUploadClicked(object sender, EventArgs e)
    {
        if (_photo == null || string.IsNullOrWhiteSpace(PhotoNameEntry.Text))
        {
            await DisplayAlert("Erreur", "Veuillez sélectionner une photo et lui donner un nom.", "OK");
            return;
        }

        try
        {
            string base64String = "";

            using (var stream = await _photo.OpenReadAsync())
            using (var ms = new MemoryStream())
            {
                await stream.CopyToAsync(ms);
                var bytes = ms.ToArray();
                base64String = Convert.ToBase64String(bytes).Replace("\n", "").Replace("\r", "").Trim();

            }

            var selectedTags = new List<int>();
            if (TagPortrait.IsChecked) selectedTags.Add(1);
            if (TagPaysage.IsChecked) selectedTags.Add(2);
            if (TagSport.IsChecked) selectedTags.Add(3);

            var photoData = new
            {
                photoTitle = PhotoNameEntry.Text,
                photoBase64 = base64String,
                status = "available",
                tags = selectedTags.ToArray()
            };
        
            var result = await _apiService.UploadPhotoAsync(photoData);

            if (result != null)
            {
                await DisplayAlert("Succès", "Photo partagée !", "OK");
                ResetUI(); 
                await Shell.Current.GoToAsync("///main/catalog");
            }
            else
            {
                await DisplayAlert("Erreur", "Le serveur n'a pas pu traiter l'image.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", $"Erreur technique: {ex.Message}", "OK");
        }
    }

}

