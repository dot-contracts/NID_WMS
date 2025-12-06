using wms_android.ViewModels;

namespace wms_android.Views;

public partial class ReportView : ContentPage
{
    private readonly ReportViewModel _viewModel;
    
    public ReportView(ReportViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        System.Diagnostics.Debug.WriteLine("ReportView appearing");
    }
} 