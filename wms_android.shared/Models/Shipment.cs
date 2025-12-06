using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wms_android.shared.Models
{
    public class Shipment
    {
        [Key]
        public Guid Id { get; set; } // Primary Key

        [Required]
        [StringLength(50)] // Adjust length as needed
        public string WaybillNumber { get; set; } // This will have the unique constraint

        public DateTimeOffset CreatedAt { get; set; }

        public int? CreatedById { get; set; } // Foreign key to User
        [ForeignKey("CreatedById")]
        public virtual User Creator { get; set; } // Navigation property to User

        public string CreatorLastNameSnapshot { get; set; } // Snapshot for performance/history

        public virtual ICollection<Parcel> Parcels { get; set; } // Navigation property for related parcels

        public Shipment()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
            Parcels = new HashSet<Parcel>();
        }
    }
} 