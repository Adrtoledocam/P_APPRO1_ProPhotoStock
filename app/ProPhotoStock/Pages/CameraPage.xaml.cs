using Microsoft.Maui.Media;
namespace ProPhotoStock.Pages;


public partial class CameraPage : ContentPage
{
	FileResult? _photo;
	public CameraPage()
	{
		InitializeComponent();
	}

	private async void OnTakePhotoClicked(object sender, EventArgs e)
	{
		try
		{
			_photo = await MediaPicker.Default.CapturePhotoAsync();
			if (_photo != null)
			{
				var stream = await _photo.OpenReadAsync();
                PreviewImage.Source = ImageSource.FromStream(() => stream);
                PreviewImage.IsVisible = true; 
				ContinueButton.IsVisible = true;
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
				PreviewImage.Source = ImageSource.FromStream(() => stream);
				PreviewImage.IsVisible = true; 
				ContinueButton.IsVisible = true;
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"Unable to pick photo: {ex.Message}", "OK");
		}
    }

	private async void OnContinueClicked(object sender, EventArgs e)
	{
		if (_photo == null)
			return;
        await Shell.Current.GoToAsync($"//uploadphoto?path={Uri.EscapeDataString(_photo.FullPath)}");
    }
}