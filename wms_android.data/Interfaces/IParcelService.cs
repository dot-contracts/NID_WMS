using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wms_android.shared.Models;

namespace wms_android.data.Interfaces
{
    public interface IParcelService
    {
        Task<bool> CheckDatabaseConnectionAsync();
        bool IsNetworkAvailable();
        Task CreateParcelAsync(Parcel parcel);
        Task CreateCartParcels(List<Parcel> parcels);
        Task<List<Parcel>> GetAllParcelsAsync();
        Task<Parcel> GetParcelByWaybillAsync(string waybillNumber);
        Task<Parcel> GetParcelByWaybillNumberAsync(string waybillNumber);
        Task<Parcel> GetParcelByQRCodeAsync(string qrCode);
        Task DispatchParcelAsync(Parcel parcel);
        Task DispatchParcelAsync(string waybillNumber);
        Task<string> GenerateWaybillNumberAsync();
        Task FinalizeWaybillAsync();
        Task<int> GetParcelCountForDateAsync(DateTime date);
        Task<decimal> GetTotalSalesForDateAsync(DateTime date);
        Task<IEnumerable<Parcel>> GetPendingOrdersAsync();
        Task<int> GetParcelCountAsync();
        Task<decimal> GetTotalSalesAsync();
        Task<IEnumerable<Parcel>> GetPendingParcelsAsync();
        
        // Dashboard methods
        Task<int> GetParcelCountAsync(DateTime date);
        Task<int> GetDeliveredParcelCountAsync(DateTime date);
        Task<double> GetTotalSalesAsync(DateTime date);
    }
} 