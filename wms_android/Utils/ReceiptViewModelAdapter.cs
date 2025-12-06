using System;
using System.Collections.ObjectModel;
using wms_android.shared.Interfaces;
using wms_android.shared.Models;
using wms_android.ViewModels;

namespace wms_android.Utils
{
    /// <summary>
    /// Adapter methods for ReceiptViewModel to handle different constructor patterns
    /// NOTE: This class is deprecated. Use dependency injection to get ReceiptViewModel instances instead.
    /// </summary>
    [Obsolete("Use dependency injection to get ReceiptViewModel instances instead of manual creation")]
    public static class ReceiptViewModelAdapter
    {
        /// <summary>
        /// Creates a ReceiptViewModel for a single parcel
        /// NOTE: This method is deprecated. Use dependency injection instead.
        /// </summary>
        [Obsolete("Use dependency injection to get ReceiptViewModel instances")]
        public static void CreateForSingleParcel(IParcelService parcelService, Parcel parcel)
        {
            throw new InvalidOperationException("ReceiptViewModelAdapter is deprecated. Use dependency injection to get ReceiptViewModel instances from the service container instead.");
        }

        /// <summary>
        /// Creates a ReceiptViewModel for multiple parcels with a waybill
        /// NOTE: This method is deprecated. Use dependency injection instead.
        /// </summary>
        [Obsolete("Use dependency injection to get ReceiptViewModel instances")]
        public static void CreateForMultipleParcels(
            IParcelService parcelService, 
            ObservableCollection<Parcel> parcels, 
            string waybillNumber)
        {
            throw new InvalidOperationException("ReceiptViewModelAdapter is deprecated. Use dependency injection to get ReceiptViewModel instances from the service container instead.");
        }
    }
} 