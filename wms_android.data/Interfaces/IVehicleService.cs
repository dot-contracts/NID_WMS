using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wms_android.shared.Models;

namespace wms_android.data.Interfaces
{
    public interface IVehicleService
    {
        List<Vehicle> GetVehicles();
        List<Drivers> GetDrivers();
    }
}
