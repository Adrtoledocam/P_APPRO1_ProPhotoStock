namespace ProPhotoStock
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("ConfirmContratPage", typeof(ProPhotoStock.Pages.ConfirmContratPage));
        }
    }
}
