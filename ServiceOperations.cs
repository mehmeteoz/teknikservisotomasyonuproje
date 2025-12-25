using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServisOtomasyonuProje
{
    internal class ServiceOperations
    {
        int OperationID { get; set; }
        int ServiceID { get; set; }
        string Description { get; set; }
        double Cost { get; set; }
        DateTime PerformedAt { get; set; }
    }
}
