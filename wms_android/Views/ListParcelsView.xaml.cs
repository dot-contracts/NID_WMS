using System.Collections.Generic;
using System.Collections.ObjectModel;
using wms_android.shared.Models;
using wms_android.ViewModels;
using System.Linq;

namespace wms_android.Views
{
    // Add QueryProperty attribute to receive navigation data
    [QueryProperty(nameof(ParcelsToDisplay), "ParcelsToDisplay")]
    public partial class ListParcelsView : ContentPage
    {
        private readonly ParcelsViewModel _viewModel; // This ViewModel is shared with ParcelsView

        // Property to receive the list from navigation
        public List<Parcel> ParcelsToDisplay
        {
            set
            {
                // When the data arrives, update the collection in the shared ViewModel
                LoadParcels(value);
            }
        }

        public ListParcelsView(ParcelsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
            // Ensure the collection view is bound correctly in XAML to _viewModel.ParcelsInWaybill
            // Example: <CollectionView ItemsSource="{Binding ParcelsInWaybill}">
        }

        // Method to load the received parcels into the ViewModel's collection
        private void LoadParcels(List<Parcel> parcels)
        {
            if (parcels != null && _viewModel != null)
            {
                _viewModel.ParcelsInWaybill.Clear(); // Clear existing (if any)
                foreach (var parcel in parcels)
                {
                    _viewModel.ParcelsInWaybill.Add(parcel);
                }
                
                // Now update the shared ViewModel's other properties
                if (parcels.Any())
                {
                    _viewModel.Sender = parcels.First().Sender;
                    _viewModel.Receiver = parcels.First().Receiver;
                    // Optionally set telephone numbers too if needed on this view
                    // _viewModel.SenderTelephone = parcels.First().SenderTelephone;
                    // _viewModel.ReceiverTelephone = parcels.First().ReceiverTelephone;
                    
                    // Update the total amount and trigger property changed
                    _viewModel.UpdateCartTotalAmount();
                }
                else
                {
                    // Clear details if the list is empty
                    _viewModel.Sender = string.Empty;
                    _viewModel.Receiver = string.Empty;
                    // _viewModel.SenderTelephone = string.Empty;
                    // _viewModel.ReceiverTelephone = string.Empty;
                    _viewModel.TotalAmount = 0; // Directly set or call UpdateCartTotalAmount which handles this
                }
                
                 // Trigger property changed for Sender and Receiver as they aren't updated automatically
                 // by just setting the property in the shared VM in this context.
                 // A better approach uses a dedicated VM for this view.
                 // Forcing UI update based on shared VM state:
                 // _viewModel.OnPropertyChanged(nameof(_viewModel.Sender)); // REMOVE - Inaccessible
                 // _viewModel.OnPropertyChanged(nameof(_viewModel.Receiver)); // REMOVE - Inaccessible

                // The CollectionView in XAML bound to _viewModel.ParcelsInWaybill should update automatically
            }
        }
    }
}