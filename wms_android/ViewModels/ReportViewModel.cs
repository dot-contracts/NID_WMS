using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using wms_android.data.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using wms_android.shared.Models;

namespace wms_android.ViewModels
{
    public partial class ReportViewModel : ObservableObject
    {
        private readonly wms_android.data.Interfaces.IParcelService _parcelService;
        
        [ObservableProperty]
        private bool _isLoading;
        
        [ObservableProperty]
        private DateTime _startDate = DateTime.Today.AddDays(-30);
        
        [ObservableProperty]
        private DateTime _endDate = DateTime.Today;
        
        [ObservableProperty]
        private bool _isReportGenerated;
        
        [ObservableProperty]
        private int _totalParcels;
        
        [ObservableProperty]
        private decimal _totalSales;
        
        [ObservableProperty]
        private int _deliveredParcels;
        
        [ObservableProperty]
        private int _pendingParcels;
        
        [ObservableProperty]
        private ObservableCollection<wms_android.shared.Models.Parcel> _parcels = new ObservableCollection<wms_android.shared.Models.Parcel>();
        
        public ReportViewModel(wms_android.data.Interfaces.IParcelService parcelService)
        {
            _parcelService = parcelService;
        }
        
        [RelayCommand]
        private async Task GenerateReport()
        {
            if (IsLoading)
                return;
                
            IsLoading = true;
            IsReportGenerated = false;
            
            try
            {
                // Clear previous data
                Parcels.Clear();
                
                // Get report data for end date (most recent date in range)
                TotalParcels = await _parcelService.GetParcelCountAsync(EndDate);
                TotalSales = (decimal)await _parcelService.GetTotalSalesAsync(EndDate);
                DeliveredParcels = await _parcelService.GetDeliveredParcelCountAsync(EndDate);
                
                // Calculate pending parcels
                PendingParcels = TotalParcels - DeliveredParcels;
                
                // Load all parcels and filter by date range
                var allParcels = await _parcelService.GetAllParcelsAsync();
                
                if (allParcels != null)
                {
                    var filteredParcels = allParcels
                        .Where(p => p.CreatedAt >= StartDate && p.CreatedAt <= EndDate)
                        .ToList();
                        
                    foreach (var dataParcel in filteredParcels)
                    {
                        // Convert data project parcel to shared project parcel
                        var sharedParcel = new wms_android.shared.Models.Parcel
                        {
                            Id = dataParcel.Id,
                            CreatedAt = dataParcel.CreatedAt,
                            WaybillNumber = dataParcel.WaybillNumber,
                            QRCode = dataParcel.QRCode,
                            DispatchedAt = dataParcel.DispatchedAt,
                            DispatchTrackingCode = dataParcel.DispatchTrackingCode,
                            Sender = dataParcel.Sender,
                            SenderTelephone = dataParcel.SenderTelephone,
                            Receiver = dataParcel.Receiver,
                            ReceiverTelephone = dataParcel.ReceiverTelephone,
                            Destination = dataParcel.Destination,
                            Quantity = dataParcel.Quantity,
                            Description = dataParcel.Description,
                            Amount = dataParcel.Amount,
                            Rate = dataParcel.Rate,
                            PaymentMethods = dataParcel.PaymentMethods,
                            TotalAmount = dataParcel.TotalAmount,
                            TotalRate = dataParcel.TotalRate,
                            Status = (wms_android.shared.Models.ParcelStatus)(int)dataParcel.Status
                        };
                        
                        Parcels.Add(sharedParcel);
                    }
                }
                
                IsReportGenerated = true;
            }
            catch (Exception ex)
            {
                // Handle errors
                System.Diagnostics.Debug.WriteLine($"Error generating report: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }
        
        [RelayCommand]
        private Task ExportReport()
        {
            // Export functionality would go here
            return Task.CompletedTask;
        }
        
        [RelayCommand]
        private Task ShareReport()
        {
            // Share functionality would go here
            return Task.CompletedTask;
        }
    }
} 