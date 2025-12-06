using wms_android.ViewModels;

namespace wms_android.Views;

public partial class DeliveryView : ContentPage
{
    private readonly DeliveryViewModel _viewModel;

    public DeliveryView(DeliveryViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    // Optional: Handle page lifecycle events here if needed
    // protected override void OnAppearing()
    // {
    //     base.OnAppearing();
    //     // You could potentially call an Initialize method on the ViewModel here
    // }

    // protected override void OnDisappearing()
    // {
    //     base.OnDisappearing();
    //     // Call ViewModel cleanup or directly close scanner if needed
    //     _viewModel.Cleanup(); // Example call to VM cleanup
    // }
} 