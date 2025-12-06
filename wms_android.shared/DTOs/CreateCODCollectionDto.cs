using System;
using System.ComponentModel.DataAnnotations;

namespace wms_android.shared.DTOs
{
    public class CreateCODCollectionDto
    {
        [Required]
        public Guid DispatchId { get; set; } // DispatchCode will be auto-populated from the linked Dispatch
        
        [Required]
        public string DriverName { get; set; }
        
        [Required]
        public string VehicleNumber { get; set; }
        
        [Required]
        public string BranchName { get; set; }
        
        [Required]
        public int BranchId { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal TotalCODAmount { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal DepositedAmount { get; set; } = 0.00m;
        
        [Required]
        public DateTime CollectionDate { get; set; }
        
        public DateTime? DepositDate { get; set; }
        
        public string Status { get; set; } = "collected";
        
        public string Notes { get; set; }
    }
    
    public class UpdateCODCollectionDto
    {
        [Range(0, double.MaxValue)]
        public decimal DepositedAmount { get; set; }
        
        public DateTime? DepositDate { get; set; }
        
        public string Status { get; set; }
        
        public string Notes { get; set; }
    }
}