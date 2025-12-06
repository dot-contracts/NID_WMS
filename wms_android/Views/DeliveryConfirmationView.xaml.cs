using wms_android.ViewModels;

namespace wms_android.Views;

public partial class DeliveryConfirmationView : ContentPage
{
	private readonly DeliveryConfirmationViewModel _viewModel;

	public DeliveryConfirmationView(DeliveryConfirmationViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

	protected override bool OnBackButtonPressed()
	{
		// Handle hardware back button
		if (_viewModel.IsBusy)
		{
			// Don't allow back navigation while processing
			return true;
		}
		
		_ = Task.Run(async () => await _viewModel.NavigateBackCommand.ExecuteAsync(null));
		return true; // Prevent default back behavior
	}
	
	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		System.Diagnostics.Debug.WriteLine("DeliveryConfirmationView: OnNavigatedTo called");
	}

	protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
	{
		base.OnNavigatedFrom(args);
		System.Diagnostics.Debug.WriteLine("DeliveryConfirmationView: OnNavigatedFrom called");
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		// Reset navigation state when page appears
		_viewModel.CanNavigateBack = true;
		_viewModel.IsProcessingComplete = false;
	}
} 