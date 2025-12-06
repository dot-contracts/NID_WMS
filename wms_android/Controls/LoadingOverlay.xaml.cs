using wms_android.Services;

namespace wms_android.Controls;

public partial class LoadingOverlay : ContentView
{
	public LoadingOverlay()
	{
		InitializeComponent();
        
        // Bind to the LoadingService singleton
        BindingContext = LoadingService.Instance;
	}
} 