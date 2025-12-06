using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wms_android.shared.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string InvoiceNumber { get; set; }
        
        public int CustomerId { get; set; }
        
        [ForeignKey("CustomerId")]
        public virtual ContractCustomer Customer { get; set; }
        
        [Required]
        public DateTime IssueDate { get; set; }
        
        [Required]
        public DateTime DueDate { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "draft";
        
        [Column(TypeName = "decimal(12,2)")]
        public decimal Subtotal { get; set; } = 0.00m;
        
        [Column(TypeName = "decimal(12,2)")]
        public decimal TaxAmount { get; set; } = 0.00m;
        
        [Column(TypeName = "decimal(12,2)")]
        public decimal TotalAmount { get; set; } = 0.00m;
        
        [Column(TypeName = "decimal(12,2)")]
        public decimal PaidAmount { get; set; } = 0.00m;
        
        // Foreign key for Contract Customer (alternative to CustomerId for clarity)
        public int ContractCustomerId { get; set; }
        
        // Billing period information
        public DateTime BillingPeriodStart { get; set; }
        public DateTime BillingPeriodEnd { get; set; }
        
        [StringLength(1000)]
        public string Notes { get; set; }
        
        public int CreatedById { get; set; }
        
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation property for invoice items
        public virtual ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    }
} 