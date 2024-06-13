using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Models
{
    public class Evento
    {
        public int  Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public int Cupo  { get; set; }
        public string Imagen { get; set; }
    }
}
