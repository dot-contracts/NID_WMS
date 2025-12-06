using Microsoft.Maui.Controls;
using wms_android.ViewModels;
using wms_android.Controls;

namespace wms_android.Views
{
    public partial class ClerkDashboardView : ContentPage
    {
        private readonly ClerkDashboardViewModel _viewModel;

        public ClerkDashboardView(ClerkDashboardViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            // Load dashboard data when the page appears
            if (_viewModel != null)
            {
                await _viewModel.LoadDashboardData();
            }
        }
    }
}