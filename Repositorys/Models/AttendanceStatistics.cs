using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Models
{
    public class AttendanceStatistics
    {
        public int TotalAttendance { get; set; }
        public int TotalRegistrations { get; set; }
        public double AverageAttendancePercentage { get; set; }
    }
}
