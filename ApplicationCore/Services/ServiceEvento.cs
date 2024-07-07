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


        public Task<Evento> Create(Evento evento)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateEvent(string descripcion, DateTime fecha, int cupo, byte[] imagen)
        {
            return await repository.InsertEventAsync(descripcion, fecha, cupo, imagen);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Evento> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Evento>> GetEventos()
        {
           return await repository.GetEventos();
        }

        public Task<Evento> Uptade(Evento evento)
        {
            throw new NotImplementedException();
        }
    }
}
