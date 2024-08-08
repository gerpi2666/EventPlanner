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

        //[Fact]
        //public async void CreateEventTest()
        //{
        //    IRepositoryEvento repository = new RepositoryEvento();

        //    var password = Cryptography.EncrypthAES("123456");
        //    var Event = new Evento {Cupo=10,Descripcion="Evento de corridas y montaderas rusticas",Name="El toro de oro",Fecha  };

        //    int result = await repository.Create(Event);

        //    Assert.NotEqual<int>(0, result);

        }
    }
}
