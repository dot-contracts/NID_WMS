using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wms_android.shared.Models
{
    public class ContractCustomer
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        
        [StringLength(200)]
        public string CompanyName { get; set; }
        
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        
        [StringLength(20)]
        public string Phone { get; set; }
        
        [StringLength(500)]
        public string Address { get; set; }
        
        [StringLength(100)]
        public string ContactPerson { get; set; }
        
        [StringLength(50)]
        public string ContractNumber { get; set; }
        
        [StringLength(100)]
        public string PaymentTerms { get; set; } = "Net 30";
        
        [Column(TypeName = "decimal(5,2)")]
        public decimal TaxRate { get; set; } = 0.00m;
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        public int CreatedById { get; set; }
        
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }
    }
} 