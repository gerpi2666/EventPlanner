using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Models
{
    public class Eventformating
    {
        public Eventformating()
        {
            Eventos = new List<Evento>();
        }

        public bool Activo { get; set; } 
        public  List<Evento> Eventos { get; set; }
    }
}


