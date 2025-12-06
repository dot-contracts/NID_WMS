using wms_android.ViewModels;

namespace wms_android.Views
{
    public partial class AllParcelsView : ContentPage
    {
        private readonly AllParcelsViewModel _viewModel;

        public AllParcelsView(AllParcelsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadParcelsCommand.ExecuteAsync(null);
        }
    }
}