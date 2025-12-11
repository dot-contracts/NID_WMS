using System;
using System.ComponentModel.DataAnnotations;

namespace wms_android.shared.DTOs
{
    public class CreateInvoiceDto
    {
        [Required]
        public int ContractCustomerId { get; set; }
        
        [Required]
        public DateTime IssueDate { get; set; }
        
        [Required]
        public DateTime DueDate { get; set; }
        
        [Required]
        public DateTime BillingPeriodStart { get; set; }
        
        [Required]
        public DateTime BillingPeriodEnd { get; set; }
        
        public string Notes { get; set; }
        
        [Required]
        public int CreatedById { get; set; }
        
        [Required]
        public List<string> ParcelIds { get; set; } = new List<string>();
    }
    
    public class UpdateInvoiceDto
    {
        public DateTime? IssueDate { get; set; }
        
        public DateTime? DueDate { get; set; }
        
        public DateTime? BillingPeriodStart { get; set; }
        
        public DateTime? BillingPeriodEnd { get; set; }
        
        public string Status { get; set; }
        
        public string Notes { get; set; }
    }
    
    public class RecordPaymentDto
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Payment amount must be greater than 0")]
        public decimal Amount { get; set; }
        
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        
        public string PaymentMethod { get; set; } = "cheque";
        
        public string PaymentReference { get; set; }
        
        public string Notes { get; set; }
    }
}