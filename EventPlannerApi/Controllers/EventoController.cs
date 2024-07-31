using ApplicationCore.Services;
using ApplicationCore.Utils;
using EventPlannerApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Extensions.Options;
using Repositorys.Models;
using System.Configuration;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace EventPlannerApi.Controllers
{
    [ApiController]
    [Route("Event")]
    public class EventoController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly EmailSetting EmailConfiguration;

        public EventoController(IConfiguration configuration,IOptions<EmailSetting> Emn)
        {
            Configuration = configuration;
            EmailConfiguration = Emn.Value;
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
        [Route("delete")]
        public async Task<IActionResult> DeleteEvent(int Id)
        {

            ResponseModel response = new ResponseModel();
            IServiceEvento service = new ServiceEvento(Configuration);

            try
            {
                //int eventId = await service.CreateEvent( descripcion,  fecha, cupo, imagen);
                int eventId = await service.Delete(Id);

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

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int Id)
        {

            ResponseModel response = new ResponseModel();
            IServiceEvento service = new ServiceEvento(Configuration);

            try
            {
                //int eventId = await service.CreateEvent( descripcion,  fecha, cupo, imagen);
                Evento eventId = await service.GetById(Id);

                if (eventId == null)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "Error al crear el evento.";
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Evento creado correctamente.";
                    response.Data = eventId;
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

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateEvent(Evento evento)
        {

            ResponseModel response = new ResponseModel();
            IServiceEvento service = new ServiceEvento(Configuration);

            try
            {
                //int eventId = await service.CreateEvent( descripcion,  fecha, cupo, imagen);
                int eventId = await service.Uptade(evento);

                if (eventId == 0)
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

        [HttpPost]
        [Route("register-user")]
        public async Task<IActionResult> RegisterUserToEvent(int userId, int eventId)
        {
            ResponseModel response = new ResponseModel();
            IServiceEvento service = new ServiceEvento(Configuration);
            IServiceEmail serviceEmail = new ServiceEmail(EmailConfiguration);
            try
            {
                string message = await service.RegisterUserToEventAsync(userId, eventId);

                if (message.Contains("con éxito"))
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = message;
                    serviceEmail.SendEmail("Prueba", "PRUEBA MICHI PRUEBA", "laurnortiz05@gmail.com");

                }
                else if (message.Contains("Error"))
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = message;
                }
                else if (message.Contains("No hay cupos disponibles"))
                {
                    response.StatusCode = (int)HttpStatusCode.Conflict; 
                    response.Message = message;
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = "Resultado desconocido";
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

        [HttpGet]
        [Route("get-events-by-user")]
        public async Task<IActionResult> GetEventByUser(int usertId)
        {
            ResponseModel response = new ResponseModel();
            IServiceEvento service = new ServiceEvento(Configuration);

            try
            {
                var events = await service.GetEventsByUserAsync(usertId);

                if (events == null || events.Count == 0)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "No se encontraron eventos para este usuario.";
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Eventos encontrados.";
                    response.Data = events;
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
