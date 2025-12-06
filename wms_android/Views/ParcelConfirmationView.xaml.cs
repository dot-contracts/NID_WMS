using wms_android.ViewModels;

namespace wms_android.Views;

public partial class ParcelConfirmationView : ContentPage
{
    public ParcelConfirmationView(ParcelConfirmationViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ParcelConfirmationViewModel viewModel)
        {
            await viewModel.LoadDataAsync();
        }
    }
}