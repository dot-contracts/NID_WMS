using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wms_android.shared.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string VehicleRegistrationNumber { get; set; }
        public string BodyType { get; set; }
    }

}
