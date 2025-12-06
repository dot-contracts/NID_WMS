using System;
using System.ComponentModel.DataAnnotations;

namespace wms_android.shared.DTOs
{
    public class CreateChequeDepositDto
    {
        [Required]
        public string ChequeNumber { get; set; }
        
        [Required]
        public string DrawerName { get; set; }
        
        [Required]
        public string BankName { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }
        
        [Required]
        public DateTime DepositDate { get; set; }
        
        public string Status { get; set; } = "deposited";
        
        public string RelatedInvoiceNumber { get; set; }
        
        public int? RelatedInvoiceId { get; set; }
        
        public int? ContractCustomerId { get; set; }
        
        public string CustomerName { get; set; }
        
        public int? BranchId { get; set; }
        
        public string BranchName { get; set; }
        
        public string Notes { get; set; }
    }
    
    public class UpdateChequeDepositDto
    {
        public DateTime? ClearanceDate { get; set; }
        
        public string Status { get; set; }
        
        public string Notes { get; set; }
        
        public string BounceReason { get; set; }
    }
}