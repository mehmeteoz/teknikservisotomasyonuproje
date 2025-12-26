using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServisOtomasyonuProje
{
    internal class ServiceOperations
    {
        public int OperationID { get; set; }
        public int ServiceID { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public DateTime PerformedAt { get; set; }
    }
}
