using wms_android.data.Services;
using wms_android.ViewModels;
using wms_android.Controls;

namespace wms_android.Views;

public partial class ParcelsView : ContentPage
{
    private readonly ParcelsViewModel _viewModel;

    public ParcelsView(ParcelsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;  // ViewModel is injected via constructor
    }

    // When using Dependency Injection (if you're using an IoC container like Microsoft.Extensions.DependencyInjection)


    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ParcelsViewModel viewModel)
        {
            // Only reset if this is the first time loading the view (i.e., not navigating back)
            if (!viewModel.IsReturningToView) // Add a flag to track this
            {
                viewModel.ResetParcel(); // Reset only on the first load
            }

            // Reset the flag for subsequent navigation
            viewModel.IsReturningToView = false;
        }
    }


    // Real-time validation for Sender's phone number
    private void SenderPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!IsValidPhoneNumber(e.NewTextValue))
        {
            // Show error message
            SenderPhoneError.IsVisible = true;
        }
        else
        {
            // Hide error message when valid
            SenderPhoneError.IsVisible = false;
        }
    }

    // Real-time validation for Receiver's phone number
    private void ReceiverPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!IsValidPhoneNumber(e.NewTextValue))
        {
            // Show error message
            ReceiverPhoneError.IsVisible = true;
        }
        else
        {
            // Hide error message when valid
            ReceiverPhoneError.IsVisible = false;
        }
    }

    // Phone number validation logic
    private bool IsValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        // Check if the number starts with +254 (international format) or 07 (local format)
        if (phoneNumber.StartsWith("+254") && phoneNumber.Length == 13)
        {
            // Ensure the rest of the number is digits
            return phoneNumber.Substring(4).All(char.IsDigit);
        }
        else if (phoneNumber.StartsWith("07") && phoneNumber.Length == 10)
        {
            // Ensure the rest of the number is digits
            return phoneNumber.Substring(2).All(char.IsDigit);
        }

        return false;
    }

}