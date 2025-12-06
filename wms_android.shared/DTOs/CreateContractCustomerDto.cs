using System.ComponentModel.DataAnnotations;

namespace wms_android.shared.DTOs
{
    public class CreateContractCustomerDto
    {
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
        public string? ContractNumber { get; set; } // Optional - will be auto-generated if not provided
        
        [StringLength(100)]
        public string PaymentTerms { get; set; } = "Net 30";
        
        [Range(0, 100)]
        public decimal TaxRate { get; set; } = 0.00m;
    }
    
    public class UpdateContractCustomerDto
    {
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
        public string PaymentTerms { get; set; }
        
        [Range(0, 100)]
        public decimal? TaxRate { get; set; }
        
        public bool? IsActive { get; set; }
    }
}