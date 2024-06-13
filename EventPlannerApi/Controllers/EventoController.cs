using ApplicationCore.Services;
using EventPlannerApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositorys.Models;
using System.Net;

namespace EventPlannerApi.Controllers
{
    [ApiController]
    [Route("Event")]
    public class EventoController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        public EventoController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> GetAll()
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceEvento service = new ServiceEvento(Configuration);

                List<Evento> events = await service.GetEventos();

                if (events == null && events.Count > 0)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Eventos no encontrados";
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Reporte encontrado";
                    response.Data = events;
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = e.Message;

                return StatusCode(response.StatusCode, response);
            }
        }
    }
}
