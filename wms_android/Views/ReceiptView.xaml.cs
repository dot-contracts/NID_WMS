using wms_android.ViewModels;

namespace wms_android.Views;

public partial class ReceiptView : ContentPage
{
    public ReceiptView(ReceiptViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}