using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wms_android.shared.Models
{
    public class Drivers
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }
        public string LicenseNumber { get; set; }
    }
}
