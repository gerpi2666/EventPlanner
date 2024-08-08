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
        public ServiceEvento()
        {
            repository = new RepositoryEvento();
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

        public async Task<Evento> GetById(int id)
        {
           return await repository.GetById(id);
        }

        public async Task<String> RegisterUserToEventAsync(int userId, int eventId)
        {
            return await repository.RegisterUserToEventAsync( userId, eventId);
        }

        public async Task<List<Eventformating>> GetEventos()
        {
           return await repository.GetEventos();
        }

        public async Task<int> Uptade(Evento evento)
        {
           return await repository.Uptade(evento);
        }

        public async Task<List<Evento>> GetEventsByUserAsync(int userId)
        {
            return await repository.GetEventsByUserAsync(userId);
        }

        public async Task<int> UnsubscribeFromEvent(int userId, int eventId)
        {
            return await repository.UnsubscribeFromEvent(userId, eventId);
        }

        public async Task<List<AttendanceStatistics>> GetAttendanceStatistics()
        {
            return await repository.GetAttendanceStatistics();
        }

        public async Task<List<Eventformating>> GetEventosByCustomer(int id)
        {
            return await repository.GetEventosByCustomer(id);
        }

        public async Task<Evento> GetByName(string id)
        {
           return await repository.GetByName(id);
        }
    }
}
