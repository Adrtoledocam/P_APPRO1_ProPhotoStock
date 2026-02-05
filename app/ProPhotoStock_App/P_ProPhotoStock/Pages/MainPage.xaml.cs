using P_ProPhotoStock.Models;
using P_ProPhotoStock.PageModels;

namespace P_ProPhotoStock.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}