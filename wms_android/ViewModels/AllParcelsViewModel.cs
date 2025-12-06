using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using wms_android.shared.Models;
using wms_android.shared.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Storage;

namespace wms_android.ViewModels
{
    public partial class AllParcelsViewModel : ObservableObject
    {
        private readonly IParcelService _parcelService;
        private readonly ILogger<AllParcelsViewModel> _logger;
        private IEnumerable<Parcel> _allParcels = new List<Parcel>();

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private ObservableCollection<Parcel> _filteredParcels = new();

        [ObservableProperty]
        private DateTime _fromDate = DateTime.Today.AddDays(-30);

        [ObservableProperty]
        private DateTime _toDate = DateTime.Today;

        [ObservableProperty]
        private string _selectedPaymentMode = "All";

        [ObservableProperty]
        private string _selectedDestination = "All";

        public ObservableCollection<string> PaymentModes { get; } = new()
        {
            "All", "COD", "Paid", "Contract"
        };

        public ObservableCollection<string> Destinations { get; } = new()
        {
            "All", "Eldoret", "Kapsabet", "Kakamega", "Kericho", "Kisumu", "Kitale", "Mombasa", "Nairobi", "Nakuru"
        };

        public AllParcelsViewModel(IParcelService parcelService, ILogger<AllParcelsViewModel> logger)
        {
            _parcelService = parcelService;
            _logger = logger;
        }

        [RelayCommand]
        public async Task LoadParcels()
        {
            try
            {
                IsLoading = true;
                _logger.LogInformation("Loading all parcels for user");

                var currentUserId = Preferences.Get("CurrentUserId", 0);
                if (currentUserId == 0)
                {
                    _logger.LogWarning("No current user ID found");
                    return;
                }

                _allParcels = await _parcelService.GetParcelsByUserAsync(currentUserId);
                ApplyFilters();
                
                _logger.LogInformation($"Loaded {_allParcels.Count()} parcels for user {currentUserId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading parcels");
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to load parcels", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        public void ApplyFilters()
        {
            try
            {
                var filtered = _allParcels.AsEnumerable();

                filtered = filtered.Where(p => p.CreatedAt.Date >= FromDate.Date && p.CreatedAt.Date <= ToDate.Date);

                if (SelectedPaymentMode != "All")
                {
                    filtered = filtered.Where(p => p.PaymentMethods?.Contains(SelectedPaymentMode) == true);
                }

                if (SelectedDestination != "All")
                {
                    filtered = filtered.Where(p => p.Destination == SelectedDestination);
                }

                FilteredParcels.Clear();
                foreach (var parcel in filtered.OrderByDescending(p => p.CreatedAt))
                {
                    FilteredParcels.Add(parcel);
                }

                _logger.LogInformation($"Applied filters: {FilteredParcels.Count} parcels match criteria");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error applying filters");
            }
        }

        [RelayCommand]
        public void ClearFilters()
        {
            FromDate = DateTime.Today.AddDays(-30);
            ToDate = DateTime.Today;
            SelectedPaymentMode = "All";
            SelectedDestination = "All";
            ApplyFilters();
        }

        partial void OnFromDateChanged(DateTime value) => ApplyFilters();
        partial void OnToDateChanged(DateTime value) => ApplyFilters();
        partial void OnSelectedPaymentModeChanged(string value) => ApplyFilters();
        partial void OnSelectedDestinationChanged(string value) => ApplyFilters();
    }
}