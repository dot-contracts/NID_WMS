using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wms_android.shared.Models
{
    public class DailyExpense
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Category { get; set; } // fuel, casual_labor, hired_cars, maintenance, office_supplies, utilities, transport, other
        
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        
        [Column(TypeName = "decimal(12,2)")]
        public decimal Amount { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        [StringLength(100)]
        public string Vendor { get; set; }
        
        [StringLength(50)]
        public string ReceiptNumber { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "pending"; // pending, approved, rejected
        
        [StringLength(500)]
        public string ApprovalNotes { get; set; }
        
        [StringLength(500)]
        public string RejectionReason { get; set; }
        
        public int? BranchId { get; set; }
        
        [ForeignKey("BranchId")]
        public virtual Branch Branch { get; set; }
        
        [StringLength(100)]
        public string BranchName { get; set; }
        
        public int? ClerkId { get; set; }
        
        [ForeignKey("ClerkId")]
        public virtual User Clerk { get; set; }
        
        [StringLength(100)]
        public string ClerkName { get; set; }
        
        public int CreatedById { get; set; }
        
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }
        
        [StringLength(100)]
        public string CreatedByName { get; set; }
        
        public int? ApprovedById { get; set; }
        
        [ForeignKey("ApprovedById")]
        public virtual User ApprovedBy { get; set; }
        
        [StringLength(100)]
        public string ApprovedByName { get; set; }
        
        public DateTime? ApprovedAt { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}