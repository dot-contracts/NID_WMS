using System;

namespace wms_android.shared.DTOs
{
    /// <summary>
    /// DTO for returning ParcelDeposit data from API
    /// </summary>
    public class ParcelDepositDto
    {
        public int Id { get; set; }
        public Guid ParcelId { get; set; }
        public string? WaybillNumber { get; set; }
        public decimal DepositedAmount { get; set; }
        public decimal Expenses { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? CreatedById { get; set; }
        public string? CreatedByName { get; set; }
        public int? UpdatedById { get; set; }
        public string? UpdatedByName { get; set; }
        public decimal RemainingDebt { get; set; }
        
        // Parcel information for convenience
        public decimal ParcelTotalAmount { get; set; }
        public string? ParcelSender { get; set; }
        public string? ParcelReceiver { get; set; }
        public string? ParcelDestination { get; set; }
    }

    /// <summary>
    /// DTO for creating a new ParcelDeposit
    /// </summary>
    public class CreateParcelDepositDto
    {
        public Guid ParcelId { get; set; }
        public decimal DepositedAmount { get; set; }
        public decimal Expenses { get; set; }
        public string? Notes { get; set; }
        public int? CreatedById { get; set; }
    }

    /// <summary>
    /// DTO for updating an existing ParcelDeposit
    /// </summary>
    public class UpdateParcelDepositDto
    {
        public decimal DepositedAmount { get; set; }
        public decimal Expenses { get; set; }
        public string? Notes { get; set; }
        public int? UpdatedById { get; set; }
    }

    /// <summary>
    /// DTO for clerk cash-in summary
    /// </summary>
    public class ClerkCashInSummaryDto
    {
        public int ClerkId { get; set; }
        public string ClerkName { get; set; } = string.Empty;
        public string ClerkUsername { get; set; } = string.Empty;
        public decimal TotalPaidAmount { get; set; }
        public decimal TotalDeposited { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal RemainingDebt { get; set; }
        public int ParcelCount { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}