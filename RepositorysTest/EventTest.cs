using AplicationCore.Utils;
using EventPlannerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Repositorys.Models;
using Repositorys.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorysTest
{
    public class EventTest
    {
        [Fact]
        public async void GetAllTest()
        {
            IRepositoryEvento repository = new RepositoryEvento();

            int expectedValue = 1;

            var result = await repository.GetEventos();

            Assert.NotEmpty(result);

        }

        [Fact]
        public async void CreateEventTest()
        {
            IRepositoryEvento repository = new RepositoryEvento();


            var evento = new Evento
            {
                Cupo = 10,
                Descripcion = "Evento de corridas y montaderas rusticas",
                Name = "El toro de oro",
                Fecha = DateTime.Now,
                Imagen="223"
            };

            int result = await repository.Create(evento);

            Assert.NotEqual<int>(0, result);

        }

        [Fact]
        public async void RegisterEventTest()
        {
            IRepositoryEvento repository = new RepositoryEvento();          

           
            int userId = 10; 
            int eventId = 2; 

           
            string result = await repository.RegisterUserToEventAsync(userId, eventId);

           
            Assert.Equal("Registrado en el evento con éxito", result);

        }

        [Fact]
        public async Task UnsubscribeFromEventTest()
        {

            IRepositoryEvento repository = new RepositoryEvento();

            
            int userId = 10;
            int eventId = 2; 
           
            int affectedRows = await repository.UnsubscribeFromEvent(userId, eventId);

            
            Assert.True(affectedRows > 0, "El número de filas afectadas debe ser mayor que 0.");
        }
    }
}
