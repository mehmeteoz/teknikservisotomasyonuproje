using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServisOtomasyonuProje
{
    internal class ServiceReports
    {
        public int ReportID { get; set; }
        public int ServiceID { get; set; }
        public int TechnicianID { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
