using System.Collections.Generic;

namespace wms_android.shared.Models
{
    public class PaginatedResponse<T>
    {
        public IEnumerable<T> Data { get; set; } = new List<T>();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }

    public class ParcelQueryParams
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public string? Destination { get; set; }
        public ParcelStatus? Status { get; set; }
        public List<ParcelStatus>? Statuses { get; set; } // Support multiple statuses
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? CreatedByUsername { get; set; }
        public string? SearchTerm { get; set; }
    }
}