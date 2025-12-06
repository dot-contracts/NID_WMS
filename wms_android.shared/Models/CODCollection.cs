using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wms_android.shared.Models
{
    public class CODCollection
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string DispatchCode { get; set; }
        
        public Guid DispatchId { get; set; }
        
        [ForeignKey("DispatchId")]
        public virtual Dispatch Dispatch { get; set; }
        
        [Required]
        [StringLength(100)]
        public string DriverName { get; set; }
        
        [Required]
        [StringLength(20)]
        public string VehicleNumber { get; set; }
        
        [Required]
        [StringLength(100)]
        public string BranchName { get; set; }
        
        public int BranchId { get; set; }
        
        [ForeignKey("BranchId")]
        public virtual Branch Branch { get; set; }
        
        [Column(TypeName = "decimal(12,2)")]
        public decimal TotalCODAmount { get; set; }
        
        [Column(TypeName = "decimal(12,2)")]
        public decimal DepositedAmount { get; set; } = 0.00m;
        
        [Column(TypeName = "decimal(12,2)")]
        public decimal Shortfall { get; set; } = 0.00m;
        
        [Required]
        public DateTime CollectionDate { get; set; }
        
        public DateTime? DepositDate { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "collected"; // collected, deposited, reconciled
        
        [StringLength(500)]
        public string Notes { get; set; }
        
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