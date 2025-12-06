using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using wms_android.shared.Interfaces;
using wms_android.shared.Models;

namespace wms_android.ViewModels
{
    public partial class VehicleSelectionViewModel : ObservableObject
    {
        private readonly IVehicleService _vehicleService;
        
        [ObservableProperty]
        private ObservableCollection<Vehicle> _vehicles;

        [ObservableProperty]
        private Vehicle _selectedVehicle;

        [ObservableProperty]
        private bool _isBusy;

        public VehicleSelectionViewModel(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
            Vehicles = new ObservableCollection<Vehicle>();
            Debug.WriteLine("VehicleSelectionViewModel initialized");
            LoadVehiclesCommand.Execute(null);
        }

        [RelayCommand]
        private async Task LoadVehicles()
        {
            IsBusy = true;
            
            try
            {
                // Clear existing vehicles
                Vehicles.Clear();
                
                // Get vehicles from service
                var vehicleList = await _vehicleService.GetVehiclesAsync();
                foreach (var vehicle in vehicleList)
                {
                    Vehicles.Add(vehicle);
                }
                
                Debug.WriteLine($"Loaded {Vehicles.Count} vehicles");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading vehicles: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task SelectVehicle()
        {
            if (SelectedVehicle == null)
            {
                return;
            }

            Debug.WriteLine($"Vehicle selected: {SelectedVehicle.VehicleRegistrationNumber}");
            
            // Save selected vehicle ID in preferences or state management
            await Shell.Current.GoToAsync("//Dashboard");
        }
    }
} 