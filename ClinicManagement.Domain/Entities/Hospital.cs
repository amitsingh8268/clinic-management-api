using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Domain.Entities
{
    public class Hospital
    {
        public int HospitalId { get; set; }
        public string HospitalCode { get; set; }
        public string HospitalName { get; set; }
    }
}
