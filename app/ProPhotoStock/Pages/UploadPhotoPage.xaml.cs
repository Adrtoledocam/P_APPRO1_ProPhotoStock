using Microsoft.Maui.Media;
namespace ProPhotoStock.Pages;


public partial class UploadPhotoPage : ContentPage
{

    FileResult? _photo;

    public string PhotoPath { get; set; }
    private string _savedPath;


    public UploadPhotoPage()
	{
		InitializeComponent();
        //Console.WriteLine("Constructor: PhotoPath = " + PhotoPath);
    }

    private void ResetUI()
    {
        UploadFields.IsVisible = false;
        SelectButtons.IsVisible = true;
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
                SelectButtons.IsVisible = false;
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
                SelectButtons.IsVisible = false;
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
        await DisplayAlert("Succès", "Photo partagée avec succès !", "OK");
        await Shell.Current.GoToAsync("///main/catalog");
    }
    
}

