using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Repositorys.Models
{
    public class Usuario
    {
        public int? Id { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Rol { get; set; }
        [JsonIgnore]
        public bool Activo { get; set; }
        public string? RolDescripcion { get; set; }
        public string NombreUsuario { get; set; }
    }
}
