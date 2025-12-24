using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServisOtomasyonuProje
{
    internal class UserServices
    {
        public int ServiceID { get; set; }
        public int CustomerID { get; set; }
        public int? AssignedStaffID { get; set; }
        public string DeviceType { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string ProblemDescription { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string Picture64 { get; set; }
}
}
