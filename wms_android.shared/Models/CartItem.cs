using System;

namespace wms_android.shared.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public string Sender { get; set; }
        public string SenderTelephone { get; set; }
        public string Receiver { get; set; }
        public string ReceiverTelephone { get; set; }
        public string Destination { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public string PaymentMethods { get; set; }
        public DateTime CreatedAt { get; set; }
        public string WaybillNumber { get; set; }
        public string QRCode { get; set; }
        public bool IsFinalized { get; set; }
        public DateTime? DispatchedAt { get; set; }
        public Parcel Parcel { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalRate { get; set; }

        public void UpdateTotals()
        {
            Amount = Weight * Rate;
            TotalAmount = Amount * Quantity;
            TotalRate = Rate * Quantity;
        }
    }
} 