using Repositorys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Repositorys
{
    public interface IRepositoryEvento
    {
        Task<List<Eventformating>> GetEventos();
        Task<Evento> GetById(int id);
        Task<int> Create(Evento evento);
        Task<int> Uptade(Evento evento);
        Task<int> Delete(int id);
        Task<int> InsertEventAsync(string descripcion, DateTime fecha, int cupo, string  imagen);
        Task<String> RegisterUserToEventAsync(int userId, int eventId);
        Task<List<Evento>> GetEventsByUserAsync(int userId);
    }
}
