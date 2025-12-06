using System;

namespace wms_android.shared.Models
{
    public class SignatureData
    {
        public string SignatureImageData { get; set; }
        public DateTime SignedAt { get; set; }
        public string SignedBy { get; set; }
    }
} 