using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wms_android.shared.Models
{
    public class InvoiceItem
    {
        [Key]
        public int Id { get; set; }
        
        public int InvoiceId { get; set; }
        
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }
        
        [Required]
        [StringLength(100)]
        public string ParcelId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string WaybillNumber { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        
        public int Quantity { get; set; } = 1;
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
} 