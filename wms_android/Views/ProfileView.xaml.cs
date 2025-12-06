using wms_android.ViewModels;

namespace wms_android.Views;

public partial class ProfileView : ContentPage
{
    public ProfileView(ProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
} 