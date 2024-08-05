using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Models
{
    public class AttendanceStatistics
    {
        public string EventName { get; set; }        // Nombre del evento
        public int TotalAttendance { get; set; }     // Total de asistencias
        public int TotalNoAttendence { get; set; }  // Total de registros
        public double AverageAttendancePercentage { get; set; }  // Porcentaje promedio de asistencia
        public double NonAttendancePercentage { get; set; }
    }
}
