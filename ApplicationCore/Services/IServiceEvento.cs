using Repositorys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceEvento
    {
        Task<List<Eventformating>> GetEventos();
        Task<Evento> GetById(int id);
        Task<int> Create(Evento evento);
        Task<int> Uptade(Evento evento);
        Task<int> Delete(int id);
        Task<int> CreateEvent(string descripcion, DateTime fecha, int cupo, string imagen);
    }
}
