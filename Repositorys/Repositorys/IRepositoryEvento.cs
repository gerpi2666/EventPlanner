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
        Task<List<Evento>> GetEventos();
        Task<Evento> GetById(int id);
        Task<Evento> Create(Evento evento);
        Task<Evento> Uptade(Evento evento);
        void Delete(int id);    
    }
}
