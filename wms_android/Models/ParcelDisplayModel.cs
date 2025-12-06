using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace wms_android.Models
{
    public partial class ParcelDisplayModel : ObservableObject
    {
        public Guid Id { get; set; }
        public string WaybillNumber { get; set; } = string.Empty;
        public string RecipientName { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string PaymentMethods { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Sender { get; set; } = string.Empty;
        
        [ObservableProperty]
        private bool _isSelected;
    }
}