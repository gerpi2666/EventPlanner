using ApplicationCore.Services;
using EventPlannerApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Repositorys.Models;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

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

                List<Eventformating> events = await service.GetEventos();

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



        [HttpPost]
        [Route("create-event")]
        public async Task<IActionResult> CreateEvent(Evento evento)
        {

            ResponseModel response = new ResponseModel();
            IServiceEvento service = new ServiceEvento(Configuration);

            try
            {
                //int eventId = await service.CreateEvent( descripcion,  fecha, cupo, imagen);
                int eventId = await service.Create(evento);

                if (eventId <= 0)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "Error al crear el evento.";
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Evento creado correctamente.";
                    response.Data = new { EventId = eventId };
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;

                return StatusCode(response.StatusCode, response);
            }
        }


    } 
}
