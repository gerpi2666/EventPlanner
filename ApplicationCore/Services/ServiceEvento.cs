using Microsoft.Extensions.Configuration;
using Repositorys.Models;
using Repositorys.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEvento : IServiceEvento
    {
        private IRepositoryEvento repository;
        public ServiceEvento(IConfiguration configuration)
        {
            repository = new RepositoryEvento(configuration);
        }


        public Task<int> Create(Evento evento)
        {
           return repository.Create(evento);
        }

        public async Task<int> CreateEvent(string descripcion, DateTime fecha, int cupo,string imagen)
        {
            return await repository.InsertEventAsync(descripcion, fecha, cupo, imagen);
        }

        public async Task<int> Delete(int id)

        {
            return await repository.Delete(id);
        }

        public Task<Evento> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Eventformating>> GetEventos()
        {
           return await repository.GetEventos();
        }

        public async Task<int> Uptade(Evento evento)
        {
           return await repository.Uptade(evento);
        }
    }
}
