using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wms_android.shared.Models
{
    public class ChequeDeposit
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string ChequeNumber { get; set; }
        
        [Required]
        [StringLength(100)]
        public string DrawerName { get; set; }
        
        [Required]
        [StringLength(100)]
        public string BankName { get; set; }
        
        [Column(TypeName = "decimal(12,2)")]
        public decimal Amount { get; set; }
        
        [Required]
        public DateTime DepositDate { get; set; }
        
        public DateTime? ClearanceDate { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "deposited"; // deposited, cleared, bounced, cancelled
        
        [StringLength(50)]
        public string RelatedInvoiceNumber { get; set; }
        
        public int? RelatedInvoiceId { get; set; }
        
        [ForeignKey("RelatedInvoiceId")]
        public virtual Invoice RelatedInvoice { get; set; }
        
        public int? ContractCustomerId { get; set; }
        
        [ForeignKey("ContractCustomerId")]
        public virtual ContractCustomer ContractCustomer { get; set; }
        
        [StringLength(100)]
        public string CustomerName { get; set; }
        
        public int? BranchId { get; set; }
        
        [ForeignKey("BranchId")]
        public virtual Branch Branch { get; set; }
        
        [StringLength(100)]
        public string BranchName { get; set; }
        
        [StringLength(500)]
        public string Notes { get; set; }
        
        [StringLength(500)]
        public string BounceReason { get; set; }
        
        public int CreatedById { get; set; }
        
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }
        
        public int? UpdatedById { get; set; }
        
        [ForeignKey("UpdatedById")]
        public virtual User UpdatedBy { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}