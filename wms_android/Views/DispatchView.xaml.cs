using wms_android.ViewModels;

namespace wms_android.Views;

public partial class DispatchView : ContentPage
{
    public DispatchView(DispatchViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        if (BindingContext is DispatchViewModel viewModel)
        {
            await viewModel.LoadDataAsync();
        }
    }
}