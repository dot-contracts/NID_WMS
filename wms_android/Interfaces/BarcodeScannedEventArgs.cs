using System;

namespace wms_android.Interfaces
{
    public class BarcodeScannedEventArgs : EventArgs
    {
        public string Barcode { get; }
        public string Format { get; }
        public DateTime Timestamp { get; }

        public BarcodeScannedEventArgs(string barcode, string format)
        {
            Barcode = barcode;
            Format = format;
            Timestamp = DateTime.Now;
        }
    }
} 